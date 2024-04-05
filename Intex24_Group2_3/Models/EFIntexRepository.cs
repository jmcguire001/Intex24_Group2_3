using Intex24_Group2_3.Data;

namespace Intex24_Group2_3.Models
{
    public class EFIntexRepository : IIntexRepository
    {
        private readonly ShoppingContext _shoppingContext;

        public EFIntexRepository(ShoppingContext shoppingContext)
        {
            _shoppingContext = shoppingContext;
        }

        public IQueryable<Project> Projects => _shoppingContext.Projects;
    }
}
