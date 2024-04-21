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

        //var firstLetter = value.ToString()[0].ToString();
        //if (firstLetter != firstLetter.ToUpper())
        //{
        //    return new ValidationResult("First letter the name of product has to be uppercase");
        //}

        // This is the same code above but in a way that resolves the nullability error.
        var stringValue = value.ToString();
        if (!string.IsNullOrEmpty(stringValue))
        {
            var firstLetter = stringValue[0].ToString();
            if (firstLetter != firstLetter.ToUpper())
            {
                return new ValidationResult("First letter the name of product has to be uppercase");
            }
        }

        return ValidationResult.Success;
    }
}
