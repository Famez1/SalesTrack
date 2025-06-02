namespace SalesTrack.Contracts.Dto;

public class AddProductDto
{
    public string Name { get; set; }

    public decimal Price { get; set; }  

    public string Unit {  get; set; }

    public string CategoryName { get; set; }
}
