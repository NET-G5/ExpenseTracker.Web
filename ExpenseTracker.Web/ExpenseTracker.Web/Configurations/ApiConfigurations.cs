using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Web.Configurations;

public class ApiConfigurations
{
    public const string SectionName = nameof(ApiConfigurations);

    [Required(ErrorMessage = "API Base URL is required.")]
    public required string BaseAddress { get; init; }
}
