using Dapper;
using MySqlConnector;
using web_app_domain;

namespace web_app_repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly MySqlConnection mySqlConnection;

        public ProdutoRepository()
        {
            string connectionString = "Server=localhost;Database=sys;User=root;Password=123;";
            mySqlConnection = new MySqlConnection(connectionString);
        }

        public async Task<IEnumerable<Produto>> ListarProdutos()
        {
            await mySqlConnection.OpenAsync();
            string query = "select Id, Nome, Preco, QuantidadeEstoque, DataCriacao from produtos;";
            var Produtos = await mySqlConnection.QueryAsync<Produto>(query);
            mySqlConnection.Close();

            return Produtos;
        }

        public async Task SalvarProduto(Produto Produto)
        {
            await mySqlConnection.OpenAsync();
            string sql = @"insert into produtos(nome, preco, quantidadeEstoque, dataCriacao) 
                            values(@nome, @preco, @quantidadeEstoque, NOW());";
            await mySqlConnection.ExecuteAsync(sql, Produto);
            mySqlConnection.Close();
        }

        public async Task AtualizarProduto(Produto Produto)
        {

            await mySqlConnection.OpenAsync();

            string sql = @"update produtos 
                            set Nome = @nome, 
	                            Preco = @preco,
                                QuantidadeEstoque = @quantidadeEstoque,
                                DataCriacao = @dataCriacao
                            where Id = @id;";

            await mySqlConnection.ExecuteAsync(sql, Produto);
            await mySqlConnection.CloseAsync();
        }

        public async Task RemoverProduto(int id)
        {

            await mySqlConnection.OpenAsync();

            string sql = @"delete from Produtos where Id = @id;";

            await mySqlConnection.ExecuteAsync(sql, new { id });
            await mySqlConnection.CloseAsync();
        }
    }
}
