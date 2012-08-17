<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu_list.aspx.cs" Inherits="YAgileASP.background.sys.menu.menu_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统菜单列表</title>
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
         * 新增分组。
         * 作者：董帅 创建时间：2012-8-13 17:20:49
         */
        function addGroup()
        {
            window.parent.popupsWindow("#popups", "新增菜单分组", 700, 230, "sys/menu/menu_edit.aspx?pageType=group", "icon-add", true, true);
        }

        /*!
         * \brief
         * 修改分组。
         * 作者：董帅 创建时间：2012-8-14 11:35:05
         */
        function editGroup()
        {
            //判断选中
            if ($("input:checked[type='checkbox'][name='chkGroup']").length != 1)
            {
                alert("请选中要编辑的分组，一次只能选择一个！");
                return;
            }

            //打开编辑页面
            window.parent.popupsWindow("#popups", "修改菜单分组", 700, 230, "sys/menu/menu_edit.aspx?pageType=group&id=" + $("input:checked[type='checkbox'][name='chkGroup']").eq(0).val(), "icon-edit", true, true);
        }

        /*!
         * \brief
         * 删除选中的分组。
         * 作者：董帅 创建时间：2012-8-14 14:19:19
         */
        function deleteGroups()
        {
            //判断选中
            if ($("input:checked[type='checkbox'][name='chkGroup']").length > 0)
            {
                return confirm("确认要删除选中的分组？删除分组将连同子菜单一并删除，并且无法恢复！");
            }
            else
            {
                alert("请选中要删除的分组！");
                return false;
            }
        }

        /*!
         * \brief
         * 新增菜单。
         * 作者：董帅 创建时间：2012-8-14 21:34:51
         */
        function addMenu()
        {
            window.parent.popupsWindow("#popups", "新增菜单", 700, 230, "sys/menu/menu_edit.aspx?pageType=item&parentId=" + $("#selectGroupId").val(), "icon-add", true, true);
        }

        /*!
         * \brief
         * 编辑菜单。
         * 作者：董帅 创建时间：2012-8-14 21:45:54
         */
        function editMenu()
        {
            //判断选中
            if ($("input:checked[type='checkbox'][name='chkItem']").length != 1)
            {
                alert("请选中要编辑的菜单，一次只能选择一个！");
                return;
            }

            //打开编辑页面
            window.parent.popupsWindow("#popups", "修改菜单", 700, 230, "sys/menu/menu_edit.aspx?pageType=item&id=" + $("input:checked[type='checkbox'][name='chkItem']").eq(0).val() + "&parentId=" + $("#selectGroupId").val(), "icon-edit", true, true);
        }

        /*!
         * \brief
         * 删除菜单。
         * 作者：董帅 创建时间：2012-8-14 22:10:03
         */
        function deleteItem()
        {
            //判断选中
            if ($("input:checked[type='checkbox'][name='chkItem']").length > 0)
            {
                return confirm("确认要删除选中的菜单？");
            }
            else
            {
                alert("请选中要删除的菜单！");
                return false;
            }
        }
    </script>
</head>
<body class="easyui-layout">
    <div id="center" region="center" style="padding:3px;background-color:#EEF5FD">
    <form id="form1" runat="server">
    <input type="hidden" id="selectGroupId" name="selectGroupId" value="" runat="server" />
    <div style="width:1024px;height:500px;background-color:#EEF5FD;margin:0px auto">
        <div class="easyui-layout"  style="width: 100%;height: 100%;background-color:#EEF5FD">
        <div region="west" border="false" style="width:250px;padding:0px;background-color:#EEF5FD">
            <div class="easyui-panel" title="菜单分组" fit="true" tools="#groutsButtons" style="overflow-x:hidden;background-color:#FFFFFF">
                <asp:Repeater ID="menuGroups" runat="server">
                <HeaderTemplate>
                    <table class="admintable" style="width:100%">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="width:100%;height:30px">
                        <td class="admincls0" style="text-align:center;width:30px"><input type="checkbox" value="<%#Eval("ID") %>" name="chkGroup" /></td>
                        <td class="admincls0" style="width:220px">
                            <a href="#" class="easyui-linkbutton" id="<%#Eval("ID") %>" iconCls="<%#Eval("ICON") %>" plain="true" style="width:215px" onclick="javascript:href='menu_list.aspx?id=<%#Eval("ID") %>'"><%#Eval("NAME").ToString().Length > 16 ? Eval("NAME").ToString().Substring(0, 15) + "..." : Eval("NAME")%></a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
                </asp:Repeater>
	        </div>
        </div>
        <div region="center" title="<%=groupTitle %>" iconCls="<%=groupIcon %>" tools="#menusButtons" style="background-color:#FFFFFF">
            <asp:Repeater ID="childs" runat="server">
                <HeaderTemplate>
                    <table class="admintable" style="width:100%">
                    <tr style="width:100%;height:30px">
                        <th class="adminth" style="width:40px">选择</th>
                        <th class="adminth" style="width:200px">名称</th>
                        <th class="adminth" style="width:300px">页面URL</th>
                        <th class="adminth" style="width:100px">菜单图标</th>
                        <th class="adminth" style="width:100px">桌面图标</th>
                        <th class="adminth" style="width:40px">序号</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="width:100%;height:30px">
                        <td class="admincls0" style="text-align:center">
                            <input type="checkbox" name="chkItem" value="<%#Eval("id") %>" />
                        </td>
                        <td class="admincls0" style="text-align:center"><%#Eval("name") %></td>
                        <td class="admincls0"><%#Eval("url") %></td>
                        <td class="admincls0" style="text-align:center"><%#Eval("icon") %></td>
                        <td class="admincls0" style="text-align:center"><%#Eval("desktopIcon")%></td>
                        <td class="admincls0" style="text-align:center"><%#Eval("order")%></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="width:100%;height:30px">
                        <td class="admincls1" style="text-align:center">
                            <input type="checkbox" name="chkItem" value="<%#Eval("id") %>" />
                        </td>
                        <td class="admincls1" style="text-align:center"><%#Eval("name") %></td>
                        <td class="admincls1"><%#Eval("url") %></td>
                        <td class="admincls1" style="text-align:center"><%#Eval("icon") %></td>
                        <td class="admincls1" style="text-align:center"><%#Eval("desktopIcon")%></td>
                        <td class="admincls1" style="text-align:center"><%#Eval("order")%></td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        </div>
    </div>
    <div id="groutsButtons">
		<a href="#" class="icon-add" onclick="javascript:addGroup();"></a>
		<a href="#" class="icon-edit" onclick="javascript:editGroup();"></a>
		<a href="#" class="icon-cancel" onclick="javascript:return deleteGroups();" runat="server" onserverclick="butDeleteGroup_Click"></a>
	</div>
    <div id="menusButtons">
		<a href="#" class="icon-add" onclick="javascript:addMenu();"></a>
		<a href="#" class="icon-edit" onclick="javascript:editMenu();"></a>
		<a href="#" class="icon-cancel" onclick="javascript:return deleteItem();" runat="server" onserverclick="butDeleteItem_Click"></a>
	</div>
    </form>
    </div>
</body>
</html>
