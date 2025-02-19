using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("alumni-posts")]
public class AlumniPosts
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public byte[] ImageData { get; set; }

    [Required]
    public string ImageExtension { get; set; }
    [Required]
    public bool? IsEventPost { get; set; }
}