<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu_list.aspx.cs" Inherits="YAgileASP.background.sys.menu.menu_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统菜单列表</title>

    <link href="../../../js/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/jquery-easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/table.css" rel="stylesheet" type="text/css" />

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
                <div id="p" class="easyui-panel" title="菜单分组" fit="true" style="overflow-x:hidden;background-color:#FFFFFF">
                    <asp:Repeater ID="menuGroups" runat="server">
                        <ItemTemplate>
                        <a href="#" class="easyui-linkbutton" id="<%#Eval("ID") %>" iconCls="<%#Eval("ICON") %>" plain="true" style="width:100%" onclick="javascript:menuButtonOnClick('<%#Eval("NAME") %>','<%#Eval("ICON") %>','<%#Eval("URL") %>');"><%#Eval("NAME") %></a>            
                        </ItemTemplate>
                    </asp:Repeater>
	            </div>
            </div>
            <div region="center" title="<%=groupTitle %>" iconCls="<%=groupIcon %>" style="background-color:#FFFFFF">
                <asp:Repeater ID="childs" runat="server">
                    <HeaderTemplate>
                        <table class="admintable" style="width:100%">
                        <tr style="width:100%;height:30px">
                            <th class="adminth" style="width:40px">选择</th>
                            <th class="adminth" style="width:200px">名称</th>
                            <th class="adminth" style="width:300px">页面URL</th>
                            <th class="adminth" style="width:100px">菜单图标</th>
                            <th class="adminth" style="width:100px">桌面图标</th>
                            <th class="adminth" style="width:40px">序号</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="width:100%;height:30px">
                            <td class="admincls0" style="text-align:center">
                                <input type="checkbox" id="<%#Eval("id") %>" />
                            </td>
                            <td class="admincls0" style="text-align:center"><%#Eval("name") %></td>
                            <td class="admincls0"><%#Eval("url") %></td>
                            <td class="admincls0" style="text-align:center"><%#Eval("icon") %></td>
                            <td class="admincls0" style="text-align:center"><%#Eval("desktopIcon")%></td>
                            <td class="admincls0" style="text-align:center"><%#Eval("order")%></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="width:100%;height:30px">
                            <td class="admincls1" style="text-align:center">
                                <input type="checkbox" id="<%#Eval("id") %>" />
                            </td>
                            <td class="admincls1" style="text-align:center"><%#Eval("name") %></td>
                            <td class="admincls1"><%#Eval("url") %></td>
                            <td class="admincls1" style="text-align:center"><%#Eval("icon") %></td>
                            <td class="admincls1" style="text-align:center"><%#Eval("desktopIcon")%></td>
                            <td class="admincls1" style="text-align:center"><%#Eval("order")%></td>
                        </tr>
                    </AlternatingItemTemplate>
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
