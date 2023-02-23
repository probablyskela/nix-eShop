using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Shared.Data.Dtos.Attributes;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not IFormFile file)
        {
            return new ValidationResult("File is null.");
        }

        var extension = Path.GetExtension(file.FileName);
        
        return !_extensions.Contains(extension.ToLower())
            ? new ValidationResult("File extension is not allowed.")
            : ValidationResult.Success;
    }
}