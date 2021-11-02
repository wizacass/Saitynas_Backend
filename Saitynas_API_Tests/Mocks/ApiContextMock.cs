using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models;
using Saitynas_API.Models.Entities.Workplace;

namespace Saitynas_API_Tests.Mocks
{
    public class ApiContextMock : ApiContext
    {
        private static readonly DbContextOptions<ApiContext> DbContextOptions =
            new DbContextOptionsBuilder<ApiContext>().UseInMemoryDatabase("MockDb")
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;

        public ApiContextMock() : base(DbContextOptions)
        {
            SeedDb();
        }

        private void SeedDb()
        {
            new WorkplaceSeed(this).EnsureCreated();
        }
    }
}
