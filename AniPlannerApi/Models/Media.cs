using System.ComponentModel.DataAnnotations;
using AniPlannerApi.Models.Types;

namespace AniPlannerApi.Models;

public class Media
{
    [Key]
    public Guid MediaId { get; set; }
    public MediaType Type { get; set; }
    public string Title { get; set; }
    public MediaStatus Status { get; set; }
    public string PictureUrl { get; set; }
    public ICollection<MediaTag> MediaTags { get; set; }
    public int Episodes { get; set; }
}
