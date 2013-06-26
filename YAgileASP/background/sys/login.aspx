﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="YAgileASP.background.sys.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>异类人敏捷开发平台</title>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="cache-control" ontent="no-cache" />  
    <meta http-equiv="expires" content="0" />  
    
    <link href="../../js/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../js/jquery-easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../js/md5.js"></script>
    <script type="text/javascript" src="../../js/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="../../js/jquery-easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../js/jquery-easyui/locale/easyui-lang-zh_CN.js"></script>

    <script language="javascript" type="text/javascript">
    <!--
        //使用MD5加密算法加密密码
        function changePassword() 
        {
            var oldPassword = $("#passUserPassword").val();
            var newPassword = hex_md5(oldPassword);
            $("#passUserPassword").val(newPassword);
        }

        //表单验证方法
        function checkForms()
        {
            if ($("#txtUserName").validatebox("isValid"))
            {

                changePassword();
                return true;
            }
            else 
            {

                return false;
            }
        }

        //键盘按键事件处理方法
        function enterPress()
        {
            if (window.event.keyCode == 13)
            {
                //如果按下的是回车，则出发登陆按钮单击事件
                $("#butLogin").click();
            }
        }

        $(document).ready(function ()
        {
            if (window.top.location != window.self.location)
            {
                window.top.location = "login.aspx";
            }

            $("#txtUserName").bind("keypress", function (e)
            {
                if (e.keyCode == 13 && $(this).val() != "")
                {
                    $("#passUserPassword").focus();
                }
            });

            $("#passUserPassword").bind("keypress", function (e)
            {
                if (e.keyCode == 13)
                {
                    if (checkForms())
                    {
                        __doPostBack('butLogin', '')
                    }
                }
            });
        });
    //-->
    </script>
</head>
<body style="padding:0px;margin:0px;background-color:#89BBDE">
    <form id="form1" runat="server">
    <div style="background-color:#4550B8;padding:0px;margin:0px;width:100%;height:30px;top:0px;left:0px;right:0px;bottom:0px">
    </div>

    <div style="width:100%;height:100%;margin-left:auto;margin-right:auto;margin-top:10%;margin-bottom:0px;float:left">
        <div style="width:640px;height:400px;margin-left:auto;margin-right:auto;background-image:url('../images/login/login.png')">
    
        <div style="width:640px;height:270px"></div>
        <div style="width:335px;height:40px;margin-left:auto;margin-right:5px;background-image:url('../images/login/userName.png')">
            <input type="text" id="txtUserName" name="txtUserName" runat="server" runat="server" maxlength="20" style="width:236px;height:26px;margin-left:92px;margin-top:6px;border-width:0px;font-size:18px" class="easyui-validatebox" required="true"/>
        </div>
        <div style="width:335px;height:40px;margin-top:5px;margin-left:auto;margin-right:5px;background-image:url('../images/login/userPassword.png')">
            <input type="password" id="passUserPassword" name="passUserPassword" maxlength="20" runat="server" runat="server" style="width:236px;height:26px;margin-left:92px;margin-top:6px;border-width:0px;font-size:18px" />
        </div>
        <div style="width:100px;height:40px;margin-top:5px;margin-left:auto;margin-right:5px">
            <a href="#" id="butLogin" name="butLogin" class="easyui-linkbutton" iconCls="icon-userlogin" runat="server" onclick="javascript:if(!checkForms()){ return false; };" onserverclick="butLogin_Click">登陆</a>
        </div>
        </div>
    </div>
    </form>
</body>
</html>
