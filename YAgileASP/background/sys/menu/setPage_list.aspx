﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="setPage_list.aspx.cs" Inherits="YAgileASP.background.sys.menu.setPage_list" %>

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
            <a href="#" class="easyui-linkbutton" iconCls="icon-edit" plain="true" onclick="javascript:editDataDictionary();">修改</a>
            <a id="A2" href="#" class="easyui-linkbutton" iconCls="icon-cancel" plain="true" onclick="javascript:return deleteDataDictionarys();" runat="server" >删除</a>
        </div>
    </div>
    <div id="center" region="center" style="padding:3px;background-color:#EEF5FD">
        <div style="background-color:#FDFC01;padding:0px;margin:0px;width:100%;height:38px;top:0px;left:0px;right:0px;">
            <div style="float:left;width:32px;height:37px;"><img src="../../images/warning.png" alt="" style="width:32px;height:32px;margin-top:5px" /></div>
            <div style="float:left;height:37px;margin-left:5px"><p>页面地址使用相对网站根目录的完整路径，例如“/background/sys/login.aspx”。</p></div>
        </div>
        <div style="width:100%">
            
        </div>

    </div>
    </form>
</body>
</html>
