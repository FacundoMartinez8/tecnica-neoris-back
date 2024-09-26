using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductMicroservice.Models;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OrderController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {

        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"http://localhost:5054/api/product/{order.ProductId}");


        if (!response.IsSuccessStatusCode)
        {
            return BadRequest("Error al verificar el stock del producto.");
        }

        var product = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());

        if (product != null && product.Stock < order.Quantity)
        {
            return BadRequest("Stock insuficiente.");
        }

        return Ok("hay stock.");
    }
}