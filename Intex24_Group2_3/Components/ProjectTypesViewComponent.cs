using Microsoft.AspNetCore.Mvc;
using Intex24_Group2_3.Models;

namespace Intex24_Group2_3.Components
{
    public class ProjectTypesViewComponent : ViewComponent
    {
        private IShoppingRepository _repo;

        // Constructor
        public ProjectTypesViewComponent(IShoppingRepository temp)
        {
            _repo = temp;
        }

        // This method gets specific projects based on unique project types
        public IViewComponentResult Invoke()
        {
            // Get the project type from the URL; store it in the ViewBag
            ViewBag.SelectedProjectType = RouteData?.Values["projectType"]; // RouteData is a dictionary that holds the URL info

            var projectTypes = _repo.Projects
                .Select(x => x.ProjectType)
                .Distinct()
                .OrderBy(x => x);

            // Return to the default view
            return View(projectTypes);
        }
    }
}
