using Catalog.API.Repository.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Entities;
using Shared.Repository;

namespace Catalog.API.Repository.Repositories;

public class PictureRepository : RepositoryBase<Picture>, IPictureRepository
{
    public PictureRepository(DbContext repositoryContext)
        : base(repositoryContext)
    {
    }
}