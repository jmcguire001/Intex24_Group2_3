using Intex24_Group2_3.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Intex24_Group2_3.Models;

namespace Intex24_Group2_3.Pages
{
    public class CartModel : PageModel
    {
        private IShoppingRepository _repo;

        public CartModel(IShoppingRepository temp)
        {
            _repo = temp;
        }

        public Cart? Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(int projectId)
        {
            Project proj = _repo.Projects
                .FirstOrDefault(x => x.ProjectId == projectId);

            if (proj != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(proj, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }

            return RedirectToPage(new { returnUrl = ReturnUrl });
        }
    }
}