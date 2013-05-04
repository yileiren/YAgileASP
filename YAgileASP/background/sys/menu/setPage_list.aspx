<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="setPage_list.aspx.cs" Inherits="YAgileASP.background.sys.menu.setPage_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置菜单</title>
    <style type="text/css">
        html,body{ height:100%;}
    </style>

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

    <script language="javascript" type="text/javascript">
        /*!
        * \brief
        * 修改字典。
        * 作者：董帅 创建时间：2012-8-28 21:59:45
        */
        function editPage()
        {
            //判断选中
            if ($("input:checked[type='checkbox'][name='chkPage']").length != 1)
            {
                alert("请选中要编辑的页面，一次只能选择一个！");
                return;
            }

            window.location.href = "setPage_edit.aspx?menuId=" + $("#hidMenuId").val() + "&&pageId="  + $("input:checked[type='checkbox'][name='chkPage']").val();
        }

        /*!
        * \brief
        * 动态调整layout。
        */
        $(function ()
        {
            $(window).resize(function ()
            {
                $('form#form1').height($(window).height());
                $('form#form1').width($(window).width());
                $('form#form1').height($(window).height());
                $('form#form1').layout();
            });
        });
    </script>
</head>
<body style="width:100%;margin:0px;background-color:#EEF5FD;">
    <form id="form1" runat="server" class="easyui-layout" flt="true" style="width:100%;height:100%;margin:0px;background-color:#EEF5FD;">
    <input type="hidden" id="hidMenuId" name="hidMenuId" runat="server" value="" />
    <div region="north" border="true" style="height:28px;background-color:#EEF5FD">
        <div style="width:200px;margin-left:auto;margin-top:0px;margin-right:0px">
            <a href="#" class="easyui-linkbutton" iconCls="icon-add" plain="true" onclick="javascript:window.location.href='setPage_edit.aspx?menuId=<%=menuId %>';">新增</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-edit" plain="true" onclick="javascript:editPage();">修改</a>
            <a id="A2" href="#" class="easyui-linkbutton" iconCls="icon-cancel" plain="true" onclick="javascript:return deleteDataDictionarys();" runat="server" >删除</a>
        </div>
    </div>
    <div id="center" region="center" style="padding:3px;background-color:#EEF5FD">
        <div style="width:100%">
            <table class="listTable" style="width:100%">
                <tr class="tableHead"">
                    <th style="width:30px">选择</th>
                    <th style="width:300px">路径</th>
                    <th style="width:300px">说明</th>
                </tr>
            <asp:Repeater ID="pageList" runat="server">
            <ItemTemplate>
                <tr class="tableBody1">
                    <td style="text-align:center;"><input type="checkbox" value="<%#Eval("ID") %>" name="chkPage" /></td>
                    <td>
                        <%#Eval("filePath")%>
                    </td>
                    <td>
                        <%#Eval("detail")%>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="tableBody2">
                    <td style="text-align:center;"><input type="checkbox" value="<%#Eval("ID") %>" name="chkPage" /></td>
                    <td>
                        <%#Eval("filePath")%>
                    </td>
                    <td>
                        <%#Eval("detail")%>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            </asp:Repeater>
            </table>
        </div>

    </div>
    </form>
</body>
</html>
