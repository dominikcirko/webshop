using Microsoft.AspNetCore.Mvc.RazorPages;
using webshopAPI.DTOs;

public class ItemModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ItemModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public ItemDTO Item { get; set; }
    public string ItemCategoryName { get; set; } = "Unknown";
    public string TagName { get; set; } = "Unknown";

    public async Task OnGetAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("BackendAPI");

        var response = await client.GetAsync($"/api/item/{id}");
        if (response.IsSuccessStatusCode)
        {
            Item = await response.Content.ReadFromJsonAsync<ItemDTO>();
        }

        if (Item != null)
        {
            var categoryResponse = await client.GetAsync($"/api/itemcategory/{Item.ItemCategoryID}");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var category = await categoryResponse.Content.ReadFromJsonAsync<ItemCategoryDTO>();
                ItemCategoryName = category?.CategoryName ?? "Unknown";
            }

            if (Item.TagID.HasValue)
            {
                var tagResponse = await client.GetAsync($"/api/tag/{Item.TagID.Value}");
                if (tagResponse.IsSuccessStatusCode)
                {
                    var tag = await tagResponse.Content.ReadFromJsonAsync<TagDTO>();
                    TagName = tag?.Name ?? "Unknown";
                }
            }
        }
    }
}
