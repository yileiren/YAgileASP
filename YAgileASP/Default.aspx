<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="YAgileASP._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>异类人敏捷开发平台</title>
    <script src="js/jquery/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function ()
        {
            location.href = "background/sys/login.aspx";
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a href="background/sys/login.aspx">登陆</a>
    </div>
    </form>
</body>
</html>
