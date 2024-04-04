namespace Intex24_Group2_3.Models
{
    public interface IShoppingRepository
    {
        public IQueryable<Project> Projects { get; }
    }
}
