<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changePassword.aspx.cs" Inherits="YAgileASP.background.sys.changePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改密码</title>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="cache-control" ontent="no-cache">  
    <meta http-equiv="expires" content="0">  

    <link href="../../js/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../js/jquery-easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../js/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="../../js/jquery-easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../js/jquery-easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../js/YWindows.js"></script>
    <script type="text/javascript" src="../../js/md5.js"></script>

    <script type="text/javascript">
        //修改密码
        function changePsw()
        {
            var new1 = $("#pswNewPsw1").val();
            var new2 = $("#pswNewPsw2").val();
            if (new1 == new2)
            {
                var oldPsw = $("#pswOldPsw").val();
                $("#pswOldPsw").val(hex_md5(oldPsw));
                $("#pswNewPsw1").val(hex_md5(new1));
                $("#pswNewPsw2").val(hex_md5(new2));
            }
            else
            {
                alert("新密码与确认密码不一致！");
                return false;
            }
            
            return true;
        }
    </script>
</head>
<body style="margin:0px;background-color:#EEF5FD">
    <form id="form1" runat="server">
	<div id="p" class="easyui-panel" noheader="true" fit="true" style="margin-left:auto;margin-right:auto;padding:3px">
	
        <div style="width:250px;margin-left:auto;margin-right:auto">
            <table style="border-collapse:collapse">
	            <tr><td style="width:70px;text-align:right">原密码：</td><td><input type="password" id="pswOldPsw" name="pswOldPsw" runat="server" style="width:150px" /></td></tr>
	            <tr><td style="width:70px;text-align:right">新密码：</td><td><input type="password" id="pswNewPsw1" name="pswNewPsw1" runat="server" style="width:150px" /></td></tr>
	            <tr><td style="width:70px;text-align:right">确认密码：</td><td><input type="password" id="pswNewPsw2" name="pswNewPsw2" runat="server" style="width:150px" /></td></tr>
	        </table>
        </div>
        <div style="width:170px;margin-left:auto;margin-top:5px;margin-right:5px">
            <a href="#" class="easyui-linkbutton" iconCls="icon-ok" onclick="javascript:return changePsw();"  runat="server" onserverclick="butChange_onClick">修改</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:window.parent.closePopupsWindow('#popups')">取消</a>
        </div>
    </div>
    </form>
</body>
</html>
