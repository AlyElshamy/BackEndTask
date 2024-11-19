using AdminStaff.Entities;
using AdminStaff.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AdminStaff.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
      

        public IndexModel(ILogger<IndexModel> logger )
        {
            _logger = logger;
          

    }
        public void OnGet()
        {

        }
       
    }
}
