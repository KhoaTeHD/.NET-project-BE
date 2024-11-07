﻿using System.ComponentModel.DataAnnotations;

namespace Services.ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Cat_Id { get; set; }
        [Required]
        public int Nat_Id { get; set; }
        [Required]
        public int Bra_Id { get; set; }
        [Required]
        public int Sup_Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Boolean Status { get; set; }
    }
}
