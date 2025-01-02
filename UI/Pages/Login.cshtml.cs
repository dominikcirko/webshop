using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

public class LoginModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LoginModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public string Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("BackendAPI");

            var requestData = new { Email = Email, Password = Password };
            var response = await client.PostAsJsonAsync("/api/Auth/login", requestData);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                var token = jsonResponse["token"];
                return RedirectToPage("/Index");
            }
            else
            {
                Message = $"Error: {response.StatusCode} - {responseContent}";
            }
        }
        catch (Exception ex)
        {
            Message = $"An error occurred: {ex.Message}";
        }
        return Page();
    }
}