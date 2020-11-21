using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp.Models
{
    public class IngredientModel
    {
        public int IngredientID { get; set; }
        public int RecipeID { get; set; }
        [Required]
        public string IngredientName { get; set; }
        [Range(1, 1000)]
        public int Quantity { get; set; }
        [Required]
        public string UOM { get; set; }
    }
}

