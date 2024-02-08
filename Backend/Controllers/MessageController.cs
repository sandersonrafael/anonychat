using Backend.Exceptions;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/messages")]
public class MessageController(MessageService service) : ControllerBase
{
    private readonly MessageService _service = service;

    [HttpGet("{id}")]
    public async Task<IActionResult> FindAllUserMessages([FromRoute] Guid id)
    {
        try
        {
            List<MessageResponse> messages = await _service.FindAllUserMessages(id);
            return Ok(messages);
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

    [HttpGet("fromchat/{id}")]
    public async Task<IActionResult> FindAllUserMessagesFromAChat([FromRoute] Guid id, ChatRequest request)
    {
        try
        {
            List<MessageResponse> messages = await _service.FindAllUserMessagesFromAChat(id, request);
            return Ok(messages);
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
    public async Task<IActionResult> Create(MessageRequest request)
    {
        try
        {
            // Validator
            // JwtAuthorization

            MessageResponse message = await _service.Create(request);
            return StatusCode(201, message);
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Updte([FromRoute] long id, MessageRequest request)
    {
        try
        {
            // Validations
            // JWT

            MessageResponse message = await _service.Update(id, request);
            return Ok(message);
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById([FromRoute] long id, ChatRequest request)
    {
        try
        {
            await _service.DeleteById(id, request);
            return NoContent();
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
}
