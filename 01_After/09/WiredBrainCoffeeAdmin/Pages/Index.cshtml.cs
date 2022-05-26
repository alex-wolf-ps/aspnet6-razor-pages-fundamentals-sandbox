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
        private readonly IHttpClientFactory factory;
        private readonly IWebHostEnvironment _env;

        public IDictionary<string, string> OrderStats { get; set; }

        public List<SurveyItem> SurveyResults { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpFactory, IWebHostEnvironment env)
        {
            _logger = logger;
            this.factory = httpFactory;
            _env = env;
        }

        public async Task<IActionResult> OnGet()
        {
            var client = this.factory.CreateClient();
            var response = await client
                .GetAsync("https://wiredbraincoffeeadmin.azurewebsites.net/api/orderStats");

            var responseData = await response.Content.ReadAsStringAsync();
            OrderStats = JsonSerializer.Deserialize<IDictionary<string, string>>(responseData);

            var rawJson = System.IO.File
               .ReadAllText(Path.Combine(_env.ContentRootPath,
                   "wwwroot/sampledata/survey.json"));

            SurveyResults = JsonSerializer.Deserialize<List<SurveyItem>>(rawJson);

            return Page();
        }
    }
}