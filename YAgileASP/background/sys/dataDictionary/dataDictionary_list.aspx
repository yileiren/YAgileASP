<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dataDictionary_list.aspx.cs" Inherits="YAgileASP.background.sys.dataDictionary.dataDictionary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据字典</title>
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
        /*!
        * \brief
        * 新增字典项。
        * 作者：董帅 创建时间：2012-8-28 21:19:48
        */
        function addDataDictionary()
        {
            window.parent.popupsWindow("#popups", "新增字典项", 600, 200, "sys/dataDictionary/dataDictionary_edit.aspx?parentId=" + $("#hidParentId").val(), "icon-add", true, true);
        }

        /*!
        * \brief 
        * 返回上级字典.
        * 作者：董帅 创建时间：2012-8-28 21:51:59
        */

        function returnParent()
        {
            window.parent.menuButtonOnClick('数据字典', 'icon-dictionary', 'sys/dataDictionary/dataDictionary_list.aspx?parentId=' + $("#hidReturnId").val());
        }
    </script>
</head>
<body class="easyui-layout" style="margin:0px;background-color:#EEF5FD;">
    <div region="north" border="true" style="height:28px;background-color:#EEF5FD">
        <div style="width:380px;margin-left:auto;margin-top:0px;margin-right:0px">
            <a href="#" class="easyui-linkbutton" iconCls="icon-add" plain="true" onclick="javascript:addDataDictionary();">新增</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-edit" plain="true" onclick="javascript:editItem();">修改</a>
            <a id="A1" href="#" class="easyui-linkbutton" iconCls="icon-cancel" plain="true" onclick="javascript:return deleteItem();" runat="server">删除</a>
        </div>
    </div>
    <div id="center" region="center" style="padding:3px;background-color:#EEF5FD"">
    <form id="form1" runat="server">
    <input type="hidden" id="hidParentId" name="hidParentId" runat="server" />
    <input type="hidden" id="hidReturnId" name="hidReturnId" runat="server" />
    <div style="width:600px;height:28px;margin:0px auto">
        <a id="returnButton" href="#" class="easyui-linkbutton" iconCls="icon-back" plain="true" onclick="javascript:returnParent();" runat="server">返回</a>
        <span>当前字典：</span>
        <span id="spanParentName" runat="server" style="font-size:16px;font-weight:bold"></span>
    </div>
    <div style="width:600px;margin:0px auto">
        <table class="admintable" style="width:100%">
            <tr style="width:100%;height:30px">
                <th class="adminth" style="width:30px">选择</th>
                <th class="adminth" style="width:auto">名称</th>
                <th class="adminth" style="width:50px">数值</th>
                <th class="adminth" style="width:150px">代码</th>
                <th class="adminth" style="width:30px">序号</th>
            </tr>
        <asp:Repeater ID="dicList" runat="server">
        <ItemTemplate>
            <tr style="width:100%;height:30px">
                <td class="admincls0" style="text-align:center;"><input type="checkbox" value="<%#Eval("ID") %>" name="chkOrg" /></td>
                <td class="admincls0" style="width:auto">
                    <a href="dataDictionary_list.aspx?parentId=<%#Eval("ID") %>" class="easyui-linkbutton" id="<%#Eval("ID") %>" plain="true" style="width:317px" ><%#Eval("NAME")%></a>
                </td>
                <td class="admincls0" style="text-align:center">
                    <%#Eval("value")%>
                </td>
                <td class="admincls0" style="text-align:center">
                    <%#Eval("code")%>
                </td>
                <td class="admincls0" style="text-align:center">
                    <%#Eval("order") %>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="width:100%;height:30px">
                <td class="admincls1" style="text-align:center;"><input type="checkbox" value="<%#Eval("ID") %>" name="chkOrg" /></td>
                <td class="admincls1" style="width:auto">
                    <a href="dataDictionary_list.aspx?parentId=<%#Eval("ID") %>" class="easyui-linkbutton" id="<%#Eval("ID") %>" plain="true" style="width:317px" ><%#Eval("NAME")%></a>
                </td>
                <td class="admincls1" style="text-align:center">
                    <%#Eval("value")%>
                </td>
                <td class="admincls1" style="text-align:center">
                    <%#Eval("code")%>
                </td>
                <td class="admincls1" style="text-align:center">
                    <%#Eval("order") %>
                </td>
            </tr>
        </AlternatingItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </form>
    </div>
</body>
</html>
