using System.Collections.Generic;
using NUnit.Framework;
using Saitynas_API_Tests.Mocks;
using Saitynas_API.Services;

namespace Saitynas_API_Tests;

[TestFixture]
public class EntityValidatorTests
{
    [SetUp]
    public void Setup()
    {
        var context = new ApiContextMock();

        _validator = new EntityValidator(context);
    }

    private IEntityValidator _validator;

    private static IEnumerable<TestCaseData> WorkplaceTestData()
    {
        yield return new TestCaseData(1, true);
        yield return new TestCaseData(0, false);
        yield return new TestCaseData(10, false);
        yield return new TestCaseData(null, true);
    }

    [Test]
    [TestCaseSource(nameof(WorkplaceTestData))]
    public void TestWorkplaceExists(int? id, bool isValid)
    {
        Assert.AreEqual(isValid, _validator.IsWorkplaceIdValid(id));
    }
}
