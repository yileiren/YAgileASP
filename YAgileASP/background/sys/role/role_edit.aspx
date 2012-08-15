<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="role_edit.aspx.cs" Inherits="YAgileASP.background.sys.role.role_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑角色</title>

    <link href="../../../js/jquery-easyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/jquery-easyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/table.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../js/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="../../../js/jquery-easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../js/jquery-easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../../js/YWindows.js"></script>

    <script type="text/javascript" language="javascript">
        //表单验证方法
        function checkForms()
        {
            if (!$("#txtRoleName").validatebox("isValid"))
            {
                return false;
            }

            if ($("#txtRoleExplain").val().length > 50)
            {
                alert("说明文字不能超过50个！");
                return false;
            }

            return true;
        }
    </script>
</head>
<body style="margin:0px;background-color:#EEF5FD">
    <form id="form1" runat="server">
    <input type="hidden" id="roleId" name="roleId" runat="server" />
    <div style="width:500px;margin:0px auto;padding:5px">
    <table class="admintable" style="width:100%;">
        <tr style="height:30px">
            <th class="adminth_s2" style="width:120px;text-align:right">名称：</th>
            <td bgcolor="#FFFFFF"><input type="text" id="txtRoleName" name="txtRoleName" class="easyui-validatebox" required="true" maxlength="20" runat="server" style="width:200px" /></td>
        </tr>
        <tr style="height:30px">
            <th class="adminth_s2" style="width:120px;text-align:right">说明：</th>
            <td bgcolor="#FFFFFF">
                <textarea id="txtRoleExplain" name="txtRoleExplain" runat="server" rows="5" cols="10" style="width:200px;height:50px"></textarea>
            </td>
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
