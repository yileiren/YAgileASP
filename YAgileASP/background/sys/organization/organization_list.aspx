<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="organization_list.aspx.cs" Inherits="YAgileASP.background.sys.organization.organization_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组织机构</title>
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
</head>
<body class="easyui-layout" style="margin:0px;background-color:#EEF5FD;">
    <div region="north" border="true" style="height:28px;background-color:#EEF5FD">
        <div style="width:280px;margin-left:auto;margin-top:0px;margin-right:0px">
            <a id="A2" href="#" class="easyui-linkbutton" iconCls="icon-menu" plain="true" onclick="javascript:menuSet();">访问菜单</a>
            <a id="A1" href="#" class="easyui-linkbutton" iconCls="icon-add" plain="true" onclick="javascript:addRole();">新增</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-edit" plain="true" onclick="javascript:editRole();">修改</a>
            <a id="A1" href="#" class="easyui-linkbutton" iconCls="icon-cancel" plain="true" onclick="javascript:return deleteRoles();" runat="server">删除</a>
        </div>
    </div>
    <div id="center" region="center" style="padding:3px;background-color:#EEF5FD"">
    <form id="form1" runat="server">
    <div>
    组织机构
    </div>
    </form>
    </div>
</body>
</html>
