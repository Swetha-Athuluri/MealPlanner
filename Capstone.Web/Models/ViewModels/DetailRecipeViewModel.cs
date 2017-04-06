using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models.ViewModels
{
    public class DetailRecipeViewModel
    {
        public Recipe Recipe { get; set; }
        public PreparationSteps Preparationstep { get; set; }
        public RecipeIngredient RecipeIngredient { get; set; }
    }
}