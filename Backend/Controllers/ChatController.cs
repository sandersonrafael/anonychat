using Backend.Exceptions;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/chats")]
public class ChatController(ChatService service) : ControllerBase
{
    private readonly ChatService _service = service;

    [HttpGet]
    public async Task<IActionResult> FindAllByUserId([FromRoute] Guid id)
    {
        try
        {
            // Implement JWT validation to get information
            List<ChatResponse> chatResponses = await _service.FindAllByUserId(id);
            return Ok(chatResponses);
        }
        catch (ApiException e)
        {
            return StatusCode(e.Status, new ExceptionResponse(e.Status, Request.Path, Request.Method, e.Message));
        }
        catch (Exception)
        {
            return StatusCode(500, ExceptionResponse.Internal(Request.Path, Request.Method));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(ChatRequest request)
    {
        try
        {
            // Validator

            ChatResponse chat = await _service.Create(request);
            return StatusCode(201, chat);
        }
        catch (ApiException e)
        {
            return StatusCode(e.Status, new ExceptionResponse(e.Status, Request.Path, Request.Method, e.Message));
        }
        catch(Exception) 
        {
            return StatusCode(500, ExceptionResponse.Internal(Request.Path, Request.Method));
        }
    }
}
