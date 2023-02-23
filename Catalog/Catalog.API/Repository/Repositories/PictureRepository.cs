using Catalog.API.Repository.Extensions;
using Catalog.API.Repository.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;
using Shared.Repository;

namespace Catalog.API.Repository.Repositories;

public class PictureRepository : RepositoryBase<Picture>, IPictureRepository
{
    public PictureRepository(DbContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<PagedList<Picture>> GetPicturesAsync(PictureParameters pictureParameters, bool trackChanges)
    {
        var pictures = FindAll(trackChanges)
            .Sort(pictureParameters.OrderBy)
            .Include(p => p.ProductVariants);

        return await PagedList<Picture>
            .ToPagedList(pictures, pictureParameters.PageIndex, pictureParameters.PageSize);
    }

    public async Task<Picture?> GetPictureAsync(int productId, bool trackChanges)
    {
        return await FindByCondition(c => c.Id.Equals(productId), trackChanges)
            .Include(p => p.ProductVariants)
            .SingleOrDefaultAsync();
    }

    public async Task CreatePictureAsync(Picture picture)
    {
        await CreateAsync(picture);
    }
}