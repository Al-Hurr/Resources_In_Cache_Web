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
    public class EditModel : PageModel
    {
        private ResourceService _service;

        public EditModel(ResourceService service)
        {
            _service = service;
        }

        [BindProperty]
        public ResourceCreateModel ResourceCreateModel { get; set; }
        = new ResourceCreateModel();

        public async Task<IActionResult> OnGet(Guid id)
        {
            ResourceCreateModel.Title = await Task.Run(() => _service.Get(id).Title);
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            await Task.Run(() => _service.Update(id, ResourceCreateModel.Title));
            return RedirectToPage("Index");
        }
    }
}
