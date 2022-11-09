using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookModels
{
    [Table("Books")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [MaxLength(255)]
        [Required(ErrorMessage = "Insert the title of the book")]
        public  string Title { get; set; }
        
        public string Description { get; set; }

        [DefaultValue("DateTime()")]
        public DateTime Created { get; set; }
    }
}  