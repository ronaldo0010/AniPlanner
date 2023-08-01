using AniPlannerApi.Data;
using Contracts;

namespace Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _repoContext;
    private IMediaRepository _media = null!;
    private ITagRepository _tag = null!;
    private IUserRepository _user = null!;
    
    public IMediaRepository MediaRepo
    {
        get
        {
            if (_media == null) _media = new MediaRepository(_repoContext);
            
            return _media;
        }
    }
    
    public ITagRepository TagRepo
    {
        get
        {
            if (_tag == null) _tag = new TagRepository(_repoContext);
            
            return _tag;
        }
    }
    
    public IUserRepository UserRepo
    {
        get
        {
            if (_user == null) _user = new UserRepository(_repoContext);
            
            return _user;
        }
    }
    

    public void Save()
    {
        throw new NotImplementedException();
    }

    public UnitOfWork(DataContext context)
    {
        _repoContext = context;
    }
    
}