using Intex24_Group2_3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Intex24_Group2_3.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Intex24_Group2_3.Controllers
{
    public class HomeController : Controller
    {
        private IShoppingRepository _repo;

        public HomeController(IShoppingRepository temp)
        {
            _repo = temp;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Shop(int pageNum, string? projectType) // 'page' means something in dotnet
        {
            int pageSize = 1; // How many items to show per page

            // This variable will hold everything from ProjectsListViewModel, and then be passed to Index.cshtml
            var listData = new ProjectsListViewModel
            {
                // This info is for the projects specifically
                Projects = _repo.Projects
                    .Where(x => x.ProjectType == projectType || projectType == null) // If projectType is null, show all projects
                    .OrderBy(x => x.ProjectName)
                    .Skip((pageNum - 1) * pageSize) // NOT SURE WHAT THIS DOES
                    .Take(pageSize), // Only gets a certain number of projects

                // This info is for pagination
                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = projectType == null ? _repo.Projects.Count() : _repo.Projects.Where(x => x.ProjectType == projectType).Count() // If projectType is null, show all projects, otherwise, filter specific projects
                },

                CurrentProjectType = projectType
            };

            return View(listData);
        }
    }
}
