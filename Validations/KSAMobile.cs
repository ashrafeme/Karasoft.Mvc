
using Karasoft.Mvc.Extension;
using System.ComponentModel.DataAnnotations;
namespace Karasoft.Mvc.Validations
{
    public class KSAMobile : ValidationAttribute
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
    }
}
