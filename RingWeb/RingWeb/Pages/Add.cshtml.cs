using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RingWeb;
using System.Text;
using System.Text.Json;

namespace RingWeb.Pages
{
    public class AddModel : PageModel
    {

        [BindProperty]
        public Ring Ring { get; set; } = new Ring();

        public bool IsEdit { get; set; }

        private string API_URL = "http://localhost:5159/api/ring";

        public List<string> aneis = new() { "https://ae01.alicdn.com/kf/S3d7a45bd7b3f4b608f70e57b3070e035y.jpg_640x640q90.jpg",
                                            "https://dionjoias.com/cdn/shop/files/tAUmS3096f4251d9c4ea5ae6ae1310f13d817v.jpg?v=1725991324",
                                            "https://ae01.alicdn.com/kf/S3bcce296b2ad45a2a4436085b12bb7aem.jpg_640x640q90.jpg",
                                            "https://www.theonestore.com.br/cdn/shop/collections/1658368158255.png?v=1713085602"
        };

        private readonly HttpClient _httpClient;

        public AddModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
            
                var response = await _httpClient.GetAsync(API_URL + "/" + id);

                if (response.IsSuccessStatusCode)
                {

                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    Ring = JsonSerializer.Deserialize<Ring>(jsonResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true 
                    });

                    IsEdit = true;
                }
          
            }
            else
            {
                IsEdit = false;
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
        
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var jsonContent = JsonSerializer.Serialize(Ring);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
           
            if (id.HasValue)
            {

                var responsePut = await _httpClient.PutAsync(API_URL, content);

                if (!responsePut.IsSuccessStatusCode)
                {

                    var errorMessage = await responsePut.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Erro ao atualizar o anel. Detalhes: {errorMessage}");
                    return Page();
                }
            
                TempData["SuccessMessage"] = "Anel atualizado com sucesso!";
            }
            else
            {
             
                var responsePost = await _httpClient.PostAsync(API_URL, content);
       
                if (!responsePost.IsSuccessStatusCode)
                {
                    var errorMessage = await responsePost.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty,$"Erro ao criar o anel. Detalhes: {errorMessage}");
                    return Page();
                }


                TempData["SuccessMessage"] = "Anel adicionado com sucesso!";
            }

            return RedirectToPage("/Index"); 
        
        }
    }

}