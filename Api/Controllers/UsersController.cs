using Application.Interfaces;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetUserById(Guid id)
    {
        var user = await _service.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return user;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers([FromQuery] Query query)
    {
        var SUPPORTED_QUERY = new string[] { "country", "name", "email", "age" };
        var BAD_QUERY_MSG = new { Message = $"Only one query param is supported, and it must be one of the following: {string.Join(", ", SUPPORTED_QUERY)}" };
        IEnumerable<User> users = new List<User>();

        var queryCount = query.GetType().GetProperties().Where(p => p.GetValue(query, null) != null).Count();
        if (queryCount > 1 || queryCount == 0)
        {
            return BadRequest(BAD_QUERY_MSG);
        }

        if (query.Country != null)
        {
            users = await _service.GetUsersByCountry(query.Country);
        }

        if (query.Age > 0)
        {
            int age = query.Age ?? 0;
            users = await _service.GetUsersByAge(age);
        }
        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            if (query.Name.Trim().Split(" ").Length > 1)
            {
                users = await _service.GetUsersByName(query.Name);
            }
            else
            {
                users = await _service.GetUsersByPrefixName(query.Name);
            }
        }

        if (users == null || users.Count() == 0)
        {
            return NotFound();
        }


        return Ok(users);


    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        await _service.Remove(id);
        return NoContent();
    }
}
