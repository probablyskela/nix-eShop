using AutoMapper;
using Catalog.API.Repository.Abstractions;
using Catalog.API.Service.Services.Abstractions;
using Shared.Data.Dtos.PictureDtos;
using Shared.Data.Entities;

namespace Catalog.API.Service.Services;

public class PictureService : IPictureService
{
    private readonly IRepositoryManager _repository;
    private readonly ILogger<PictureService> _logger;
    private readonly IMapper _mapper;

    public PictureService(IRepositoryManager repositoryManager, ILogger<PictureService> logger, IMapper mapper)
    {
        _repository = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PictureDto> CreatePictureAsync(PictureForCreationDto pictureForCreation)
    {
        var pictureEntity = new Picture { PictureFileName = "temp" };
        pictureEntity.PictureFileName =
            $"{pictureEntity.Id}{Path.GetExtension(pictureForCreation.PictureFile.FileName)}";

        await _repository.Picture.CreatePictureAsync(pictureEntity);
        await _repository.SaveAsync();

        var pictureDto = _mapper.Map<PictureDto>(pictureEntity);

        return pictureDto;
    }
}