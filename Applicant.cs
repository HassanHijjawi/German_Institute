using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("applicant")]
public class Applicant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Nationality { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public string CityOfBirth { get; set; }

    [Required]
    public string CountryOfBirth { get; set; }

    [Required]
    public string MobileNumber { get; set; }

    public string WhatsAppNumber { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string GermanKnowledge { get; set; } 

    [Required]
    public string Domain { get; set; } 

    [Required]
    public string DegreeType { get; set; } 

    [Required]
    public string University { get; set; }

    [Required]
    public int YearsOfExperience { get; set; }

    public bool KnowsPreviousAttendee { get; set; }

    public byte[] PassportScan { get; set; }

    public byte[] CV { get; set; }

    public DateTime DateofApplication { get; set; } = DateTime.UtcNow;

}
