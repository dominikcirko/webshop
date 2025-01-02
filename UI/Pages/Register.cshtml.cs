using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class RegisterModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public RegisterModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    [BindProperty]
    public string FirstName { get; set; }

    [BindProperty]
    public string LastName { get; set; }

    [BindProperty]
    public string PhoneNumber { get; set; }

    public string Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("BackendAPI");
            var response = await client.PostAsJsonAsync("/api/Auth/register", new
            {
                Username,
                Email,
                Password,
                FirstName,
                LastName,
                PhoneNumber
            });

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                Message = "Registration failed. Try again.";
            }
        }
        catch (Exception ex)
        {
            Message = $"An error occurred: {ex.Message}";
        }

        return Page();
    }
}
