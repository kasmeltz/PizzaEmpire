using System.Collections.Generic;

namespace KS.PizzaEmpire.GameLogic.ItemLogic
{
    public class Recipe
    {
        public int ItemCode { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
