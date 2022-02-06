using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Saitynas_API.Models.Common.Interfaces;
using Saitynas_API.Models.Entities.Role;

namespace Saitynas_API.Models.Entities.User;

public class UserSeed : ISeed
{
    private const string DefaultPasswordKey = "DefaultPassword";
    private readonly string _defaultPassword;

    private readonly ApiContext _context;
    private readonly UserManager<User> _userManager;

    public UserSeed(ApiContext apiContext, UserManager<User> userManager, IConfiguration configuration)
    {
        _context = apiContext;
        _userManager = userManager;
        _defaultPassword = configuration[DefaultPasswordKey] ?? Environment.GetEnvironmentVariable(DefaultPasswordKey);
    }

    public void EnsureCreated()
    {
        if (!ShouldSeed()) return;

        var users = new[]
        {
            new User
            {
                Id = 1,
                Email = "admin@example.com",
                RegistrationDate = DateTime.UtcNow,
                RoleId = RoleId.Admin
            },
            new User
            {
                Id = 2,
                Email = "specialist@example.com",
                RegistrationDate = DateTime.UtcNow,
                RoleId = RoleId.Specialist,
                SpecialistId = 1
            },
            new User
            {
                Id = 3,
                Email = "patient@example.com",
                RegistrationDate = DateTime.UtcNow,
                RoleId = RoleId.Patient,
                PatientId = 1
            }
        };

        foreach (var user in users)
        {
            _ = _userManager.CreateAsync(user, _defaultPassword).Result;
        }

        _context.SaveChanges();
    }

    private bool ShouldSeed()
    {
        var testObject = _context.Users.IgnoreQueryFilters().FirstOrDefault(o => o.Id == 1);

        return testObject == null;
    }
}
