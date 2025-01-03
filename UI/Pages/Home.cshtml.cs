using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using webshopAPI.DTOs;

public class HomeModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public string SearchQuery { get; set; } = "";
    public List<ItemDTO> Items { get; set; } = new List<ItemDTO>();
    public List<ItemCategoryDTO> Categories { get; set; } = new List<ItemCategoryDTO>();

    public async Task OnGetAsync(string search = "", int categoryId = 0)
    {
        SearchQuery = search;

        var client = _httpClientFactory.CreateClient("BackendAPI");

        string apiUrl = "/api/item";
        if (!string.IsNullOrEmpty(search))
        {
            apiUrl += $"?title={search}";
        }
        else if (categoryId > 0)
        {
            apiUrl += $"?categoryId={categoryId}";
        }

        var response = await client.GetAsync(apiUrl);
        if (response.IsSuccessStatusCode)
        {
            Items = await response.Content.ReadFromJsonAsync<List<ItemDTO>>();
        }
        else
        {
            Items = new List<ItemDTO>();
        }

        var categoryResponse = await client.GetAsync("/api/itemcategory");
        if (categoryResponse.IsSuccessStatusCode)
        {
            Categories = await categoryResponse.Content.ReadFromJsonAsync<List<ItemCategoryDTO>>();
        }
        else
        {
            Categories = new List<ItemCategoryDTO>();
        }
    }
}
