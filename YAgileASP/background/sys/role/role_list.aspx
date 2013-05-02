<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="role_list.aspx.cs" Inherits="YAgileASP.background.sys.role.role_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色管理</title>
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
         * 新增角色。
         * 作者：董帅 创建时间：2012-8-15 22:27:32
         */
        function addRole()
        {
            window.parent.popupsWindow("#popups", "新增角色", 600, 170, "sys/role/role_edit.aspx", "icon-add", true, true);
        }

        /*!
         * \brief
         * 修改权限。
         * 作者：董帅 创建时间：2012-8-16 22:03:13
         */
        function editRole()
        {
            //判断选中
            if ($("input:checked[type='checkbox'][name='chkItem']").length != 1)
            {
                alert("请选中要编辑的权限，一次只能选择一个！");
                return;
            }

            //打开编辑页面
            window.parent.popupsWindow("#popups", "修改权限", 600, 170, "sys/role/role_edit.aspx?id=" + $("input:checked[type='checkbox'][name='chkItem']").eq(0).val(), "icon-edit", true, true);
        }

        /*!
         * \brief
         * 删除权限。
         * 作者：董帅 创建时间：2012-8-16 22:27:50
         */
        function deleteRoles()
        {
            //判断选中
            if ($("input:checked[type='checkbox'][name='chkItem']").length > 0)
            {
                return confirm("确认要删除选中的角色？");
            }
            else
            {
                alert("请选中要删除的角色！");
                return false;
            }
        }

        /*!
         * \brief
         * 权限访问菜单设置按钮。
         * 作者：董帅 创建时间：2012-8-20 21:30:28
         */
        function menuSet()
        {
            //判断选中
            if ($("input:checked[type='checkbox'][name='chkItem']").length != 1)
            {
                alert("请选中要设置的权限，一次只能选择一个！");
                return;
            }

            //打开编辑页面
            window.parent.popupsWindow("#popups", "选择菜单", 350, 500, "sys/role/chouseMenu.aspx?id=" + $("input:checked[type='checkbox'][name='chkItem']").eq(0).val(), "icon-menu", true, true);
        }
    </script>
</head>
<body class="easyui-layout" style="margin:0px;background-color:#EEF5FD;">
    <div region="north" border="true" style="height:28px;background-color:#EEF5FD">
        <div style="width:280px;margin-left:auto;margin-top:0px;margin-right:0px">
            <a id="A2" href="#" class="easyui-linkbutton" iconCls="icon-menu" plain="true" onclick="javascript:menuSet();">访问菜单</a>
            <a id="A1" href="#" class="easyui-linkbutton" iconCls="icon-add" plain="true" onclick="javascript:addRole();">新增</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-edit" plain="true" onclick="javascript:editRole();">修改</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" plain="true" onclick="javascript:return deleteRoles();" runat="server" onserverclick="butDeleteRoles_Click">删除</a>
        </div>
    </div>
    <div id="center" region="center" style="padding:3px;background-color:#EEF5FD"">
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="rolesRepeater" runat="server">
            <HeaderTemplate>
                <table class="admintable" style="width:100%">
                <tr class="tableHead">
                    <th style="width:40px">选择</th>
                    <th style="width:200px">名称</th>
                    <th style="width:auto">说明</th>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="width:100%;height:30px">
                    <td class="admincls0" style="text-align:center">
                        <input type="checkbox" name="chkItem" value="<%#Eval("id") %>" />
                    </td>
                    <td class="admincls0" style="text-align:center"><%#Eval("name") %></td>
                    <td class="admincls0"><%#Eval("explain")%></td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="width:100%;height:30px">
                    <td class="admincls1" style="text-align:center">
                        <input type="checkbox" name="chkItem" value="<%#Eval("id") %>" />
                    </td>
                    <td class="admincls1" style="text-align:center"><%#Eval("name") %></td>
                    <td class="admincls1"><%#Eval("explain")%></td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
    </div>
</body>
</html>
