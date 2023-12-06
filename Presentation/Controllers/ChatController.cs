using Core.Features.Chat.Command;
using Core.Features.Chat.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("Correo")]
    public async Task<ActionResult> PostCorreo([FromBody] CorreoCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    
    [HttpPost("Mensaje")]
    public async Task<ActionResult> PostMensaje([FromBody] ChatCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    
    [HttpGet("Chats/{id_contrato}")]
    public async Task<List<ObtenerChatsResponse>> GetChats(int id_contrato)
    {
        return await _mediator.Send(new ObtenerChats(){ Id_Contrato = id_contrato });
    }
}