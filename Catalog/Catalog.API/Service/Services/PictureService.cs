using AutoMapper;
using Catalog.API.Repository.Abstractions;
using Catalog.API.Service.Services.Abstractions;

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
}