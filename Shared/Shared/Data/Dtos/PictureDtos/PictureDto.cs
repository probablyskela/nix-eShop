namespace Shared.Data.Dtos.PictureDtos;

public record PictureDto
{
    public int Id { get; set; }
    public string PictureFileName { get; set; } = null!;
}