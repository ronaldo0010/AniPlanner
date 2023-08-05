using Contracts;
using Entities.Data;
using Entities.Models;

namespace Repository;

public class MediaRepository : GenericRepo<Media>, IMediaRepository
{
    public MediaRepository(DataContext context) : base(context) { }
    
}