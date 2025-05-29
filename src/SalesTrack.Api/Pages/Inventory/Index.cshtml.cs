using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SalesTrack.Contracts.Dto;
using SalesTrack.Common.Models;
using System.Net.Http.Json;
using SalesTrack.Api.Contracts;

namespace SalesTrack.Api.Pages.Inventory;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;

    public IndexModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public List<GetInventoriesResponseDto.InventoriesInfoModel> Inventories { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string Search { get; set; } = "";

    [BindProperty(SupportsGet = true)]
    public int? Limit { get; set; } = 10;

    [BindProperty(SupportsGet = true)]
    public int? Offset { get; set; } = 0;

    [BindProperty]
    public AddInventoryDto NewInventory { get; set; } = new();

    public async Task OnGetAsync()
    {
        var queryParams = new Dictionary<string, string?>()
        {
            ["limit"] = Limit?.ToString(),
            ["offset"] = Offset?.ToString(),
            ["direction"] = "1",
            ["search"] = string.IsNullOrWhiteSpace(Search) ? null : Search
        };

        var queryString = string.Join("&", queryParams
            .Where(kv => !string.IsNullOrEmpty(kv.Value))
            .Select(kv => $"{kv.Key}={System.Net.WebUtility.UrlEncode(kv.Value)}"));

        var url = $"https://localhost:7197/api/v1/Inventory?{queryString}";

        var response = await _httpClient.GetFromJsonAsync<ApiResponseV1<GetInventoriesResponseDto>>(url);

        Inventories = response?.Data?.Inventories ?? new List<GetInventoriesResponseDto.InventoriesInfoModel>();
    }

    public async Task<IActionResult> OnPostAddInventoryAsync()
    {
        if (!ModelState.IsValid)
        {
            await OnGetAsync();
            return Page();
        }

        var url = "https://localhost:7197/api/v1/Inventory";

        var response = await _httpClient.PutAsJsonAsync(url, NewInventory);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage(new
            {
                Search = this.Search,
                Limit = this.Limit,
                Offset = this.Offset
            });
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Ошибка при добавлении товара на склад");
            await OnGetAsync();
            return Page();
        }
    }
}
