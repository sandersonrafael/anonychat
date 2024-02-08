using Backend.Exceptions;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController(UserService service) : ControllerBase
{
    private readonly UserService _service = service;

    [HttpGet("{id}")]
    public async Task<IActionResult> FindById([FromRoute] Guid id)
    {
        try
        {
            // ValidationErrors errors = ValidationErrors.ValidateUser(user);
            // if (errors != null) return BadRequest(new ExceptionResponse(...));

            UserResponse dbUser = await _service.FindbyId(id);
            return Ok(dbUser);
        }
        catch(ApiException e)
        {
            return StatusCode(e.Status, new ExceptionResponse(e.Status, Request.Path, Request.Method, e.Message));
        }
        catch (Exception)
        {
            return StatusCode(500, ExceptionResponse.Internal(Request.Path, Request.Method));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserRequest user)
    {
        try
        {
            // ValidationErrors errors = ValidationErrors.ValidateUser(user);
            // if (errors != null) return BadRequest(new ExceptionResponse(...));

            UserResponse newUser = await _service.Create(user);
            return Created($"{Request.Path}/{newUser.Id}", newUser);
        }
        catch(Exception)
        {
            return StatusCode(500, ExceptionResponse.Internal(Request.Path, Request.Method));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UserRequest user)
    {
        try
        {
            // ValidationErrors errors = ValidationErrors.ValidateUser(user);
            // if (errors != null) return BadRequest(new ExceptionResponse(...));

            UserResponse dbUser = await _service.Update(id, user);
            return Ok(dbUser);
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
