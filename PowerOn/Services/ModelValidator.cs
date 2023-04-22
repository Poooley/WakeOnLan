using System.ComponentModel.DataAnnotations;

namespace WakeOnLan.Services;

public static class ModelValidator
{
    public static bool TryValidate(object model, out ICollection<ValidationResult> validationResults)
    {
        validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model);
        return Validator.TryValidateObject(model, validationContext, validationResults, true);
    }
}