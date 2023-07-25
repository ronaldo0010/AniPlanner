using AniPlannerApi.Models.Types;

namespace AniPlannerApi.Models;

public class Media
{
    public Guid MediaId { get; set; }
    public MediaType Type { get; set; }
    public string Title { get; set; }
    public MediaStatus Status { get; set; }
    public string PictureUrl { get; set; }
    public ICollection<Tag> Tags { get; set; }
    public int Episodes { get; set; }
}
