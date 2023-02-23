using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Shared.Data.Dtos.Attributes;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;

    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not IFormFile file)
        {
            return new ValidationResult("File is null.");
        }

        return file.Length > _maxFileSize
            ? new ValidationResult($"Maximum allowed file size is {_maxFileSize} bytes.")
            : ValidationResult.Success;
    }
}