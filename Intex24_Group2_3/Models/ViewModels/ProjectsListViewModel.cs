namespace Intex24_Group2_3.Models.ViewModels
{
    public class ProjectsListViewModel
    {
        public IQueryable<Project> Projects { get; set; } // This is a collection of <Project> instances
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo(); // WHY DO WE NOT NEED TO <> THIS? We don't need <> because PaginationInfo is just a class, not a collection
        public string? CurrentProjectType { get; set; } // We can set the current project type
    }
}
