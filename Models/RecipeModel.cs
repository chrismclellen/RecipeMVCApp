using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp.Models
{
    public class RecipeModel
    {
        public int RecipeID { get; set; }
        [DisplayName("Recipe Name")]
        [Required]
        public string RecipeName { get; set; }
        [DisplayName("Recipe Instructions")]
        [Required]
        public string RecipeInstructions { get; set; }
        public List<IngredientModel> Ingredients { get; set; }

    }
}
