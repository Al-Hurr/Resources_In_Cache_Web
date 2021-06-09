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
    public class IndexModel : PageModel
    {
        private ResourceService _service;

        public IndexModel(ResourceService service)
        {
            _service = service;
        }

        public List<Resource> Resources { get; set; }

        public async Task OnGet()
        {
            Resources = await Task.Run(() => _service.GetAll().ToList());
        }
        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            await Task.Run(() => _service.Remove(id));  
            return RedirectToPage("Index");
        }
    }
}