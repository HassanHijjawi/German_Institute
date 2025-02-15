using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/applicants")]
[ApiController]
[AllowAnonymous]
public class ApplicantsController : ControllerBase
{
    private readonly MyDbContext _context;

    public ApplicantsController(MyDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateApplicant([FromForm] ApplicantDto dto)
    {
        if (dto.PassportScan == null || dto.CV == null)
            return BadRequest("Passport and CV are required");

        if (dto.CV == null || !IsPdfFile(dto.CV))
            return BadRequest("CV must be a PDF file.");

        if (dto.PassportScan == null || !IsPdfFile(dto.PassportScan))
            return BadRequest("Passport scan must be a PDF file.");


        var applicant = ApplicantMapper.ToEntity(dto);
        _context.Applicants.Add(applicant);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Applicant created successfully" , Id = applicant.Id});
    }
    private bool IsPdfFile(IFormFile file)
    {
        if (file.ContentType != "application/pdf")
            return false;

        var allowedExtensions = new[] { ".pdf" };
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return allowedExtensions.Contains(fileExtension);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplicant(int id)
    {
        var applicant = await _context.Applicants.FindAsync(id);
        if (applicant == null)
            return NotFound();

        var dto = ApplicantMapper.ToDto(applicant);
        return Ok(dto);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IActionResult>>> GetApplicants()
    {
        var applicants = await _context.Applicants 
            .Select(a => new ApplicantDto
            {

                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Nationality = a.Nationality,
                DateOfBirth = a.DateOfBirth,
                CityOfBirth = a.CityOfBirth,
                CountryOfBirth = a.CountryOfBirth,
                MobileNumber = a.MobileNumber,
                WhatsAppNumber = a.WhatsAppNumber,
                Email = a.Email,
                GermanKnowledge = a.GermanKnowledge,
                Domain = a.Domain,
                DegreeType = a.DegreeType,
                University = a.University,
                YearsOfExperience = a.YearsOfExperience,
                KnowsPreviousAttendee = a.KnowsPreviousAttendee
            }).ToListAsync();

        return Ok(applicants);
    }

    [HttpGet("{id}/cv")]
    public async Task<IActionResult> DownloadCv(int id)
    {
        var applicant = await _context.Applicants
            .Where(a => a.Id == id)
            .Select(a => new { a.CV, a.FirstName, a.LastName })
            .FirstOrDefaultAsync();

        if (applicant == null || applicant.CV == null)
            return NotFound();

        return File(applicant.CV, "application/octet-stream", $"{applicant.FirstName}_{applicant.LastName}_CV.pdf");
    }

    [HttpGet("{id}/passport")]
    public async Task<IActionResult> DownloadPassport(int id)
    {
        var applicant = await _context.Applicants
            .Where(a => a.Id == id)
            .Select(a => new { a.PassportScan, a.FirstName, a.LastName })
            .FirstOrDefaultAsync();

        if (applicant == null || applicant.PassportScan == null)
            return NotFound();

        return File(applicant.PassportScan, "application/octet-stream", $"{applicant.FirstName}_{applicant.LastName}_PassportScan.pdf");
    }
}
