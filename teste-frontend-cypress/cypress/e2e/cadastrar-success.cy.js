function sortearValor() {
  return Math.floor(Math.random() * 51);
}

function sortearEstoque() {
  return Math.floor(Math.random() * 21);
}

function sortearProduto() {
  const produtos = [
      'Arroz', 'Feijão', 'Macarrão', 'Açúcar', 'Sal', 'Café', 'Óleo de cozinha', 
      'Farinha de trigo', 'Leite', 'Manteiga', 'Queijo', 'Pão', 'Ovos', 'Carne bovina', 
      'Frango', 'Peixe', 'Batata', 'Cebola', 'Alho', 'Tomate', 'Cenoura', 'Alface', 
      'Brócolis', 'Couve', 'Banana', 'Maçã', 'Laranja', 'Uva', 'Manga', 'Pera', 
      'Melancia', 'Suco de laranja', 'Refrigerante', 'Biscoito', 'Chocolate', 'Sorvete', 
      'Detergente', 'Sabão em pó', 'Papel higiênico', 'Shampoo', 'Condicionador', 
      'Sabonete', 'Creme dental', 'Desodorante', 'Esponja de aço', 'Amaciante', 
      'Água sanitária', 'Saco de lixo', 'Fósforos', 'Velas', 'Guardanapo'
  ];

  // Sorteando um produto da lista
  const produtoSorteado = produtos[Math.floor(Math.random() * produtos.length)];
  return produtoSorteado;
}

describe('template spec', () => {
  it('passes', () => {
    cy.visit('http://127.0.0.1:5500/index.html')
    for (let index = 0; index < 5; index++) {
      const produto = `${sortearProduto()}`;
    const valor = `${sortearValor()}`;
    const estoque = `${sortearEstoque()}`;
    cy.get('#registerName').type(produto)
    cy.get('#registerPrice').type(valor)
    cy.get('#registerQtt').type(estoque)
    cy.get('#registerForm > .btn').click()
    
    cy.get('.btn').click()
    }
    cy.get('.position-absolute').click()
  })
})