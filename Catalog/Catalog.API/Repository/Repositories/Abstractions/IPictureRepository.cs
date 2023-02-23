using Shared.Data.Entities;
using Shared.Data.Requests.RequestFeatures.Parameters;
using Shared.Misc;

namespace Catalog.API.Repository.Repositories.Abstractions;

public interface IPictureRepository
{
    Task<PagedList<Picture>> GetPicturesAsync(PictureParameters pictureParameters, bool trackChanges);
    Task<Picture?> GetPictureAsync(int productId, bool trackChanges);
    Task CreatePictureAsync(Picture picture);
}