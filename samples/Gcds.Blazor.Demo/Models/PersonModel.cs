using System.ComponentModel.DataAnnotations;

namespace Gcds.Blazor.Demo.Models;

public sealed class PersonModel
{
    [Required(ErrorMessage = "Enter your name.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Enter your date of birth.")]
    [ValidDateOfBirth]
    public string? DateOfBirth { get; set; }

    [Required(ErrorMessage = "Enter your email address.")]
    [EmailAddress(ErrorMessage = "Enter an email address in the correct format.")]
    public string? Email { get; set; }
}

internal sealed class ValidDateOfBirthAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string dateOfBirth || string.IsNullOrWhiteSpace(dateOfBirth))
        {
            return ValidationResult.Success;
        }

        if (!DateOnly.TryParse(dateOfBirth, out var parsedDate))
        {
            return new ValidationResult("Enter a valid date of birth.");
        }

        return parsedDate <= DateOnly.FromDateTime(DateTime.Today)
            ? ValidationResult.Success
            : new ValidationResult("Date of birth cannot be in the future.");
    }
}
