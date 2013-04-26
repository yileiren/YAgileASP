<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu_edit.aspx.cs" Inherits="YAgileASP.background.sys.menu.menu_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新增菜单分组</title>
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
        //表单验证方法
        function checkForms()
        {
            if ($("#txtMenuName").validatebox("isValid"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    </script>
</head>
<body class="easyui-layout">
<div id="center" region="center" style="padding:3px;background-color:#EEF5FD">
    <form id="form1" runat="server">
    <input type="hidden" id="hidMenuId" name="hidMenuId" runat="server" />
    <input type="hidden" id="hidParentId" name="hidParentId" runat="server" />
    <div style="width:600px;margin:0px auto;padding:0px">
        <table class="admintable" style="width:100%;">
            <tr style="height:30px">
                <th class="adminth_s2" style="width:120px;text-align:right">名称：</th>
                <td bgcolor="#FFFFFF"><input type="text" id="txtMenuName" name="txtMenuName" class="easyui-validatebox" required="true" maxlength="20" runat="server" style="width:300px" /></td>
            </tr>
            <tr style="height:30px">
                <th class="adminth_s2" style="width:120px;text-align:right">页面URL：</th>
                <td bgcolor="#FFFFFF"><input type="text" id="txtMenuURL" name="txtMenuURL" maxlength="200" runat="server" style="width:300px" /></td>
            </tr>
            <tr style="height:30px">
                <th class="adminth_s2" style="width:120px;text-align:right">菜单图标：</th>
                <td bgcolor="#FFFFFF"><input type="text" id="txtMenuICON" name="txtMenuICON" maxlength="20" runat="server" style="width:300px" /></td>
            </tr>
            <tr style="height:30px">
                <th class="adminth_s2" style="width:120px;text-align:right">桌面图标：</th>
                <td bgcolor="#FFFFFF"><input type="text" id="txtMenuDesktopICON" name="txtMenuDesktopICON" maxlength="100" runat="server" style="width:300px" /></td>
            </tr>
            <tr style="height:30px">
                <th class="adminth_s2" style="width:120px;text-align:right">序号：</th>
                <td bgcolor="#FFFFFF"><input type="text" id="txtMenuOrder" name="txtMenuOrder" class="easyui-numberbox" min="0" max="50000" precision="0" value="0" runat="server" style="width:300px" /></td>
            </tr>
        </table>
        <div style="width:170px;margin-left:auto;margin-top:5px;margin-right:5px">
            <a id="A1" href="#" class="easyui-linkbutton" iconCls="icon-save" runat="server" onclick="javascript:if(!checkForms()){ return false; };" onserverclick="butSave_Click" >保存</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:window.parent.closePopupsWindow('#popups')">取消</a>
        </div>
    </div>
    </form>
   </div>
</body>
</html>
