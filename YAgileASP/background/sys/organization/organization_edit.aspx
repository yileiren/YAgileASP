<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="organization_edit.aspx.cs" Inherits="YAgileASP.background.sys.organization.organization_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑组织机构</title>
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

    <script type="text/javascript" type="text/javascript">
        //表单验证方法
        function checkForms()
        {
            if (!$("#txtOrgName").validatebox("isValid"))
            {
                return false;
            }

            return true;
        }
    </script>
</head>
<body style="margin:0px;background-color:#EEF5FD">
    <form id="form1" runat="server">
    <input type="hidden" id="hidOrgId" name="hidOrgId" runat="server" />
    <input type="hidden" id="hidParentId" name="hidParentId" runat="server" />
    <div style="width:500px;margin:0px auto;padding:5px">
    <table class="admintable" style="width:100%;">
        <tr style="height:30px">
            <th class="adminth_s2" style="width:120px;text-align:right">名称：</th>
            <td bgcolor="#FFFFFF"><input type="text" id="txtOrgName" name="txtOrgName" class="easyui-validatebox" required="true" maxlength="50" runat="server" style="width:200px" /></td>
        </tr>
        <tr style="height:30px">
            <th class="adminth_s2" style="width:120px;text-align:right">序号：</th>
            <td bgcolor="#FFFFFF"><input type="text" id="txtOrgOrder" name="txtOrgOrder" class="easyui-numberbox" min="0" max="50000" precision="0" value="0" runat="server" style="width:200px" /></td>
        </tr>
    </table>
    <div style="width:170px;margin-left:auto;margin-top:5px;margin-right:5px">
            <a id="A1" href="#" class="easyui-linkbutton" iconCls="icon-save" runat="server" onclick="javascript:return checkForms();" onserverclick="butSave_Click" >保存</a>
            <a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:window.parent.closePopupsWindow('#popups')">取消</a>
        </div>
    </div>
    </form>
</body>
</html>
