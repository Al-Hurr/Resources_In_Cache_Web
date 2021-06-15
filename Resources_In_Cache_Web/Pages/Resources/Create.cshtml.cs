using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Resources_In_Cache_Web.Models;
using Resources_In_Cache_Web.Services;

namespace Resources_In_Cache_Web.Pages.Resources
{
    public class CreateModel : PageModel
    {
        private ResourceService _service;

        public CreateModel(ResourceService service)
        {
            _service = service;
        }

        [BindProperty]
        public ResourceCreateModel ResourceCreateModel { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            await _service.Create(ResourceCreateModel.Title);
            return RedirectToPage("Index");
        }
    }
}
