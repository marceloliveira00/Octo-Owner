using Back.Data;
using Back.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Back.Controllers
{
    [ApiController]
    [Route("clientes")]
    public class ClientsController : ControllerBase
    {
        public SqlContext _sqlContext { get; set; }

        public ClientsController(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        [HttpGet]
        [Route("obterClientes")]
        public IActionResult Get()
        {
            return Ok(_sqlContext.Clients.ToList());
        }

        [HttpPost]
        [Route("adicionarClientes")]
        public IActionResult Add(Client client)
        {
            _sqlContext.Clients.Add(client);
            _sqlContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("deletarClientes/{Id}")]
        public IActionResult Delete(int Id)
        {
            _sqlContext.Clients.Remove(new Client { Id = Id });
            _sqlContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("updateClientes")]
        public IActionResult Update(Client client)
        {
            _sqlContext.Clients.Update(client);
            _sqlContext.SaveChanges();
            return Ok();
        }
    }
}
