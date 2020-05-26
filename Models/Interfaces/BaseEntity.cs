using System;

namespace bookstore.Models.Interfaces
{
    public interface BaseEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
    }
}