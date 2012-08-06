<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu_list.aspx.cs" Inherits="YAgileASP.background.sys.menu.menu_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统菜单列表</title>

    <link href="../../../js/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/jquery-easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../js/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="../../../js/jquery-easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../js/jquery-easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../../js/YWindows.js"></script>
</head>
<body style="width:100%;height:600px;margin:0px;background-color:#EEF5FD">
    <form id="form1" runat="server">
    <div style="width:1024px;height:768px;margin:0px auto">
    <input type="hidden" id="selectGroupId" name="selectGroupId" value="" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
        <div class="easyui-panel" fit="true" style="width:1024px;height:600px;padding:5px;background-color:#EEF5FD">
            <div class="easyui-layout"  style="width: 100%;height: 100%;background-color:#EEF5FD">
            <div region="west" border="false" style="width:250px;padding:0px;background-color:#EEF5FD">
                <div id="p" class="easyui-panel" title="菜单分组" fit="true" style="overflow-x:hidden;background-color:#EEF5FD">
                    <asp:Repeater ID="menuGroups" runat="server">
                        <ItemTemplate>
                        <a href="#" class="easyui-linkbutton" id="<%#Eval("ID") %>" iconCls="<%#Eval("ICON") %>" plain="true" style="width:100%" onclick="javascript:menuButtonOnClick('<%#Eval("NAME") %>','<%#Eval("ICON") %>','<%#Eval("URL") %>');"><%#Eval("NAME") %></a>            
                        </ItemTemplate>
                    </asp:Repeater>
	            </div>
            </div>
            <div region="center" title="<%=groupTitle %>" iconCls="<%=groupIcon %>" style="background-color:#EEF5FD">
                <asp:Repeater ID="childs" runat="server">
                    <HeaderTemplate>
                        <table>
                        <tr><th>名称</th></tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr><td><%#Eval("NAME") %></td></tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
