using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Text;
using System.Text.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using WiredBrainCoffeeAdmin.Data.Models;

namespace WiredBrainCoffeeAdmin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebHostEnvironment _env;

        public List<SurveyItem> SurveyResults { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public void OnGet()
        {
            var rawJson = System.IO.File
                .ReadAllText(Path.Combine(_env.ContentRootPath,
                    "wwwroot/sampledata/survey.json"));

            SurveyResults = JsonSerializer.Deserialize<List<SurveyItem>>(rawJson);
        }
    }
}