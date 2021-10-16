using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Saitynas_API_Tests.Mocks;
using Saitynas_API.Models;
using Saitynas_API.Models.WorkplaceEntity;
using Saitynas_API.Models.WorkplaceEntity.Repository;
using static NUnit.Framework.Assert;

namespace Saitynas_API_Tests
{
    [TestFixture]
    public class WorkplacesRepositoryTests
    {
        private ApiContext _context;
        private IWorkplacesRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _context = new ApiContextMock();
            _repository = new WorkplacesRepository(_context);
        }

        [Test]
        public async Task TestGetAll()
        {
            var workplaces = await _repository.GetAllAsync();

            AreEqual(_context.Workplaces.Count(), workplaces.Count());
        }

        [Test]
        public async Task TestGetExistingElement()
        {
            const int targetId = 1;
            var workplace = await _repository.GetAsync(targetId);

            AreEqual(targetId, workplace.Id);
        }

        [Test]
        public async Task TestGetNonExistingElement()
        {
            var workplace = await _repository.GetAsync(0);

            IsNull(workplace);
        }

        [Test]
        public async Task TestInsert()
        {
            await _repository.InsertAsync(new Workplace
            {
                City = "Test City"
            });

            await TestGetAll();
        }

        [Test]
        public async Task TestUpdate()
        {
            const string city = "Test updated";
            const int targetId = 1;

            var workplace = new Workplace
            {
                Id = targetId,
                City = city
            };

            await _repository.UpdateAsync(targetId, workplace);

            var updatedWorkplace = await _repository.GetAsync(targetId);

            AreEqual(city, updatedWorkplace.City);
        }

        [Test]
        public async Task TestExistingDelete()
        {
            const int targetId = 1;

            await _repository.DeleteAsync(targetId);

            var deletedWorkplace = await _repository.GetAsync(targetId);

            IsNull(deletedWorkplace);
        }
        
        [Test]
        public async Task TestNonExistingDelete()
        {
            const int targetId = 0;

            await _repository.DeleteAsync(targetId);

            Pass();
        }
    }
}
