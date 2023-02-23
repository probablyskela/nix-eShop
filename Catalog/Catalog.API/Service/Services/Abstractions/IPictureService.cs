using Shared.Data.Dtos.PictureDtos;

namespace Catalog.API.Service.Services.Abstractions;

public interface IPictureService
{
    Task<PictureDto> CreatePictureAsync(PictureForCreationDto pictureForCreation);
}