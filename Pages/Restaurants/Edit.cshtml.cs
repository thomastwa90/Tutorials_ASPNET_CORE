using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OTF.Core;
using OTF.Data;

namespace OTF.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int? restaurantid)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            if (restaurantid.HasValue)
            {
                restaurant = restaurantData.GetById(restaurantid.Value);
            }
            else
            {
                restaurant = new Restaurant();
            }

            if (restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            //ModelState["Location"].AttemptedValue

            if (!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (restaurant.Id > 0)
            {
                restaurantData.Update(restaurant);
            }
            else
            {
                restaurantData.Add(restaurant);
            }
            restaurantData.Commit();
            TempData["Message"] = "Restaurant Saved";
            return RedirectToPage("./Detail", new { restaurantId = restaurant.Id });
        }
    }
}