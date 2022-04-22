using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Saitynas_API_Tests.Mocks;
using Saitynas_API.Models;
using Saitynas_API.Models.Entities.Consultation;
using Saitynas_API.Repositories;
using static NUnit.Framework.Assert;

namespace Saitynas_API_Tests.RepositoryTests;

[TestFixture]
public class ConsultationsRepositoryTests
{
    private readonly ApiContext _context;

    private IConsultationsRepository _repository;

    private const string TestPatientDeviceToken = "Patient123";
    private const string TestSpecialistDeviceToken = "Specialist123";

    private static readonly Guid TestGuid1 = new("0ea24b43-e30a-45d7-aac9-e1653d5167a8");
    private static readonly Guid TestGuid2 = new("2639d9a8-eda9-4543-b477-d0540069b1b6");

    private readonly Consultation[] _testConsultations =
    {
        new()
        {
            Id = 1,
            PublicId = TestGuid1,
            PatientDeviceToken = TestPatientDeviceToken,
            RequestedAt = new DateTime(2022, 04, 20, 16, 00, 00),
            IsCancelled = true
        },
        new()
        {
            Id = 2,
            PublicId = TestGuid2,
            PatientDeviceToken = TestPatientDeviceToken,
            RequestedAt = new DateTime(2022, 04, 20, 18, 00, 00),
            SpecialistDeviceToken = TestSpecialistDeviceToken
        },
        new()
        {
            Id = 3,
            PublicId = TestGuid2,
            SpecialistId = 1,
            PatientDeviceToken = TestPatientDeviceToken,
            PatientId = 1,
            RequestedAt = new DateTime(2022, 04, 22, 18, 00, 00),
            StartedAt = new DateTime(2022, 04, 22, 18, 10, 00),
            FinishedAt = new DateTime(2022, 04, 22, 18, 30, 00),
            SpecialistDeviceToken = TestSpecialistDeviceToken
        }
    };

    public ConsultationsRepositoryTests()
    {
        _context = new ApiContextMock();
        SeedConsultations();
    }

    [SetUp]
    public void SetUp()
    {
        _repository = new ConsultationsRepository(_context);
    }

    private void SeedConsultations()
    {
        _context.Consultations.AddRange(_testConsultations);
        _context.SaveChanges();
    }

    [Test]
    public async Task TestGetAll()
    {
        var consultations = await _repository.GetAllAsync();

        AreEqual(_testConsultations.Length, consultations.Count());
    }

    [Test]
    public async Task TestGetExistingElement()
    {
        const int targetId = 1;
        var consultation = await _repository.GetAsync(targetId);

        AreEqual(targetId, consultation.Id);
    }

    [Test]
    public async Task TestGetNonExistingElement()
    {
        var consultation = await _repository.GetAsync(0);

        IsNull(consultation);
    }

    [Test]
    public async Task TestInsert()
    {
        await _repository.InsertAsync(new Consultation
        {
            PatientDeviceToken = TestPatientDeviceToken,
        });

        var consultations = await _repository.GetAllAsync();

        AreEqual(_testConsultations.Length + 1, consultations.Count());
    }

    [Test]
    public async Task TestUpdate()
    {
        const int targetId = 2;

        var consultation = await _repository.GetAsync(targetId);
        IsFalse(consultation.IsCancelled);

        consultation.IsCancelled = true;

        await _repository.UpdateAsync(targetId, consultation);
        consultation = await _repository.GetAsync(targetId);

        IsTrue(consultation.IsCancelled);
    }

    [Test]
    public async Task TestFindByPublicID()
    {
        var consultation = await _repository.FindByPublicID(TestGuid1);

        AreEqual(TestGuid1, consultation.PublicId);
    }

    [Test]
    public async Task TestFindRequestedBySpecialistDeviceToken()
    {
        var consultation = await _repository.FindRequestedBySpecialistDeviceToken(TestSpecialistDeviceToken);

        AreEqual(TestSpecialistDeviceToken, consultation.SpecialistDeviceToken);
    }

    [Test]
    public async Task TestGetFinishedBySpecialistId()
    {
        const int specialistId = 1;

        var consultations = await _repository.GetFinishedBySpecialistId(specialistId);

        AreEqual(1, consultations.Count());
    }
}
