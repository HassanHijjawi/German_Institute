using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GermanInstitute
{
    [Table("contact-message")]
    public class ContactMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Subject { get; set; }
         public string Message { get; set; }   

        public DateTime SentDate { get; set; } = DateTime.UtcNow;
    }
}
