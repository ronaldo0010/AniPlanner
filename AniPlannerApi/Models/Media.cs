using System.ComponentModel.DataAnnotations;
using AniPlannerApi.Models.Types;

namespace AniPlannerApi.Models;

public class Media
{
    [Key]
    public Guid MediaId { get; set; }
    public MediaType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public MediaStatus Status { get; set; }
    public string PictureUrl { get; set; } = string.Empty;
    public ICollection<MediaTag> MediaTags { get; set; } = new List<MediaTag>();
    public int Episodes { get; set; }

    /// <summary>
    /// Adds the tags given to the Media object as media tags
    /// </summary>
    /// <param name="tags">List of tags.</param>
    public void AddTags(List<Tag> tags)
    {
        foreach (var tag in tags)
        {
            MediaTags.Add(new MediaTag
            {
                Tag = tag,
                TagId = tag.TagId,
                Media = this,
                MediaId = MediaId
            });
        }
    }
}
