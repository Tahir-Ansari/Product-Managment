using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }


        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(2500)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string SKU { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public double SellingPrice { get; set; }

        [Required]
        public int AvailableQuantity { get; set; }

        public Type Type { get; set; }

        public double? Weight { get; set; }

        public double? Length { get; set; }

        public double? Width { get; set; }

        public double? Height { get; set; }

        public double ShippingCost { get; set; }

        //foreign key

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
    public enum Type { Physical, Virtual }
}