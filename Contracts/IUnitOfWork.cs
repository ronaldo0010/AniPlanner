namespace Contracts;

// TODO: Implement IDisposable
public interface IUnitOfWork
{
    IMediaRepository MediaRepo { get; }
    ITagRepository TagRepo { get; }
    IUserRepository UserRepo { get; }
    void Save();
}