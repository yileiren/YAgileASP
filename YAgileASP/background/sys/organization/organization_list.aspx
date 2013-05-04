<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="organization_list.aspx.cs" Inherits="YAgileASP.background.sys.organization.organization_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组织机构</title>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="cache-control" ontent="no-cache">  
    <meta http-equiv="expires" content="0">  

    <link href="../../../js/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/jquery-easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/table.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../js/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="../../../js/jquery-easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../js/jquery-easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../../js/YWindows.js"></script>

    <script type="text/javascript" language="javascript">
        /*!
         * \brief
         * 新增组织机构。
         * 作者：董帅 创建时间：2012-8-22 13:04:30。
         */
        function addOrganization()
        {
            window.parent.popupsWindow("#popups", "新增组织机构", 600, 140, "sys/organization/organization_edit.aspx?parentId=" + $("#hidParentId").val(), "icon-add", true, true);
        }

        /*!
        * \brief
        * 新增组织机构。
        * 作者：董帅 创建时间：2012-8-23 13:19:07。
        */
        function addUser()
        {
            window.parent.popupsWindow("#popups", "新增用户", 600, 230, "sys/organization/user_edit.aspx?orgId=" + $("#hidParentId").val(), "icon-add", true, true);
        }

        /*!
         * \brief
         * 修改项目。
         * 作者：董帅 创建时间：2012-8-22 22:16:52
         */
        function editItem()
        {
            //判断选中
            if (($("input:checked[type='checkbox'][name='chkOrg']").length + $("input:checked[type='checkbox'][name='chkUser']").length) != 1)
            {
                alert("请选中要编辑的机构或用户，一次只能选择一个！");
                return;
            }

            if ($("input:checked[type='checkbox'][name='chkOrg']").length == 1)
            {
                window.parent.popupsWindow("#popups", "修改组织机构", 600, 140, "sys/organization/organization_edit.aspx?parentId=" + $("#hidParentId").val() + "&id=" + $("input:checked[type='checkbox'][name='chkOrg']").eq(0).val(), "icon-edit", true, true);
            }
            else
            {
                window.parent.popupsWindow("#popups", "修改用户", 600, 230, "sys/organization/user_edit.aspx?orgId=" + $("#hidParentId").val() + "&id=" + $("input:checked[type='checkbox'][name='chkUser']").eq(0).val(), "icon-edit", true, true);
            }
        }

        /*!
         * \brief 
         * 返回上级机构.
         * 作者：董帅 创建时间：2012-8-23 23:43:15
         */
         
        function returnParent()
        {
           window.parent.menuButtonOnClick('组织机构管理', 'icon-organization', 'sys/organization/organization_list.aspx?parentId=' + $("#hidReturnId").val());
        }

        /*!
         * \brief
         * 删除用户和机构。
         * 作者：董帅 创建时间：2012-8-24 16:56:42
         */
        function deleteItem()
        {
            //判断选中
            if (($("input:checked[type='checkbox'][name='chkOrg']").length + $("input:checked[type='checkbox'][name='chkUser']").length) > 0)
            {
         
                return confirm("确认要删除选中的机构或用户？删除后将连同子机构和用户一并删除，且不可恢复！");
            }
            else
            {
                alert("请选中要删除的机构或用户！");
                return false;
            }
        }

        /*!
         * \brief
         * 设置角色。
         * 作者：董帅 创建时间：2012-8-27 17:29:58
         */
        function setRole()
        {
            //判断选中
            if ($("input:checked[type='checkbox'][name='chkUser']").length != 1)
            {
                alert("请选中要设置的用户，一次只能选择一个！");
                return;
            }

            window.parent.popupsWindow("#popups", "设置角色", 270, 500, "sys/organization/chouseRole.aspx?userId=" + $("input:checked[type='checkbox'][name='chkUser']").eq(0).val(), "icon-role", true, true);
        }
    </script>
</head>
<body class="easyui-layout" style="margin:0px;background-color:#EEF5FD;">
    <div region="north" border="true" style="height:28px;background-color:#EEF5FD">
        <div style="width:380px;margin-left:auto;margin-top:0px;margin-right:0px">
            <a href="#" class="easyui-linkbutton" iconCls="icon-add" plain="true" onclick="javascript:addOrganization();">新增机构</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-add" plain="true" onclick="javascript:addUser();">新增用户</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-edit" plain="true" onclick="javascript:editItem();">修改</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" plain="true" onclick="javascript:return deleteItem();" runat="server" onserverclick="butDeleteItems_Click">删除</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-role" plain="true" onclick="javascript:setRole();">设置角色</a>
        </div>
    </div>
    <div id="center" region="center" style="padding:3px;background-color:#EEF5FD"">
    <form id="form1" runat="server">
    <input type="hidden" id="hidParentId" name="hidParentId" runat="server" />
    <input type="hidden" id="hidReturnId" name="hidReturnId" runat="server" />
    <div style="width:600px;height:28px;margin:0px auto">
        <a id="returnButton" href="#" class="easyui-linkbutton" iconCls="icon-back" plain="true" onclick="javascript:returnParent();" runat="server">返回</a>
        <span>当前机构：</span>
        <span id="spanParentName" runat="server" style="font-size:16px;font-weight:bold"></span>
    </div>
    <div style="width:600px;margin:0px auto">
        <table class="listTable" style="width:100%">
            <tr class="tableHead">
                <th style="width:30px">选择</th>
                <th style="width:auto">名称</th>
                <th style="width:30px">序号</th>
            </tr>
        <asp:Repeater ID="orgList" runat="server">
        <ItemTemplate>
            <tr style="width:100%;height:30px">
                <td class="admincls0" style="text-align:center;"><input type="checkbox" value="<%#Eval("ID") %>" name="chkOrg" /></td>
                <td class="admincls0" style="width:auto">
                    <a href="organization_list.aspx?parentId=<%#Eval("ID") %>" class="easyui-linkbutton" id="<%#Eval("ID") %>" iconCls="icon-organization" plain="true" style="width:523px" ><%#Eval("NAME")%></a>
                </td>
                <td class="admincls0" style="text-align:center">
                    <%#Eval("order") %>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="width:100%;height:30px">
                <td class="admincls1" style="text-align:center;"><input type="checkbox" value="<%#Eval("ID") %>" name="chkOrg" /></td>
                <td class="admincls1" style="width:auto">
                    <a href="organization_list.aspx?parentId=<%#Eval("ID") %>" class="easyui-linkbutton" id="<%#Eval("ID") %>" iconCls="icon-organization" plain="true" style="width:523px" ><%#Eval("NAME")%></a>
                </td>
                <td class="admincls1" style="text-align:center">
                    <%#Eval("order") %>
                </td>
            </tr>
        </AlternatingItemTemplate>
        </asp:Repeater>
        <tr style="width:100%;height:5px"><td  style="background-color:#AEDEF2" colspan="3"></td></tr>
        <asp:Repeater ID="userList" runat="server">
        <ItemTemplate>
            <tr style="width:100%;height:30px">
                <td class="admincls0" style="text-align:center;"><input type="checkbox" value="<%#Eval("ID") %>" name="chkUser" /></td>
                <td class="admincls0" style="width:auto">
                    <%#Eval("NAME")%>
                </td>
                <td class="admincls0" style="text-align:center">
                    <%#Eval("order") %>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="width:100%;height:30px">
                <td class="admincls1" style="text-align:center;"><input type="checkbox" value="<%#Eval("ID") %>" name="chkUser" /></td>
                <td class="admincls1" style="width:auto">
                    <%#Eval("NAME")%>
                </td>
                <td class="admincls1" style="text-align:center">
                    <%#Eval("order") %>
                </td>
            </tr>
        </AlternatingItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </form>
    </div>
</body>
</html>
