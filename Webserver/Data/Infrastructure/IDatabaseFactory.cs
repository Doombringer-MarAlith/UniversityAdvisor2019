using System;
namespace Webserver.Data.Infrastructure
{
    public interface IDatabaseFactory
    {
        ApplicationDbContext Initialize();
    }
}
