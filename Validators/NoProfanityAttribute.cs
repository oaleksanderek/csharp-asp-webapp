namespace WebApp.Validators;

using System.ComponentModel.DataAnnotations;

public class NoProfanityAttribute : ValidationAttribute
{
    private readonly string[] _badWords = { "cake", "Kafka", "essa" };

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string comment)
        {
            foreach (var badWord in _badWords)
            {
                if (comment.Contains(badWord, StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidationResult($"The review contains: {badWord}");
                }
            }
        }

        return ValidationResult.Success;
    }
}