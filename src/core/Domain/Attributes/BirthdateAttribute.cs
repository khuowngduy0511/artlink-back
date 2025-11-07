using System.ComponentModel.DataAnnotations;

namespace Domain.Attributes;

public class BirthdateAttribute : ValidationAttribute
{
    private const int MinimumAge = 12;
    private const int MaximumAge = 150;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;
        if (value is DateTime birthdate)
        {
            // Calculate age
            int age = CalculateAge(birthdate);

            // Check if age is within the specified range
            if (age >= MinimumAge && age <= MaximumAge)
                return ValidationResult.Success;
            else
                return new ValidationResult($"The age must be between {MinimumAge} and {MaximumAge}.");
        }

        return new ValidationResult("Invalid birthdate format.");
    }

    private static int CalculateAge(DateTime birthdate)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - birthdate.Year;

        // Adjust age if birthday hasn't occurred yet this year
        if (birthdate > today.AddYears(-age))
        {
            age--;
        }
        return age;
    }
}
