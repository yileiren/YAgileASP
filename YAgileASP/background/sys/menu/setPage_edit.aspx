<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="setPage_edit.aspx.cs" Inherits="YAgileASP.background.sys.menu.setPage_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑菜单</title>
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

        function savePage()
        {
            if (!$("#txtFilePath").validatebox("isValid"))
            {
                return false;
            }

            return true;
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
    <input type="hidden" id="hidPageId" name="hidPageId" runat="server" value="" />
    <div region="north" border="true" style="height:28px;background-color:#EEF5FD">
        <div style="width:150px;margin-left:auto;margin-top:0px;margin-right:0px">
            <a href="#" class="easyui-linkbutton" iconCls="icon-save" plain="true" onclick="javascript:return savePage();" runat="server" onserverclick="butSave_Click">保存</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" plain="true" onclick="javascript:window.location.href='setPage_list.aspx?menuId=<%=menuId %>'">取消</a>
        </div>
    </div>
    <div id="center" region="center" style="padding:3px;background-color:#EEF5FD">
        <div style="background-color:#FDFC01;padding:0px;margin:0px;width:100%;height:38px;top:0px;left:0px;right:0px;">
            <div style="float:left;width:32px;height:37px;"><img src="../../images/warning.png" alt="" style="width:32px;height:32px;margin-top:5px" /></div>
            <div style="float:left;height:37px;margin-left:5px"><p>页面地址使用相对网站根目录的完整路径，例如“/background/sys/login.aspx”。</p></div>
        </div>
        <div style="width:100%">
            <table class="editTable" style="width:100%;">
                <tr>
                    <th style="width:120px;">路径：</th>
                    <td><input type="text" id="txtFilePath" name="txtFilePath" class="easyui-validatebox" required="true" maxlength="500" runat="server" style="width:350px" /></td>
                </tr>
                <tr>
                    <th style="width:120px;">说明：</th>
                    <td>
                        <input type="text" id="txtFileDetail" name="txtFileDetail" runat="server" maxlength="200" style="width:350px" />
                    </td>
                </tr>
            </table>
        </div>

    </div>
    </form>
</body>
</html>
