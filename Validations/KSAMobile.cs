
using Karasoft.Mvc.Extension;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Karasoft.Mvc.Validations
{
    public class KSAMobile : ValidationAttribute, IClientValidatable
    {
        public KSAMobile()
            : base("{0} غير صحيح.")
        {

        }



        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var mob = value.ToString().ToKsaMobileNumber();
                if (!mob.IsKasMobileNumberValid())
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
            
            rule.ValidationType = "ksamobile";
            yield return rule;
        }
    }
}
