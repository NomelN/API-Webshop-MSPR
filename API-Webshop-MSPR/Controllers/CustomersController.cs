using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace API_Webshop_MSPR.Controllers
{
    [Route("api/webshop/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet(Name = "GetCustomers")]
        
        public async Task<ActionResult<Customers>> GetCustomers()
        {
            var data = new List<dynamic>();
            try
            {
                using var httpClient = new HttpClient();
                {
                    var response = await httpClient.GetAsync("https://615f5fb4f7254d0017068109.mockapi.io/api/v1/customers/");
                    response.EnsureSuccessStatusCode();
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var customers = JsonConvert.DeserializeObject<dynamic[]>(jsonString);


                    foreach (var customer in customers)
                    {
                        data.Add(new
                        {
                            id = customer.id.Value,
                            CreatedAt = customer.createdAt,
                            Name = customer.name,
                            Username = customer.username,
                            FirstName = customer.firstName,
                            LastName = customer.lastName,
                            Address = customer.address,
                            Profile = customer.profile,
                            Company = customer.company,
                            Orders = customer.orders
                        });
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Impossible de récupérer la liste des clients");
            }

            return Ok(JsonConvert.SerializeObject(data));
        }

        [HttpGet("{idCustomers}", Name = "Get Customers By Id")]
        public async Task<ActionResult<Customers>> GetCustomerById(int idCustomers)
        {
            try
            {
                using var httpClient = new HttpClient();
                {
                    var response = await httpClient.GetAsync($"https://615f5fb4f7254d0017068109.mockapi.io/api/v1/customers/{idCustomers}");
                    response.EnsureSuccessStatusCode();
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var customer = JsonConvert.DeserializeObject<dynamic>(jsonString);

                    var result = new
                    {
                        Id = customer.id,
                        CreatedAt = customer.createdAt,
                        Name = customer.name,
                        Username = customer.username,
                        FirstName = customer.firstName,
                        LastName = customer.lastName,
                        Address = customer.address,
                        Profil = customer.profile,
                        Company = customer.company,
                        Orders = customer.orders
                    };

                    return Ok(JsonConvert.SerializeObject(result));
                }
            }
            catch (Exception)
            {
                return BadRequest("Impossible de récupérer le cleint avec l'id " + idCustomers);
            }

        }

        [HttpGet("{idCustomer}/orders", Name = "Get Orders By Id")]
        

        public async Task<ActionResult<Order>> GetOrdersById(int idCustomer)
        {
            var data = new List<dynamic>();

            try
            {
                using var httpClient = new HttpClient();
                {
                    var response = await httpClient.GetAsync($"https://615f5fb4f7254d0017068109.mockapi.io/api/v1/customers/{idCustomer}/orders");
                    response.EnsureSuccessStatusCode();
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var orders = JsonConvert.DeserializeObject<dynamic>(jsonString);

                    foreach (var order in orders)
                    {
                        data.Add(new
                        {
                            CreatedAt = order.createdAt.Value,
                            Id = order.id.Value,
                            CustomerId = order.customerId.Value,
                        });
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Impossible de récupérer les commandes d'un client avec l'id " + idCustomer);
            }

            return Ok(JsonConvert.SerializeObject(data));
        }
        [HttpGet("{idCustomer}/orders/{idOrder}/products", Name = "Get Products With Orders")]
        
        public async Task<ActionResult<Order>> GetOrderProducts(int idCustomer, int idOrder)
        {
            var data = new List<dynamic>();
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"https://615f5fb4f7254d0017068109.mockapi.io/api/v1/customers/{idCustomer}/orders/{idOrder}/products");
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<dynamic[]>(jsonString);

                foreach (var product in order)
                {
                    data.Add(new
                    {
                        CreatedAt = product.createdAt,
                        Name = product.name.Value,
                        Details = product.details,
                        Stock = product.stock,
                        Id = product.id,
                        OrderId = product.orderId
                    });
                }
            }
            catch (Exception)
            {
                return BadRequest("Impossible de récupérer la liste des produits d'une commande ");
            }

            return Ok(JsonConvert.SerializeObject(data));
        }
    }
}
