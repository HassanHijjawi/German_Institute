using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

[Route("api/alumni")]
[ApiController]
[AllowAnonymous]
public class AlumniController : ControllerBase
{
    private readonly MyDbContext _context;

    public AlumniController(MyDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAlumni([FromForm] AlumniDto dto)
    {
        if (dto.Image == null || !IsImageFile(dto.Image))
            return BadRequest("Image must be a JPG or JPEG file.");

        using var memoryStream = new MemoryStream();
        await dto.Image.CopyToAsync(memoryStream);

        var alumni = new AlumniPosts
        {
            Title = dto.Title,
            Description = dto.Description,
            Date = dto.Date,
            ImageData = memoryStream.ToArray(),
            ImageExtension = Path.GetExtension(dto.Image.FileName).ToLowerInvariant()
        };

        _context.AlumniPosts.Add(alumni);
        await _context.SaveChangesAsync();

        return Ok(new { message = "AlumniPosts created successfully" });
    }

    private bool IsImageFile(IFormFile file)
    {
        if (file.ContentType != "image/jpeg" && file.ContentType != "image/jpg")
            return false;

        var allowedExtensions = new[] { ".jpg", ".jpeg" };
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return allowedExtensions.Contains(fileExtension);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlumniDto>>> GetAlumni()
    {
        var alumniList = await _context.AlumniPosts
            .OrderByDescending(a => a.Date)
            .Select(a => new AlumniDto
            {
                Title = a.Title,
                Description = a.Description,
                Date = a.Date,
                ImageUrl = $"/api/alumni/{a.Id}/image"
            })
            .ToListAsync();

        return Ok(alumniList);
    }

    [HttpGet("{id}/image")]
    public async Task<IActionResult> GetAlumniImage(int id)
    {
        var alumni = await _context.AlumniPosts.FindAsync(id);
        if (alumni == null || alumni.ImageData == null)
            return NotFound();

        return File(alumni.ImageData, "image/jpeg", $"alumni_{id}{alumni.ImageExtension}");
    }
}
