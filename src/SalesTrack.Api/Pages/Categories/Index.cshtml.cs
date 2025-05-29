using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SalesTrack.Api.Contracts;
using SalesTrack.Common.Models;
using System.Net.Http.Json;

namespace SalesTrack.Api.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;

    public IndexModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public List<CategoryDto> Categories { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string Search { get; set; } = "";

    [BindProperty(SupportsGet = true)]
    public int? Limit { get; set; } = 10;

    [BindProperty(SupportsGet = true)]
    public int? Offset { get; set; } = 0;

    [BindProperty]
    public AddCategoryDto NewCategory { get; set; } = new();

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

        var url = $"https://localhost:7197/api/v1/Category?{queryString}";

        var response = await _httpClient.GetFromJsonAsync<ApiResponseV1<GetCategoriesResponseDto>>(url);

        Categories = response?.Data?.Categories ?? new List<CategoryDto>();
    }

    public async Task<IActionResult> OnPostAddCategoryAsync()
    {
        if (!ModelState.IsValid)
        {
            await OnGetAsync();
            return Page();
        }

        var url = "https://localhost:7197/api/v1/Category";

        var response = await _httpClient.PostAsJsonAsync(url, NewCategory);

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
            ModelState.AddModelError(string.Empty, "Ошибка при добавлении категории");
            await OnGetAsync();
            return Page();
        }
    }
}

public class AddCategoryDto
{
    public string Name { get; set; } = "";
}

public class GetCategoriesResponseDto
{
    public List<CategoryDto> Categories { get; set; } = new();
}

public class CategoryDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";
}