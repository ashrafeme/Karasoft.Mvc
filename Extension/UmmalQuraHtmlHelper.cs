using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Karasoft.Mvc.Extension
{
   public class UmmalQuraHtmlHelper
    {

        public string ListItemToOption(SelectListItem item)
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

        public TagBuilder UmmalQuraMinutes(string idprefex, string selectedMin, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttributes(htmlAttributes);
            var pr = idprefex + "_Minutes";
            tag.Attributes.Add("id", pr);
            tag.MergeAttribute("name", pr.Replace('_', '.'), true /* replaceExisting */);

            StringBuilder listItemBuilder = new StringBuilder();
            var sle = false;
            sle = ("" == selectedMin);
            listItemBuilder.AppendLine(ListItemToOption(new SelectListItem()
            {
                Text = "ساعة",
                Value = string.Empty,
                Selected = true
            }));
            for (int i = 0; i <= 23; i++)
            {
                sle = (i.ToString() == selectedMin);
                var valu = string.Format("{0:00}", i);
                listItemBuilder.AppendLine(ListItemToOption(new SelectListItem()
                {
                    Text = valu,
                    Value = valu,
                    Selected = sle
                }));
            }
            tag.InnerHtml = listItemBuilder.ToString();
            return tag;
        }

        public TagBuilder UmmalQuraHours(string idprefex, string selectedHour, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttributes(htmlAttributes);
            var pr = idprefex + "_Hour";
            tag.Attributes.Add("id", pr);
            tag.MergeAttribute("name", pr.Replace('_', '.'), true /* replaceExisting */);

            StringBuilder listItemBuilder = new StringBuilder();
            var sle = false;
            sle = ("" == selectedHour);
            listItemBuilder.AppendLine(ListItemToOption(new SelectListItem()
            {
                Text = "ساعة",
                Value = string.Empty,
                Selected = true
            }));
            for (int i = 0; i <= 23; i++)
            {
                sle = (i.ToString() == selectedHour);
                var valu = string.Format("{0:00}", i);
                listItemBuilder.AppendLine(ListItemToOption(new SelectListItem()
                {
                    Text = valu,
                    Value = valu,
                    Selected = sle
                }));
            }
            tag.InnerHtml = listItemBuilder.ToString();
            return tag;
        }

      public  TagBuilder UmmalQuraDays(string idprefex, string selectedday, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttributes(htmlAttributes);
            var pr = idprefex + "_day";
            tag.Attributes.Add("id", pr);
            tag.MergeAttribute("name", pr.Replace('_', '.'), true /* replaceExisting */);

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
                var valu = string.Format("{0:00}", i);
                listItemBuilder.AppendLine(ListItemToOption(new SelectListItem()
                {
                    Text = valu,
                    Value = valu,
                    Selected = sle
                }));
            }
            tag.InnerHtml = listItemBuilder.ToString();
            return tag;
        }

        public TagBuilder UmmalQuraMonths(string idprefex, string selectedmonth, IDictionary<string, object> htmlAttributes)
        {

            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttributes(htmlAttributes);
            var pr = idprefex + "_month";
            tag.Attributes.Add("id", pr);
            tag.MergeAttribute("name", pr.Replace('_', '.'), true /* replaceExisting */);

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
                var valu = string.Format("{0:00}", i);
                listItemBuilder.AppendLine(ListItemToOption(new SelectListItem()
                {
                    Text = valu,
                    Value = valu,
                    Selected = sle
                }));
            }
            tag.InnerHtml = listItemBuilder.ToString();
            return tag;
        }

        public TagBuilder UmmalQuraYears(string idprefex, string selectedyear, System.Globalization.UmAlQuraCalendar cal, IDictionary<string, object> htmlAttributes)
        {



            return UmmalQuraYears(idprefex, selectedyear, cal, cal.GetYear(cal.MinSupportedDateTime), cal.GetYear(cal.MaxSupportedDateTime), htmlAttributes);

        }

       public TagBuilder UmmalQuraYears(string idprefex, string selectedyear, System.Globalization.UmAlQuraCalendar cal, int MinYear, int MaxYear, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tag = new TagBuilder("select");
            tag.MergeAttributes(htmlAttributes);
            var pr = idprefex + "_year";
            tag.Attributes.Add("id", pr);
            tag.MergeAttribute("name", pr.Replace('_', '.'), true /* replaceExisting */);

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
                var valu = string.Format("{0:00}", i);
                listItemBuilder.AppendLine(ListItemToOption(new SelectListItem()
                {
                    Text = valu,
                    Value = valu,
                    Selected = sle
                }));
            }
            tag.InnerHtml = listItemBuilder.ToString();
            return tag;
        }

    }
}
