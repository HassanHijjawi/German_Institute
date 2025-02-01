public static class ApplicantMapper
{
    public static Applicant ToEntity(ApplicantDto dto)
    {
        using var passportStream = new MemoryStream();
        using var cvStream = new MemoryStream();

        dto.PassportScan.CopyTo(passportStream);
        dto.CV.CopyTo(cvStream);

        return new Applicant
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Nationality = dto.Nationality,
            DateOfBirth = dto.DateOfBirth,
            CityOfBirth = dto.CityOfBirth,
            CountryOfBirth = dto.CountryOfBirth,
            MobileNumber = dto.MobileNumber,
            WhatsAppNumber = dto.WhatsAppNumber,
            Email = dto.Email,
            GermanKnowledge = dto.GermanKnowledge,
            Domain = dto.Domain,
            DegreeType = dto.DegreeType,
            University = dto.University,
            YearsOfExperience = dto.YearsOfExperience.Value,
            KnowsPreviousAttendee = dto.KnowsPreviousAttendee.Value,
            PassportScan = passportStream.ToArray(),
            CV = cvStream.ToArray()
        };
    }

    public static ApplicantDto ToDto(Applicant entity)
    {
        return new ApplicantDto
        {
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Nationality = entity.Nationality,
            DateOfBirth = entity.DateOfBirth,
            CityOfBirth = entity.CityOfBirth,
            CountryOfBirth = entity.CountryOfBirth,
            MobileNumber = entity.MobileNumber,
            WhatsAppNumber = entity.WhatsAppNumber,
            Email = entity.Email,
            GermanKnowledge = entity.GermanKnowledge,
            Domain = entity.Domain,
            DegreeType = entity.DegreeType,
            University = entity.University,
            YearsOfExperience = entity.YearsOfExperience,
            KnowsPreviousAttendee = entity.KnowsPreviousAttendee
        };
    }
}
