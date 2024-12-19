using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using RingWeb;
using Microsoft.AspNetCore.Mvc;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;

    public IndexModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private string API_URL = "http://localhost:5159/api/ring";

    public List<Ring> Rings { get; set; } = new List<Ring>();

    public async Task OnGetAsync()
    {
        var response = await _httpClient.GetAsync(API_URL);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            Rings = JsonSerializer.Deserialize<List<Ring>>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }


    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
      
        var response = await _httpClient.DeleteAsync(API_URL + '/' + id);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Erro ao excluir o anel.");
            return Page();
        }

        TempData["SuccessMessage"] = "Anel excluído com sucesso!";
        return RedirectToPage("/Index"); // Redireciona para a página inicial ou outra página

    }
}
