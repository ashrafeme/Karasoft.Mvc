using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Karasoft.Mvc.Extension
{
    public static class UmmalQuraExtension
    {
        public static MvcHtmlString UmmalQuraWithTime(this HtmlHelper htmlHelper, string name, object valuedata = null, object htmlAttributes = null)
        {
            var cal = new System.Globalization.UmAlQuraCalendar();
            UmmalQuraHtmlHelper oUQHH = new UmmalQuraHtmlHelper();
            var selectedmonth = string.Empty;
            var selectedday = string.Empty;
            var selectedyear = string.Empty;
            var value = valuedata as DateTime?;
            if (value.HasValue)
            {
                selectedday = cal.GetDayOfMonth(value.Value).ToString();
                selectedmonth = cal.GetMonth(value.Value).ToString();
                selectedyear = cal.GetYear(value.Value).ToString();
            }
            var ha = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            TagBuilder tag = new TagBuilder("table");
            tag.InnerHtml = string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>",
              oUQHH.UmmalQuraDays(name, selectedday, ha).ToString(),
                oUQHH.UmmalQuraMonths(name, selectedmonth, ha).ToString(),
                oUQHH.UmmalQuraYears(name, selectedyear, cal, ha));

            return new MvcHtmlString(tag.ToString());
        }
    }
}

