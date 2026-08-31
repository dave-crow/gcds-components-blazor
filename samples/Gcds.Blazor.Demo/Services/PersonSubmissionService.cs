using System.ComponentModel.DataAnnotations;
using Gcds.Blazor.Demo.Models;

namespace Gcds.Blazor.Demo.Services;

public interface IPersonSubmissionService
{
    Task<PersonSubmissionResult> SubmitAsync(PersonModel person);
}

public sealed class PersonSubmissionService : IPersonSubmissionService
{
    public Task<PersonSubmissionResult> SubmitAsync(PersonModel person)
    {
        ArgumentNullException.ThrowIfNull(person);

        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(
            person,
            new ValidationContext(person),
            validationResults,
            validateAllProperties: true);

        var errors = validationResults
            .Select(result => result.ErrorMessage ?? "The submitted value is invalid.")
            .ToArray();

        return Task.FromResult(new PersonSubmissionResult(isValid, errors));
    }
}

public sealed record PersonSubmissionResult(bool Succeeded, IReadOnlyList<string> Errors);
