/*!
 * \brief
 * 基于JQuery EasyUI的窗口封装，用来快速开发窗口程序，使用以下方法需要加入JQuery EasyUI开发包。
 * 作者：董帅 创建时间：2012-7-25 21:59:12
 * 版本：v0.1.0
 */


/*!
 * \brief
 * 弹出窗口。
 * 作者：董帅 创建时间：2012-7-25 22:57:20
 * 
 * 窗口代码示例：
 * <div id="dd" class="easyui-dialog" closed="true" style="padding:5px">
 * </div>
 * 
 * \param windowId 窗口id，使用string类型。
 * \param titleName 标题名称，使用string类型。
 * \param windowWidth 窗口宽度，单位px。
 * \param windowHeight 窗口高度，单位px。
 * \param pageUrl 要显示的页面URL，页面必须是本站内的页面。
 * \param windowIcon 窗口图标，使用string类型。
 * \param allowClose 允许关闭，bool类型。
 * \param isModal 是否是模态窗口，bool类型。
 */
function popupsWindow(windowId,titleName,windowWidth,windowHeight,pageUrl,windowIcon,allowClose,isModal)
{
    $(windowId).dialog({
        title: titleName,
        width: windowWidth,
        height: windowHeight,
        iconCls: windowIcon,
        closable: allowClose,
        modal: isModal,
        closed: false,
        cache: false,
        draggable: false,
        onClose: function ()
        {
            $("#popupsIframe").attr("src", "");
        }
    });
    $("#popupsIframe").attr("src", pageUrl);
}

/*!
 * \brief
 * 关闭弹出窗口。
 * 
 * \param windowId 要关闭的窗口id。
 */
function closePopupsWindow(windowId)
{
    $(windowId).dialog('close');
    //$("#popupsIframe").attr("src", "");
}
