using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SalesTrack.Contracts.Dto;
using SalesTrack.Common.Models;
using System.Net.Http.Json;
using SalesTrack.Api.Contracts;

namespace SalesTrack.Api.Pages.Sales;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;

    public IndexModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public List<GetSalesResponseDto> Sales { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public int? Limit { get; set; } = 10;

    [BindProperty(SupportsGet = true)]
    public int? Offset { get; set; } = 0;

    [BindProperty]
    public AddSaleDto NewSale { get; set; } = new();

    public async Task OnGetAsync()
    {
        var queryParams = new Dictionary<string, string?>()
        {
            ["limit"] = Limit?.ToString(),
            ["offset"] = Offset?.ToString(),
            ["direction"] = 1.ToString(),
        };

        var queryString = string.Join("&", queryParams
            .Where(kv => kv.Value != null)
            .Select(kv => $"{kv.Key}={kv.Value}"));

        var url = $"https://localhost:7197/api/v1/Sale?{queryString}";

        var response = await _httpClient.GetFromJsonAsync<ApiResponseV1<List<GetSalesResponseDto>>>(url);

        Sales = response?.Data ?? [];
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7197/api/v1/Sale", NewSale);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage(); 
        }

        ModelState.AddModelError(string.Empty, "Ошибка при добавлении продажи.");
        await OnGetAsync(); 
        return Page();
    }
}
