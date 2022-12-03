namespace ChatApp.Domain.Interfaces;

public interface IUnitOfWork
{
    Task Commit();
}