using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using bookstore.Models.Interfaces;

namespace bookstore.Models
{
    public class Category : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}