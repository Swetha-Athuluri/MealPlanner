using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
   public interface IIngredientDAL
    {
        string GetIngredientName(int ingredientId);
    }
}
