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
         * 修改项目。
         * 作者：董帅 创建时间：2012-8-22 22:16:52
         */
        function editItem()
        {
            //判断选中
            if ($("input:checked[type='checkbox'][name='chkOrgs']").length != 1)
            {
                alert("请选中要编辑的机构或用户，一次只能选择一个！");
                return;
            }

            if ($("input:checked[type='checkbox'][name='chkOrgs']").length == 1)
            {
                window.parent.popupsWindow("#popups", "编辑组织机构", 600, 140, "sys/organization/organization_edit.aspx?parentId=" + $("#hidParentId").val() + "&id=" + $("input:checked[type='checkbox'][name='chkOrgs']").eq(0).val(), "icon-add", true, true);
            }
        }
    </script>
</head>
<body class="easyui-layout" style="margin:0px;background-color:#EEF5FD;">
    <div region="north" border="true" style="height:28px;background-color:#EEF5FD">
        <div style="width:280px;margin-left:auto;margin-top:0px;margin-right:0px">
            <a href="#" class="easyui-linkbutton" iconCls="icon-add" plain="true" onclick="javascript:addOrganization();">新增机构</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-add" plain="true" onclick="javascript:addRole();">新增</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-edit" plain="true" onclick="javascript:editItem();">修改</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" plain="true" onclick="javascript:return deleteRoles();" runat="server">删除</a>
        </div>
    </div>
    <div id="center" region="center" style="padding:3px;background-color:#EEF5FD"">
    <form id="form1" runat="server">
    <input type="hidden" id="hidParentId" name="hidParentId" runat="server" />
    <div style="width:600px;height:28px;margin:0px auto">
        <a id="returnButton" href="#" class="easyui-linkbutton" iconCls="icon-back" plain="true" onclick="javascript:addOrganization();" runat="server">返回</a>
    </div>
    <div style="width:600px;margin:0px auto">
        <table class="admintable" style="width:100%">
            <tr style="width:100%;height:30px">
                <th class="adminth" style="width:30px">选择</th>
                <th class="adminth" style="width:auto">名称</th>
                <th class="adminth" style="width:30px">序号</th>
            </tr>
        <asp:Repeater ID="orgList" runat="server">
        <ItemTemplate>
            <tr style="width:100%;height:30px">
                <td class="admincls0" style="text-align:center;"><input type="checkbox" value="<%#Eval("ID") %>" name="chkOrgs" /></td>
                <td class="admincls0" style="width:auto">
                    <a href="#" class="easyui-linkbutton" id="<%#Eval("ID") %>" iconCls="icon-organization" plain="true" style="width:523px" ><%#Eval("NAME")%></a>
                </td>
                <td class="admincls0" style="text-align:center">
                    <%#Eval("order") %>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="width:100%;height:30px">
                <td class="admincls1" style="text-align:center;"><input type="checkbox" value="<%#Eval("ID") %>" name="chkOrgs" /></td>
                <td class="admincls1" style="width:auto">
                    <a href="#" class="easyui-linkbutton" id="<%#Eval("ID") %>" iconCls="icon-organization" plain="true" style="width:523px" ><%#Eval("NAME")%></a>
                </td>
                <td class="admincls0" style="text-align:center">
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
