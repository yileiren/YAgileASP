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
<body style="margin:0px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
        <ContentTemplate>
        <div class="easyui-panel" style="width:800px;height:400px;padding:5px;">
            <div class="easyui-layout"  style="width: 100%;height: 100%">
            <div region="west" border="false" style="width:200px;padding:0px;">
                <div id="p" class="easyui-panel" title="菜单分组" fit="true" style="overflow-x:hidden">
                </div>
            </div>
            <div region="center" title="Main Title">
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    
    
    </form>
</body>
</html>
