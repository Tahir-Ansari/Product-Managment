using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Category
    {
        public Category()
        {
            CreatedOn = new DateTime();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}