using Intex24_Group2_3.Data;

namespace Intex24_Group2_3.Models
{
    public class EFShoppingRepository : IShoppingRepository
    {
        private readonly ShoppingContext _shoppingContext;

        public EFShoppingRepository(ShoppingContext shoppingContext)
        {
            _shoppingContext = shoppingContext;
        }

        public IQueryable<Project> Projects => _shoppingContext.Projects;
    }
}
