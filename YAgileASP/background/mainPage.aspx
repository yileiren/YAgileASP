<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainPage.aspx.cs" Inherits="YAgileASP.background.mainPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>异类人敏捷开发平台</title>

    <link href="../js/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../js/jquery-easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="../js/jquery-easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/jquery-easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../js/YWindows.js"></script>

    <script type="text/javascript">
        //退出系统处理方法。
        function logOut()
        {
            return confirm("确定要退出系统？");
        }

        //修改密码
        function changePassword()
        {
            popupsWindow("#popups", "修改密码", 300, 175, "sys/changePassword.aspx", "icon-key", true, true);
        }
    </script>
</head>
<body class="easyui-layout">
    <div region="north" border="false" style="height:50px;background-image:url('images/mainPage/title_01.gif')">
    <form id="title" runat="server">
        <span style="position:absolute;left:10px">
            <img src="images/mainPage/title_02.gif" alt="异类人敏捷开发平台" />
        </span>
        <span style="position:absolute;right:10px;bottom:0px">
            <a id="butChangePassword" href="#" class="easyui-linkbutton" plain="true" iconCls="icon-key" onclick="javascript:changePassword();">修改密码</a>
            <a id="logOut" href="#" class="easyui-linkbutton" plain="true" iconCls="icon-out" runat="server" onclick="javascript:return logOut();" onserverclick="logOut_onClick">退出系统</a>
        </span>
    </form>
    </div>
	<div region="west" split="true" title="菜单" iconCls="icon-menu" style="width:250px;padding:3px;background-color:#EEF5FD">
	    <div id="menu" class="easyui-accordion" fit="true" border="true" style="background-color:#EEF5FD">
            <asp:Repeater ID="menuGroup" runat="server" 
                onitemdatabound="menuGroup_ItemDataBound">
                <ItemTemplate>
                <div title="<%#Eval("NAME") %>" id="<%#Eval("ID") %>" iconCls="<%#Eval("ICON") %>" style="overflow:auto;padding:3px;overflow-x:hidden">
                    <asp:Repeater ID="menuButton" runat="server">
                        <ItemTemplate>
                        <a href="#" class="easyui-linkbutton" id="<%#Eval("ID") %>" iconCls="<%#Eval("ICON") %>" plain="true" iconCls="icon-cancel" style="width:100%"><%#Eval("NAME") %></a>            
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                </ItemTemplate>
            </asp:Repeater>
	    </div>
	</div>
	<div region="south" border="true" style="height:25px;background:#D9E5FD;padding:5px;">
	    <span style="position:absolute;right:0px;width:400px">
	        <span style="font-weight:bold">登陆用户：</span>
	        <span id="userName" runat="server"></span>
	        &nbsp;&nbsp;&nbsp;
	        <span style="font-weight:bold">登录名：</span>
	        <span id="logName" runat="server"></span>
	    </span>
	</div>
	<div id="center" region="center" title="首页" iconCls="icon-home" style="padding:3px;background-color:#EEF5FD">
	</div>

    <!--弹出窗口-->
    <div id="popups" class="easyui-dialog" closed="true" style="padding:0px;background-color:#EEF5FD">
        <div style="width:100%;height:100%">
	        <iframe id="popupsIframe" frameborder="0" style="width:100%;height:100%">
	        </iframe>
	    </div>
    </div>
</body>
</html>
