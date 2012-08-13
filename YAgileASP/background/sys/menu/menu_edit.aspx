<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu_edit.aspx.cs" Inherits="YAgileASP.background.sys.menu.menu_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新增菜单分组</title>

    <link href="../../../js/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/jquery-easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/table.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../js/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="../../../js/jquery-easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../js/jquery-easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../../js/YWindows.js"></script>
</head>
<body style="margin:0px;background-color:#EEF5FD">
    <form id="form1" runat="server">
    <div style="width:450px;margin:0px auto;padding:5px">
    <table class="admintable" style="width:100%;">
        <tr style="height:30px">
            <th class="adminth_s2" style="width:120px;text-align:right">名称：</th>
            <td bgcolor="#FFFFFF"><input type="text" id="txtMenuName" name="txtMenuName" style="width:300px" /></td>
        </tr>
        <tr style="height:30px">
            <th class="adminth_s2" style="width:120px;text-align:right">页面URL：</th>
            <td bgcolor="#FFFFFF"><input type="text" id="txtMenuURL" name="txtMenuURL" style="width:300px" /></td>
        </tr>
        <tr style="height:30px">
            <th class="adminth_s2" style="width:120px;text-align:right">菜单图标：</th>
            <td bgcolor="#FFFFFF"><input type="text" id="txtMenuICON" name="txtMenuICON" style="width:300px" /></td>
        </tr>
        <tr style="height:30px">
            <th class="adminth_s2" style="width:120px;text-align:right">桌面图标：</th>
            <td bgcolor="#FFFFFF"><input type="text" id="txtMenuDesktopICON" name="txtMenuDesktopICON" style="width:300px" /></td>
        </tr>
        <tr style="height:30px">
            <th class="adminth_s2" style="width:120px;text-align:right">序号：</th>
            <td bgcolor="#FFFFFF"><input type="text" id="txtMenuOrder" name="txtMenuOrder" style="width:300px" /></td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
