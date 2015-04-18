using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karasoft.Mvc.Extension;
using System.Web.Mvc;

namespace Karasoft.Mvc.ModelBinding
{
    public class HijriDateModelBinder : IModelBinder
    {

        public HijriDateModelBinder()
        {
            // var ee = 1;

        }



        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            DateTime? toreturn = null;
            var day = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".day").AttemptedValue;
            var month = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".month").AttemptedValue;
            var year = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".year").AttemptedValue;
            if (year.ToInt() > 0 && month.ToInt() > 0 && day.ToInt() > 0)
            {
                int dayin = day.ToInt() == 0 ? 1 : day.ToInt();
                int monthin = month.ToInt() == 0 ? 1 : month.ToInt();
                toreturn = Karasoft.Mvc.Utilities.DateUtility.ConvertHijriToGregorian(dayin, monthin, year.ToInt());
            }

            return toreturn;
            //  var date = DateTime.Now;

            //return date;
        }
    }
}
