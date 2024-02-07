using Backend.Exceptions;
using Backend.Exceptions.ApplicationExceptions;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("/api/users")]
[ApiController]
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
        catch(ApplicationException e)
        {
            ExceptionResponse exception = new(0, Request.Path, Request.Method, e.Message);

            if (e is ResourceNotFoundException) exception.Status = 404;

            if (exception.Status == 0) throw new Exception();
            return StatusCode(exception.Status, exception);
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
}
