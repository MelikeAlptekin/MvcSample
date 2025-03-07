using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MvcLab4.Entities
{
   

    [Table("KategoriTablosu")]
    public class Category
    {
        public Category()
        {
          CategoryId = Guid.NewGuid().ToString();
        }

        [Key]
        public string CategoryId { get; set; }

        [MaxLength(50)]
        public string CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
