using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karasoft.Mvc.Extension;
using System.Web.Mvc;

namespace Karasoft.Mvc.Validations
{
    public class SaudiID : ValidationAttribute, IClientValidatable
    {
        public SaudiID()
            : base("{0} رقم غير صالح.")
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var mob = value.ToString();
                if (!mob.IsValidSaudiID())
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }


            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
           // rule.ValidationParameters.Add("saudiid", metadata.v);
            rule.ValidationType = "saudiid";
            yield return rule;
        }
    }
}
