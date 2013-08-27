<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chouseRole.aspx.cs" Inherits="YAgileASP.background.sys.organization.chouseRole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择角色</title>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="cache-control" ontent="no-cache" />  
    <meta http-equiv="expires" content="0" />  

    <link href="../../../js/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/jquery-easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/table.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../js/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="../../../js/jquery-easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../js/jquery-easyui/locale/easyui-lang-zh_CN.js"></script>
</head>
<body style="margin:0px;background-color:#EEF5FD">
    
    <form id="form1" runat="server">
    <div style="width:250px;height:415px;padding:3px;background-color:#EEF5FD"">
    <div class="easyui-panel" fit="true"  style="overflow-x:hidden;background-color:#EEF5FD">
    <input type="hidden" id="hidUserId" name="hidUserId" runat="server" />
    <div>
        <asp:Repeater ID="rolesRepeater" runat="server">
            <HeaderTemplate>
                <table class="listTable" style="width:100%">
                <tr class="tableHead">
                    <th style="width:30px">选择</th>
                    <th style="width:200px">名称</th>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="width:100%;height:30px">
                    <td class="admincls0" style="text-align:center">
                        <input type="checkbox" name="chkItem" "<%#Eval("choused").ToString() == "True" ? " checked=\"checked\" " : "" %>" value="<%#Eval("id") %>"/>
                    </td>
                    <td class="admincls0" style="text-align:center"><%#Eval("name") %></td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="width:100%;height:30px">
                    <td class="admincls1" style="text-align:center">
                        <input type="checkbox" name="chkItem" "<%#Eval("choused").ToString() == "True" ? " checked=\"checked\" " : "" %>" value="<%#Eval("id") %>"/>
                    </td>
                    <td class="admincls1" style="text-align:center"><%#Eval("name") %></td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </div>
    </div>
    <div style="width:250px;height:30px;padding:3px;background-color:#EEF5FD"">
        <div style="width:170px;margin-left:auto;margin-top:0px;margin-right:0px">
            <a id="A1" href="#" class="easyui-linkbutton" iconCls="icon-ok" runat="server" onserverclick="butChouse_Click" >选择</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:window.parent.closePopupsWindow('#popups')">取消</a>
        </div>
    </div>
    </form>
    
</body>
</html>
