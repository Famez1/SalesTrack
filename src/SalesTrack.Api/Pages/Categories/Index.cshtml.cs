using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SalesTrack.Api.Contracts;
using SalesTrack.Common.Models;
using SalesTrack.Contracts.Dto;

namespace SalesTrack.Api.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;

    public List<GetCategoriesResponseDto.CategoryInfoModel> Categories { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int? Limit { get; set; } = 10;

    [BindProperty(SupportsGet = true)]
    public int? Offset { get; set; } = 0;

    [BindProperty(SupportsGet = true)]
    public OrderDirectionEnum? Direction { get; set; } = OrderDirectionEnum.ASC;

    [BindProperty(Name = "NewCategoryName")]
    public string? NewCategoryName { get; set; }

    public IndexModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task OnGetAsync()
    {
        // Собираем query-параметры
        var queryParams = new Dictionary<string, string?>()
        {
            ["limit"] = Limit?.ToString(),
            ["direction"] = 1.ToString(),
            ["offset"] = Offset?.ToString(),
        };

        // Формируем строку запроса
        var queryString = string.Join("&", queryParams
            .Where(kv => kv.Value != null)
            .Select(kv => $"{kv.Key}={kv.Value}"));

        var url = $"https://localhost:7197/api/v1/Category?{queryString}";

        // Выполняем GET запрос и десериализуем ответ
        var response = await _httpClient.GetFromJsonAsync<ApiResponseV1<GetCategoriesResponseDto>>(url);

        Categories = response?.Data?.Categories ?? new List<GetCategoriesResponseDto.CategoryInfoModel>();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(NewCategoryName))
        {
            ModelState.AddModelError(nameof(NewCategoryName), "Название категории не может быть пустым");
            await OnGetAsync();
            return Page();
        }

        var dto = new AddCategoryDto { Name = NewCategoryName };

        var response = await _httpClient.PostAsJsonAsync("https://localhost:7197/api/v1/Category", dto);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при добавлении категории");
            await OnGetAsync();
            return Page();
        }

        await OnGetAsync();

        NewCategoryName = null;

        return Page();
    }
}

public class ApiResponseV1<T>
{
    public T Data { get; set; } = default!;
}
