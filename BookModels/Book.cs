using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookModels
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(255)]
        [Required(ErrorMessage = "Insert the title of the book")]
        public  string Title { get; set; }
        
        public string Description { get; set; }
    }
}  