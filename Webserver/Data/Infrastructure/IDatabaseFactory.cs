namespace Webserver.Data.Infrastructure
{
    public interface IDatabaseFactory
    {
        ApplicationDbContext Initialize();
    }
}
