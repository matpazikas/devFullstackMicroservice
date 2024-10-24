using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using web_app_domain;
using web_app_repository;

namespace web_app_performance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        private static ConnectionMultiplexer redis;
        private readonly IProdutoRepository _repository;

        public ProdutoController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduto()
        {
            string key = "getProduto";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyExpireAsync(key, TimeSpan.FromSeconds(10));
            string user = await db.StringGetAsync(key);

            if (!string.IsNullOrEmpty(user))
            {
                return Ok(user);
            }

            var Produtos = await _repository.ListarProdutos();

            string ProdutosJson = JsonConvert.SerializeObject(Produtos);
            await db.StringSetAsync(key, ProdutosJson);

            return Ok(Produtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto Produto)
        {
            await _repository.SalvarProduto(Produto);

            //apagar o cachê
            string key = "getProduto";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Produto Produto)
        {

            await _repository.AtualizarProduto(Produto);


            //apagar o cachê
            string key = "getProduto";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            await _repository.RemoverProduto(id);



            //apagar o cachê
            string key = "getProduto";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);

            return Ok();
        }

    }
}
