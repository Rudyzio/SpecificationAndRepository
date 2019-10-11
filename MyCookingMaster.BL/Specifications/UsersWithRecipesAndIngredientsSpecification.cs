using MyCookingMaster.BL.Models;

namespace MyCookingMaster.BL.Specifications
{
    public class UsersWithRecipesAndIngredientsSpecification : BaseSpecification<User>
    {
        public UsersWithRecipesAndIngredientsSpecification() : base()
        {
            AddInclude(x => x.Recipes);
            AddInclude($"{nameof(User.Recipes)}.{nameof(Recipe.Ingredients)}");
        }

        public UsersWithRecipesAndIngredientsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Recipes);
            AddInclude($"{nameof(User.Recipes)}.{nameof(Recipe.Ingredients)}");
        }
    }
}
