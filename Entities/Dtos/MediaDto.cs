using Entities.Types;

namespace Entities.Dtos;

public class MediaDto
{
    public Guid MediaId { get; set; }
    public MediaType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public MediaStatus Status { get; set; }
    public string PictureUrl { get; set; } = string.Empty;
    public int Episodes { get; set; }
    public ICollection<TagDto> MediaTags { get; set; }
}