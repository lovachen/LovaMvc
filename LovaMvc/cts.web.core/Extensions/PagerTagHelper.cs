using cts.web.core.Datatable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cts.web.core
{
    /// <summary>
    /// 后台管理分页控件
    /// </summary>
    public class PagerTagHelper : TagHelper
    {
        private const string PageValueAttributeName = "page-value";

        /// <summary>
        /// 分页参数
        /// </summary>
        [HtmlAttributeName(PageValueAttributeName)]
        public Pagination Paging { get; set; }

        /// <summary>
        /// ajax方式？
        /// </summary>
        [HtmlAttributeName("page-ajax")]
        public bool Ajax { get; set; }

        /// <summary>
        /// 数据容器
        /// </summary>
        [HtmlAttributeName("page-target")]
        public string DataBox { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //分页model为空
            if (Paging == null)
                return;
            //没有数据
            if (Paging.TotalCount <= 0)
                return;
            string routeName = Paging.RouteName;
            string controller = "", action = "";
            if (String.IsNullOrEmpty(routeName))
                if (ViewContext.ActionDescriptor.AttributeRouteInfo != null)
                    if (!String.IsNullOrEmpty(ViewContext.ActionDescriptor.AttributeRouteInfo.Name))
                        routeName = ViewContext.ActionDescriptor.AttributeRouteInfo.Name;

            if (String.IsNullOrEmpty(routeName))
            {
                controller = ViewContext.ActionDescriptor.RouteValues["controller"];
                action = ViewContext.ActionDescriptor.RouteValues["action"];
            }

            IUrlHelper urlHelper = new UrlHelperFactory().GetUrlHelper(ViewContext);
            //连接地址
            string urlString = null;
            if (!String.IsNullOrEmpty(routeName))
                urlString = urlHelper.RouteUrl(routeName, Paging.RouteArg);
            else
                urlString = urlHelper.Action(action, controller, Paging.RouteArg);
            urlString = urlString.Any(o => o == '?') ? urlString + "&page={0}&size={1}" : urlString + "?page={0}&size={1}";
            StringBuilder sb = new StringBuilder();

            //默认显示最多7个连续按钮
            int display = 7;

            int minDisplay = 1;
            int maxDisplay = 7;
            if (Paging.TotalPages > display)
            {
                if (Paging.PageIndex + display / 2 >= Paging.TotalPages)
                {
                    maxDisplay = Paging.TotalPages;
                    minDisplay = Paging.TotalPages - display;
                }
                else if (Paging.PageIndex > display / 2)
                {
                    minDisplay = Paging.PageIndex - display / 2;
                    maxDisplay = Paging.PageIndex + display / 2;
                }
            }
            else
            {
                minDisplay = 1;
                maxDisplay = Paging.TotalPages;
            }

            string href = Ajax ? "href=\"javascript:\" data-href" : "href";

            sb.Append("<nav><ul class=\"pagination\">");

            sb.AppendFormat($"<li class=\"disabled\"><a>当前{Paging.PageIndex}/{Paging.TotalPages}页 共{Paging.TotalCount}条</a></li>");

            #region 上一页

            if (Paging.HasPreviousPage)
            {
                sb.AppendFormat($"<li><a {href}=\"{String.Format(urlString, Paging.PageIndex - 1, Paging.PageSize)}\">上一页</a></li>");
            }
            else
            {
                sb.Append("<li class=\"disabled\"><a href=\"javascript: \">上一页</a></li>");
            }

            #endregion

            if (Paging.PageIndex > display / 2 + 2)
            {
                sb.Append($"<li><a {href} = \"{String.Format(urlString, 1, Paging.PageSize)}\" >1</a></li>");
                sb.Append("<li><a href=\"javascript:\">...</a></li>");
            }
            //else if (Paging.PageIndex == display / 2 + 2)
            //{
            //    sb.Append($"<li><a {href}=\"{String.Format(urlString, 1, Paging.PageSize)}\">1</a></li>");
            //}
            for (int i = minDisplay; i <= maxDisplay; i++)
            {
                if (i == Paging.PageIndex)
                {
                    sb.Append($"<li class=\"active\"><a href = \"javascript:\" >{i}</a></li>");
                }
                else
                {
                    sb.Append($"<li><a {href} = \"{String.Format(urlString, i, Paging.PageSize)}\">{i}</a></li>");
                }
            }
            if (maxDisplay + 1 < Paging.TotalPages)
            {
                sb.Append("<li><a href=\"javascript:\" >...</ a ></li>");
                sb.AppendFormat($"<li><a {href} = \"{String.Format(urlString, Paging.TotalPages, Paging.PageSize)}\" >{Paging.TotalPages}</a></li>");
            }
            else if (maxDisplay + 1 == Paging.TotalPages)
            {
                sb.Append($"<li><a {href}= \"{String.Format(urlString, Paging.TotalPages, Paging.PageSize)}\" >{Paging.TotalPages}</a></li>");
            }


            #region 下一页

            if (Paging.HasNextPage)
            {
                sb.Append($"<li class=\"next\"><a {href}=\"{String.Format(urlString, Paging.PageIndex + 1, Paging.PageSize)}\">下一页</a></li>");
            }
            else
            {
                sb.Append("<li class=\"disabled\"><a href=\"javascript:\">下一页</a></li>");
            }
            #endregion

            sb.Append("<li>&nbsp;&nbsp;</li>");

            #region 每页条数选择按钮
            sb.Append("<li> ");
            sb.Append("<div class=\"btn-group\">");
            sb.Append("<button type=\"button\" class=\"btn btn-white dropdown-toggle btn-sm\" style=\"padding:6px 10px;\" data-toggle=\"dropdown\">");
            sb.AppendFormat("每页{0}条 <span class=\"ace-icon fa fa-caret-down icon-on-right\"></span>", Paging.PageSize);
            sb.Append("</button>");
            sb.Append("<ul class=\"dropdown-menu\" role=\"menu\">");
            sb.Append($"<li><a {href}=\"{String.Format(urlString, Paging.PageIndex, 20)}\">每页20条</a></li>");
            sb.Append($"<li><a {href}=\"{String.Format(urlString, Paging.PageIndex, 30)}\">每页30条</a></li>");
            sb.Append($"<li><a {href}=\"{String.Format(urlString, Paging.PageIndex, 50)}\">每页50条</a></li>");
            sb.Append($"<li><a {href}=\"{String.Format(urlString, Paging.PageIndex, 100)}\">每页100条</a></li>");
            sb.Append("</ul>");

            sb.Append("</div>");
            sb.Append("</li>");
            #endregion

            sb.Append("</ul></nav>");

            #region jquery操作

            if (Ajax)
            {
                sb.Append("<script>");
                sb.Append("$(function(){" +
                    "$('.pagination a[data-href]').click(function(e){" +
                        "e.preventDefault();" +
                        "layer.load(2);" +
                        "var href = $(this).data('href');" +
                        "$.ajax({" +
                        "url:href," +
                        "type:'POST'," +
                        "success:function(html){" +
                        "$('" + DataBox + "').html(html);" +
                        "$('html').scrollTop(0)}," +
                        "error:function(){" +
                            "layer.msg('请求数据错误');" +
                        "},complete:function(){" +
                           "layer.closeAll();" +
                        "}" +
                        "});" +
                    "});" +
                    "})");
                sb.Append("</script>");

            }

            #endregion

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
