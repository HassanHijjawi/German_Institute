using GermanInstitute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

[ApiController]
[Route("api/contact")]
public class ContactController : ControllerBase
{
    private readonly MyDbContext _context;

    public ContactController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ContactMessageDto>>> GetMessages()
    {
        var messages = await _context.ContactMessages.ToListAsync();
        return messages.Select(m => new ContactMessageDto
        {
            Name = m.Name,
            Email = m.Email,
            Subject = m.Subject,
            Message = m.Message,
            Id = m.Id   

        }).ToList();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<ContactMessage>> CreateMessage([FromBody]ContactMessageDto messageDto)
    {
        var message = new ContactMessage
        {
            Name = messageDto.Name,
            Email = messageDto.Email,
            Subject = messageDto.Subject,
            Message = messageDto.Message
        };

        _context.ContactMessages.Add(message);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMessages), new { id = message.Id }, message);
    }
}
