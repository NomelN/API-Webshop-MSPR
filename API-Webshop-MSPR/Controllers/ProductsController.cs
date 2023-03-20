using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API_Webshop_MSPR.Controllers
{
    [Route("api/webshop/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet(Name = "GetProducts")]
        
        public async Task<ActionResult<Products>> GetProducts()
        {
            var data = new List<dynamic>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("https://615f5fb4f7254d0017068109.mockapi.io/api/v1/products");
                    response.EnsureSuccessStatusCode();
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<dynamic[]>(jsonString);


                    foreach (var product in products)
                    {
                        data.Add(new
                        {
                            Id = product.id,
                            CreatedAt = product.createdAt,
                            Name = product.name,
                            Details = product.details,
                            stock = product.stock.Value
                        });
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Impossible de récupérer la liste des produits ");
            }

            return Ok(JsonConvert.SerializeObject(data));
        }

        [HttpGet("{idProduct}", Name = "Get Produit By Id")]
        

        public async Task<ActionResult<Products>> GetProduitById(int idProduct)
        {
            try
            {
                using var httpClient = new HttpClient();
                {
                    var response = await httpClient.GetAsync($"https://615f5fb4f7254d0017068109.mockapi.io/api/v1/products/{idProduct}");
                    response.EnsureSuccessStatusCode();
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<dynamic>(jsonString);

                    var result = new
                    {
                        Id = product.id,
                        CreatedAt = product.createdAt,
                        Name = product.name.Value,
                        Details = product.details,
                        Stock = product.stock
                    };

                    return Ok(JsonConvert.SerializeObject(result));
                }
            }
            catch (Exception)
            {
                return BadRequest("Impossible de récupérer le produit avec l'id " + idProduct);
            }
        }

    }
}
