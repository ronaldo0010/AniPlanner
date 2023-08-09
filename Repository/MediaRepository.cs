using Contracts;
using Entities.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class MediaRepository : GenericRepo<Media>, IMediaRepository
{
    public MediaRepository(DataContext context) : base(context) { }

    public async Task<List<Media>> FindBatchWithTagsAsync()
    {
        var size = await _context.Media.CountAsync();
        var skip = new Random().Next(0, size);
        
        return await _context.Media
            .Include(x => x.MediaTags)
            .ThenInclude(x => x.Tag)
            .Skip(skip)
            .Take(10)
            .ToListAsync();
    }
}