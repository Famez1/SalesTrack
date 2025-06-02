using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SalesTrack.Contracts.Dto;
using SalesTrack.Api.Contracts;

namespace SalesTrack.Api.Pages.Products;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;

    public IndexModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public List<GetProductsResponseDto.ProductInfoModel> Products { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public Guid? CategoryId { get; set; }

    [BindProperty(SupportsGet = true)]
    public string CategoryName { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? Limit { get; set; } = 10;

    [BindProperty(SupportsGet = true)]
    public int? Offset { get; set; } = 0;

    public async Task OnGetAsync()
    {
        var queryParams = new Dictionary<string, string?>()
        {
            ["limit"] = Limit?.ToString(),
            ["offset"] = Offset?.ToString(),
            ["direction"] = "1", 
        };

        if (!string.IsNullOrEmpty(CategoryName))
        {
            queryParams["CategoryName"] = CategoryName;
        }

        var queryString = string.Join("&", queryParams
            .Where(kv => !string.IsNullOrEmpty(kv.Value))
            .Select(kv => $"{kv.Key}={System.Net.WebUtility.UrlEncode(kv.Value)}"));

        var url = $"https://localhost:7197/api/v1/Product?{queryString}";

        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponseV1<GetProductsResponseDto>>(url);

            Products = response?.Data?.Products ?? new List<GetProductsResponseDto.ProductInfoModel>();
        }
        catch (Exception ex)
        {
            Products = new();
        }
    }
}
