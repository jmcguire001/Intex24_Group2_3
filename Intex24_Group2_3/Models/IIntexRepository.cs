namespace Intex24_Group2_3.Models
{
    public interface IIntexRepository
    {
        public IQueryable<Project> Projects { get; }
    }
}
