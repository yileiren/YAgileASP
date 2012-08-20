<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chouseMenu.aspx.cs" Inherits="YAgileASP.background.sys.role.chouseMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择菜单</title>
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
<%--    <script type="text/javascript" language="javascript">
        /*!
         * \brief
         * 选择菜单。
         * 作者：董帅 创建时间：2012-8-20 22:56:25
         */
        function chouseMenu()
        {
            alert($("#hidRoleId").val());
        }
    </script>--%>
</head>
<body class="easyui-layout" style="margin:0px;background-color:#EEF5FD">
    <div id="center" region="center" style="padding:3px;background-color:#EEF5FD"">
    <form id="form1" runat="server">
    <input type="hidden" id="hidRoleId" name="hidRoleId" runat="server" />
    <div>
        <ul id="tt1" class="easyui-tree" >
        <asp:Repeater ID="menuGroup" runat="server" 
                onitemdatabound="menuGroup_ItemDataBound">
            <ItemTemplate>
            <li><span><input type="checkbox" name="chkItem" "<%#Eval("choused").ToString() == "True" ? " checked=\"checked\" " : "" %>" value="<%#Eval("id") %>"/><%#Eval("NAME") %></span>
                <ul >
                <asp:Repeater ID="menuButton" runat="server">
                    <ItemTemplate>
                    <li><span><input type="checkbox" name="chkItem" "<%#Eval("choused").ToString() == "True" ? " checked=\"checked\" " : "" %>" value="<%#Eval("id") %>" /><%#Eval("NAME") %></span></li>
                    </ItemTemplate>
                </asp:Repeater>
                </ul>
            </li>
            </ItemTemplate>
        </asp:Repeater>
        </ul>
    </div>
    </div>
    <div id="south" region="south" style="height:35px;padding:3px;background-color:#EEF5FD"">
        
        <div style="width:170px;margin-left:auto;margin-top:0px;margin-right:0px">
            <a id="A1" href="#" class="easyui-linkbutton" iconCls="icon-ok" runat="server" onserverclick="butChouse_Click" >选择</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:window.parent.closePopupsWindow('#popups')">取消</a>
        </div>
        </form>
    </div>
</body>
</html>
