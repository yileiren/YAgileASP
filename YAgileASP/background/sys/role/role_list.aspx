<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="role_list.aspx.cs" Inherits="YAgileASP.background.sys.role.role_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色管理</title>

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
    </script>
</head>
<body class="easyui-layout" style="margin:0px;background-color:#EEF5FD;">
    <div region="north" border="true" style="height:28px;background-color:#EEF5FD">
        <div style="width:200px;margin-left:auto;margin-top:0px;margin-right:0px">
            <a id="A1" href="#" class="easyui-linkbutton" iconCls="icon-add" plain="true" onclick="javascript:addRole();">新增</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-edit" plain="true" onclick="javascript:window.parent.closePopupsWindow('#popups')">修改</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" plain="true" onclick="javascript:window.parent.closePopupsWindow('#popups')">删除</a>
        </div>
    </div>
    <div id="center" region="center" style="padding:3px;background-color:#EEF5FD"">
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="rolesRepeater" runat="server">
            <HeaderTemplate>
                <table class="admintable" style="width:100%">
                <tr style="width:100%;height:30px">
                    <th class="adminth" style="width:40px">选择</th>
                    <th class="adminth" style="width:200px">名称</th>
                    <th class="adminth" style="width:auto">说明</th>
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
