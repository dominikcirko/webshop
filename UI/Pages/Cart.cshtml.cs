using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using webshopAPI.DTOs;

[Authorize]
public class CartModel : PageModel
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CartModel> _logger;


    public CartModel(IHttpClientFactory httpClientFactory, ILogger<CartModel> logger)
    {
        _httpClient = httpClientFactory.CreateClient("BackendAPI");
        _logger = logger;
    }

    public List<CartItemWithDetailsDTO> CartItemsWithDetails { get; set; } = new List<CartItemWithDetailsDTO>();

    public async Task OnGetAsync()
    {

        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
        if (userIdClaim == null)
        {
            throw new UnauthorizedAccessException("User ID claim is missing. Ensure the user is authenticated.");
        }

        if (!int.TryParse(userIdClaim.Value, out var userId))
        {
            throw new InvalidCastException("The User ID claim is not a valid integer.");
        }

        var cart = await FetchCartFromBackend(userId);
        foreach (var cartItem in cart.CartItems)
        {
            var itemDetails = await FetchItemDetailsAsync(cartItem.ItemID);
            CartItemsWithDetails.Add(new CartItemWithDetailsDTO
            {
                CartItem = cartItem,
                ItemDetails = itemDetails
            });
        }
    }

    public async Task<IActionResult> OnPostUpdateQuantityAsync(int index, int quantity)
    {
        await EnsureCartItemsLoaded();
        CartItemsWithDetails[index].CartItem.Quantity = quantity;
        await UpdateCartItemQuantityInBackend(CartItemsWithDetails[index].CartItem);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostRemoveItemAsync(int index)
    {
        await EnsureCartItemsLoaded();
        await RemoveCartItemFromBackend(CartItemsWithDetails[index].CartItem.IDCartItem);
        CartItemsWithDetails.RemoveAt(index);
        return RedirectToPage();
    }

    private int? GetUserIdFromClaims()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "id");
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            _logger.LogWarning("Failed to parse User ID from claims.");
            return null;
        }
        return userId;
    }
    private async Task<CartDTO> FetchCartFromBackend(int userId)
    {
        var response = await _httpClient.GetAsync($"/api/cart/users/{userId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CartDTO>();
    }

    private async Task UpdateCartItemQuantityInBackend(CartItemDTO cartItem)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/cart/{cartItem.IDCartItem}", cartItem);
        response.EnsureSuccessStatusCode();
    }

    private async Task RemoveCartItemFromBackend(int itemId)
    {
        var response = await _httpClient.DeleteAsync($"/api/cart/{itemId}");
        response.EnsureSuccessStatusCode();
    }

    private async Task<ItemDTO> FetchItemDetailsAsync(int itemId)
    {
        var response = await _httpClient.GetAsync($"/api/items/{itemId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ItemDTO>();
    }

    private async Task EnsureCartItemsLoaded()
    {
        if (CartItemsWithDetails == null || !CartItemsWithDetails.Any())
        {
            var userId = GetUserIdFromClaims();
            if (userId == null)
            {
                _logger.LogWarning("Attempt to load cart items with missing User ID.");
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var cart = await FetchCartFromBackend(userId.Value);
            foreach (var cartItem in cart.CartItems)
            {
                var itemDetails = await FetchItemDetailsAsync(cartItem.ItemID);
                CartItemsWithDetails.Add(new CartItemWithDetailsDTO
                {
                    CartItem = cartItem,
                    ItemDetails = itemDetails
                });
            }
        }
    }
}

public class CartItemWithDetailsDTO
{
    public CartItemDTO CartItem { get; set; }
    public ItemDTO ItemDetails { get; set; }
}