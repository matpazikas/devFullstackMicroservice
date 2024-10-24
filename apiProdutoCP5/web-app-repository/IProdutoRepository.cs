﻿using web_app_domain;

namespace web_app_repository
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ListarProdutos();
        Task SalvarProduto(Produto Produto);
        Task AtualizarProduto(Produto Produto);
        Task RemoverProduto(int id);

    }
}