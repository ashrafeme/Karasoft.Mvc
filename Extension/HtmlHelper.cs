using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Karasoft.Mvc.Extension
{
    public static class HtmlHelperExtension
    {

        public static IHtmlString DropDownListWithLabelFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string label, IEnumerable<SelectListItem> items, string blankOption, object htmlAttributes = null)
        {
            var l = new TagBuilder("label");
            var br = new TagBuilder("br");

            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var mergedAttributes = helper.GetUnobtrusiveValidationAttributes(ExpressionHelper.GetExpressionText(expression), metadata);

            if (htmlAttributes != null)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(htmlAttributes))
                {
                    object value = descriptor.GetValue(htmlAttributes);
                    mergedAttributes.Add(descriptor.Name, value);
                }
            }

            l.InnerHtml = label + br.ToString(TagRenderMode.SelfClosing) + helper.DropDownListFor(expression, items, blankOption, mergedAttributes);
            return MvcHtmlString.Create(l.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString UmmalQura(this HtmlHelper htmlHelper, string name, object valuedata = null, object htmlAttributes = null)
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
            tag.InnerHtml = string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>",
              oUQHH.UmmalQuraDays(name, selectedday, ha).ToString(),
                oUQHH.UmmalQuraMonths(name, selectedmonth, ha).ToString(),
                oUQHH.UmmalQuraYears(name, selectedyear, cal, ha));

            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString UmmalQuraFor<TModel, TValue>(this System.Web.Mvc.HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression,int MinYear,int MaxYear, object htmlAttributes = null)
        {
            var cal = new System.Globalization.UmAlQuraCalendar();

           // ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
           
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            UmmalQuraHtmlHelper oUQHH = new UmmalQuraHtmlHelper();
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var ha = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var mergedAttributes = html.GetUnobtrusiveValidationAttributes(ExpressionHelper.GetExpressionText(expression), metadata);
            if (htmlAttributes != null)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(htmlAttributes))
                {
                    object valueff = descriptor.GetValue(htmlAttributes);
                    mergedAttributes.Add(descriptor.Name, valueff);
                }
            }
            var value = metadata.Model as DateTime?;

            var selectedmonth = string.Empty;
            var selectedday = string.Empty;
            var selectedyear = string.Empty;
            if (value.HasValue)
            {
                if (value.Value > cal.MinSupportedDateTime && value.Value < cal.MaxSupportedDateTime)
                {
                    selectedday = cal.GetDayOfMonth(value.Value).ToString();

                    selectedmonth = cal.GetMonth(value.Value).ToString();
                    selectedyear = cal.GetYear(value.Value).ToString();
                }
            }
            
            TagBuilder tag = new TagBuilder("table");
            tag.InnerHtml = string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>",
               oUQHH.UmmalQuraDays(fieldId, selectedday, ha).ToString(),
                oUQHH.UmmalQuraMonths(fieldId, selectedmonth, ha).ToString(),
                oUQHH.UmmalQuraYears(fieldId, selectedyear, cal, MinYear, MaxYear, ha),
                html.HiddenFor(expression,mergedAttributes));

            return new MvcHtmlString(tag.ToString());

        }

        
        public static MvcHtmlString UmmalQuraFor<TModel, TValue>(this System.Web.Mvc.HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
             var cal = new System.Globalization.UmAlQuraCalendar();
             return UmmalQuraFor(html, expression, cal.GetYear(cal.MinSupportedDateTime), cal.GetYear(cal.MaxSupportedDateTime), htmlAttributes);

            /*
            var cal = new System.Globalization.UmAlQuraCalendar();
            var fieldName = ExpressionHelper.GetExpressionText(expression);

            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model as DateTime?;

            var selectedmonth = string.Empty;
            var selectedday = string.Empty;
            var selectedyear = string.Empty;
            if (value.HasValue)
            {
                if (value.Value > cal.MinSupportedDateTime && value.Value < cal.MaxSupportedDateTime)
                {
                    selectedday = cal.GetDayOfMonth(value.Value).ToString();

                    selectedmonth = cal.GetMonth(value.Value).ToString();
                    selectedyear = cal.GetYear(value.Value).ToString();
                }
            }
            var ha = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            TagBuilder tag = new TagBuilder("table");
            tag.InnerHtml = string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>",
                UmmalQuraDays(fieldId, selectedday, ha).ToString(),
                UmmalQuraMonths(fieldId, selectedmonth, ha).ToString(),
                UmmalQuraYears(fieldId, selectedyear, cal, ha));

            return new MvcHtmlString(tag.ToString());
            */
        }
        /*
        static TagBuilder UmmalQuraDays(string idprefex, string selectedday, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttributes(htmlAttributes);
            var pr = idprefex + "_day";
            tag.Attributes.Add("id", pr);
            tag.MergeAttribute("name", pr.Replace('_','.'), true );

            StringBuilder listItemBuilder = new StringBuilder();
            var sle = false;
            sle = ("" == selectedday);
            listItemBuilder.AppendLine(ListItemToOption(new SelectListItem()
            {
                Text = "يوم",
                Value = string.Empty,
                Selected = true
            }));
            for (int i = 1; i <= 30; i++)
            {
                sle = (i.ToString() == selectedday);
                listItemBuilder.AppendLine(ListItemToOption(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = sle
                }));
            }
            tag.InnerHtml = listItemBuilder.ToString();
            return tag;
        }

        static TagBuilder UmmalQuraMonths(string idprefex, string selectedmonth, IDictionary<string, object> htmlAttributes)
        {

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttributes(htmlAttributes);
            var pr = idprefex + "_month";
            tag.Attributes.Add("id", pr);
            tag.MergeAttribute("name", pr.Replace('_', '.'), true );

            StringBuilder listItemBuilder = new StringBuilder();
            var sle = false;
            sle = ("" == selectedmonth);
            listItemBuilder.AppendLine(ListItemToOption(new SelectListItem()
            {
                Text = "شهر",
                Value = string.Empty,
                Selected = sle
            }));
            for (int i = 1; i <= 12; i++)
            {
                sle = (i.ToString() == selectedmonth);
                listItemBuilder.AppendLine(ListItemToOption(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = sle
                }));
            }
            tag.InnerHtml = listItemBuilder.ToString();
            return tag;
        }

        static TagBuilder UmmalQuraYears(string idprefex, string selectedyear, System.Globalization.UmAlQuraCalendar cal, IDictionary<string, object> htmlAttributes)
        {

           

            return UmmalQuraYears(idprefex, selectedyear, cal, cal.GetYear(cal.MinSupportedDateTime), cal.GetYear(cal.MaxSupportedDateTime), htmlAttributes);
            
        }

        private static TagBuilder UmmalQuraYears(string idprefex, string selectedyear, System.Globalization.UmAlQuraCalendar cal, int MinYear, int MaxYear, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttributes(htmlAttributes);
            var pr = idprefex + "_year";
            tag.Attributes.Add("id", pr);
            tag.MergeAttribute("name", pr.Replace('_', '.'), true );

            StringBuilder listItemBuilder = new StringBuilder();
            var sle = false;
            sle = ("" == selectedyear);
            if (sle)
            {
                selectedyear = cal.GetYear(DateTime.Now).ToString();
            }
            listItemBuilder.AppendLine(ListItemToOption(new SelectListItem()
            {
                Text = "سنة",
                Value = string.Empty,
                Selected = sle
            }));
            for (int i = MinYear; i <= MaxYear; i++)
            {
                sle = (i.ToString() == selectedyear);
                listItemBuilder.AppendLine(ListItemToOption(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = sle
                }));
            }
            tag.InnerHtml = listItemBuilder.ToString();
            return tag;
        }

        internal static string ListItemToOption(SelectListItem item)
        {
            TagBuilder builder = new TagBuilder("option")
            {
                InnerHtml = HttpUtility.HtmlEncode(item.Text)
            };
            if (item.Value != null)
            {
                builder.Attributes["value"] = item.Value;
            }
            if (item.Selected)
            {
                builder.Attributes["selected"] = "selected";
            }
            //if (item.Disabled)
            //{
            //    builder.Attributes["disabled"] = "disabled";
            //}
            return builder.ToString(TagRenderMode.Normal);
        }
        */

        public static DateTime? GetDateFromHijriControl(NameValueCollection form, string modelrpopname)
        {
            DateTime? toreturn = null;
            var day = form[modelrpopname + ".day"];
            var month = form[modelrpopname + ".month"];
            var year = form[modelrpopname + ".year"];
            if (year.ToInt() > 0 && month.ToInt() > 0 && day.ToInt() > 0)
            {
                int dayin = day.ToInt() == 0 ? 1 : day.ToInt();
                int monthin = month.ToInt() == 0 ? 1 : month.ToInt();
                toreturn = Karasoft.Mvc.Utilities.DateUtility.ConvertHijriToGregorian(dayin, monthin, year.ToInt());
            }

            return toreturn;
        }

        public static dynamic GetOrSetCache(this BaseController WebCacheHelper, string key, object value = null, int minutesToCache = 20, bool slidingExpiration = true)
        {
            var data = WebCache.Get(key);
            if (data == null)
            {
                data = value; 
                WebCache.Set(key, value);
            }

            return data;
        }

        public static MvcHtmlString DisplayHijriDate(this HtmlHelper html, DateTime? expression)
        {
            return MvcHtmlString.Create(expression.ToHijriDate());
            //return TemplateHelpers.Template(html, expression, null /* templateName */, null /* htmlFieldName */, DataBoundControlMode.ReadOnly, null /* additionalViewData */);
        }

        public static MvcHtmlString DisplayHijriDate(this HtmlHelper html, DateTime? expression, object additionalViewData)
        {
            return MvcHtmlString.Create(expression.ToHijriDate());
            // return TemplateHelpers.Template(html, expression, null /* templateName */, null /* htmlFieldName */, DataBoundControlMode.ReadOnly, additionalViewData);
        }
    }
}
