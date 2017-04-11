using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface IPreparationStepsDAL
    {
        List<PreparationSteps> GetPreparationStepsForRecipe(int recipeId);
        void SavePreparationSteps(int recipeId, List<string> prepSteps, PreparationSteps prepStep);
        void DeleteFromPreparationSteps(int recipeId);
    }
}
