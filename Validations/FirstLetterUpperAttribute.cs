using System.ComponentModel.DataAnnotations;

namespace ApiCatalog.Validations;

public class FirstLetterUpperAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString())) 
        {
            return ValidationResult.Success;
        }

        var firstLetter = value.ToString()[0].ToString();
        if (firstLetter != firstLetter.ToUpper())
        {
            return new ValidationResult("First letter the name of product has to be uppercase");
        }

        return ValidationResult.Success;
    }
}
