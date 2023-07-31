using System.ComponentModel.DataAnnotations;

namespace AniPlannerApi.Models;

public class Tag
{
    [Key]
    public Guid TagId { get; set; }
    public string Name { get; set; }
    public ICollection<MediaTag> MediaTags { get; set; }
    public Guid MediaId { get; set; }
}