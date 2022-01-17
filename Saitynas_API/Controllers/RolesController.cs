using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Role;

namespace Saitynas_API.Controllers;

[Route($"{RoutePrefix}/[Controller]")]
[ApiController]
public class RolesController : ApiControllerBase
{
    protected override string ModelName => "Role";

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<GetListDTO<GetEnumDTO>> GetRoles()
    {
        var roles = Enum.GetValues(typeof(RoleId))
            .Cast<RoleId>()
            .Skip(2)
            .Select(r => new GetEnumDTO
            {
                Id = (int) r,
                Name = r.ToString()
            })
            .ToList();

        var dto = new GetListDTO<GetEnumDTO>(roles);

        return Ok(dto);
    }
}
