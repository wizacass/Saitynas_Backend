using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models.Common.Interfaces;
using Saitynas_API.Models.Entities.Role;

namespace Saitynas_API.Models.Entities.User;

public class UserSeed : ISeed
{
    private const string DefaultPassword = "Password123";
    private readonly ApiContext _context;
    private readonly UserManager<User> _userManager;

    public UserSeed(ApiContext apiContext, UserManager<User> userManager)
    {
        _context = apiContext;
        _userManager = userManager;
    }

    public async Task EnsureCreated()
    {
        if (!ShouldSeed()) return;

        var users = new[]
        {
            new User
            {
                Id = 1,
                Email = "admin@saitynai.lt",
                RegistrationDate = new DateTime(2021, 10, 10),
                RoleId = RoleId.Admin
            },
            new User
            {
                Id = 2,
                Email = "specialist@saitynai.lt",
                RegistrationDate = new DateTime(2021, 10, 10),
                RoleId = RoleId.Specialist,
                SpecialistId = 2
            },
            new User
            {
                Id = 3,
                Email = "patient@saitynai.lt",
                RegistrationDate = new DateTime(2021, 10, 10),
                RoleId = RoleId.Patient
            }
        };

        foreach (var user in users) await _userManager.CreateAsync(user, DefaultPassword);

        await _context.SaveChangesAsync();
    }

    private bool ShouldSeed()
    {
        var testObject = _context.Users.IgnoreQueryFilters().FirstOrDefault(o => o.Id == 1);

        return testObject == null;
    }
}