using System;
using System.ComponentModel.DataAnnotations;
using bookstore.Models.Interfaces;

namespace bookstore.Models
{
    public class Book : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author Id is required")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        [Required(ErrorMessage = "Category Id is required")]
        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        [Required(ErrorMessage = "Release Date is required")]
        public DateTime? ReleaseDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}