public class AlumniDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public IFormFile Image { get; set; }
    public string? ImageUrl { get; set; }
    public int? Id { get; set; }
    public bool? IsEventPost { get; set; }

}