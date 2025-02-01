public class ApplicantDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Nationality { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? CityOfBirth { get; set; }
    public string? CountryOfBirth { get; set; }
    public string? MobileNumber { get; set; }
    public string? WhatsAppNumber { get; set; }
    public string? Email { get; set; }
    public string? GermanKnowledge { get; set; }
    public string? Domain { get; set; }
    public string? DegreeType { get; set; }
    public string? University { get; set; }
    public int? YearsOfExperience { get; set; }
    public bool? KnowsPreviousAttendee { get; set; }
    public IFormFile PassportScan { get; set; }
    public IFormFile CV { get; set; }
}
