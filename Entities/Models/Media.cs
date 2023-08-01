using System.ComponentModel.DataAnnotations;
using Entities.Types;

namespace Entities.Models;

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
    public void AddTags(List<string> tags)
    {
        foreach (var tag in tags)
        {
            MediaTags.Add(new MediaTag
            {
                Tag = new Tag
                {
                    Name = tag
                },
                MediaId = MediaId
            });
        }
    }
}
