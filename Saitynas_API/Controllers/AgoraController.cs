using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.Agora;
using Saitynas_API.Models.Agora.DTO;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Role;
using Saitynas_API.Services.AgoraIO.Media;

namespace Saitynas_API.Controllers;

[Route($"{RoutePrefix}/[Controller]")]
[ApiController]
[Produces(ApiContentType)]
public class AgoraController : ApiControllerBase
{
    protected override string ModelName => "Agora";

    private readonly AgoraSettings _settings;
    
    public AgoraController(AgoraSettings settings)
    {
        _settings = settings;
    }
    
    [HttpPost("tokens")]
    [Authorize(Roles = AuthRole.AnyRole)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<GetObjectDTO<AgoraTokenResponseDTO>> RequestAgoraToken([FromBody] AgoraTokenRequestDTO dto)
    {
        string token = BuildAgoraToken(dto.Channel);

        var response = new AgoraTokenResponseDTO
        {
            Channel = dto.Channel,
            Uid = "0",
            Token = token
        };

        return ApiCreated(new GetObjectDTO<AgoraTokenResponseDTO>(response));
    }

    private string BuildAgoraToken(string channel)
    {
        const int expiredTs = 0;
        
        var tokenBuilder = new AgoraAccessToken(_settings.AppId, _settings.AppCertificate, channel, "0");
        tokenBuilder.AddPrivilege(Privileges.KJoinChannel, expiredTs);
        tokenBuilder.AddPrivilege(Privileges.KPublishAudioStream, expiredTs);
        tokenBuilder.AddPrivilege(Privileges.KPublishVideoStream, expiredTs);
        tokenBuilder.AddPrivilege(Privileges.KPublishDataStream, expiredTs);
        tokenBuilder.AddPrivilege(Privileges.KRtmLogin, expiredTs);

        return tokenBuilder.Build();
    }
}
