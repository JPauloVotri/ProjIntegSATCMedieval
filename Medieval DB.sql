Project Medieval {
  database_type: "PostgreSQL"
  Note: "Sistema de gerenciamento de orçamentos, compras e estoque"
}


// ======================================================
// ENUMS
// ======================================================

Enum UsuarioStatus {
  ativo
  inativo
}

Enum GrupoUsuario {
  administrador
  consulta
}

Enum StatusCotacaoItem {
  pendente
  selecionado
  cancelado
}

Enum StatusPedidoCompra {
  rascunho
  enviado
  parcialmenteRecebido
  recebido
  cancelado
}

Enum TipoMovimentacaoEstoque {
  entrada
  saida
  ajuste
  perda
  devolucao
}

Enum OrigemMovimentacao {
  pedidoCompra
  saidaEstoque
  ajusteManual
  inventario
  devolucao
  perda
}

Enum TipoSaidaEstoque {
  consumo
  perda
  vencimento
  devolucao
  ajuste
  outro
}

Enum StatusSaidaEstoque {
  rascunho
  confirmada
  cancelada
}

Enum TipoAuditoria {
  INSERT
  UPDATE
  DELETE
  LOGIN
  LOGOUT
  OUTRO
}

Enum StatusCotacao {
  rascunho
  enviada
  emResposta
  encerrada
  cancelada
}


// ======================================================
// USUÁRIO
// ======================================================

Table Usuario {
  UsuarioId bigint [pk, increment]

  Nome varchar(150) [not null]
  Email varchar(150) [not null, unique]
  SenhaHash varchar(255) [not null]

  GrupoUsuario GrupoUsuario [not null, default: 'administrador']
  Status UsuarioStatus [not null, default: 'ativo']

  UltimoLogin timestamp

  CriadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]
  AtualizadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]

  indexes {
    Email [unique]
    GrupoUsuario
    Status
  }
}


// ======================================================
// CATEGORIA
// ======================================================

Table Categoria {
  CategoriaId bigint [pk, increment]

  Nome varchar(100) [not null, unique]
  Descricao varchar(255)

  Ativo boolean [not null, default: true]

  CriadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]
  AtualizadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]

  indexes {
    Nome [unique]
    Ativo
  }
}


// ======================================================
// PRODUTO
// ======================================================

Table Produto {
  ProdutoId bigint [pk, increment]

  CategoriaId bigint [not null]
  UnidadeMedidaId bigint [not null]

  Codigo varchar(50) [unique]
  Nome varchar(150) [not null]
  Descricao varchar(255)

  EstoqueMinimo decimal(14,3) [not null, default: 0]
  EstoqueMaximo decimal(14,3)

  CustoMedioEstoque decimal(14,4) [not null, default: 0]
  UltimoCusto decimal(14,4) [not null, default: 0]

  ValidadeDias int [default: null]

  Ativo boolean [not null, default: true]

  CriadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]
  AtualizadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]

  indexes {
    Codigo [unique]
    Nome
    CategoriaId
    UnidadeMedidaId
    Ativo
  }
}


// ======================================================
// UNIDADE DE MEDIDA
// ======================================================

Table UnidadeMedida {
  UnidadeMedidaId bigint [pk, increment]

  Nome varchar(100) [not null]
  Sigla varchar(20) [not null, unique]
  Tipo varchar(50) [not null]
  Ativo boolean [not null, default: true]
  CriadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]
  AtualizadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]

  indexes {
    Sigla [unique]
    Tipo
    Ativo
  }
}


// ======================================================
// FORNECEDOR
// ======================================================

Table Fornecedor {
  FornecedorId bigint [pk, increment]

  RazaoSocial varchar(200) [not null]
  NomeFantasia varchar(200)

  Documento varchar(30)
  InscricaoEstadual varchar(30)

  Email varchar(150)
  Telefone varchar(30)
  NomeContato varchar(150)

  Cep varchar(10)
  Logradouro varchar(200)
  Numero varchar(20)
  Complemento varchar(100)
  Bairro varchar(100)
  Cidade varchar(100)
  Estado varchar(2)

  DiasEntrega varchar(50)
  CondicaoPagamento varchar(150)

  Ativo boolean [not null, default: true]

  CriadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]
  AtualizadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]

  indexes {
    Documento
    RazaoSocial
    NomeFantasia
    Ativo
  }
}


// ======================================================
// COTAÇÃO
// ======================================================

Table Cotacao {
  CotacaoId bigint [pk, increment]

  UsuarioId bigint [not null]

  DataSolicitacao date [not null]
  DataLimiteResposta date

  Status StatusCotacao [not null, default: 'rascunho']

  Observacao text

  CriadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]
  AtualizadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]

  indexes {
    UsuarioId
    Status
    DataSolicitacao
    DataLimiteResposta
  }
}


// ======================================================
// ITEM DA COTAÇÃO
// ======================================================

Table CotacaoItem {
  CotacaoItemId bigint [pk, increment]

  CotacaoId bigint [not null]
  ProdutoId bigint [not null]
  UnidadeMedidaId bigint [not null]

  Quantidade decimal(14,3) [not null]

  CotacaoItemFornecedorEscolhidoId bigint

  Status StatusCotacaoItem [not null, default: 'pendente']

  Observacao varchar(255)

  CriadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]
  AtualizadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]

  indexes {
    CotacaoId
    ProdutoId
    UnidadeMedidaId
    CotacaoItemFornecedorEscolhidoId
    Status
  }
}


// ======================================================
// FORNECEDOR DO ITEM DA COTAÇÃO
// ======================================================

Table CotacaoItemFornecedor {
  CotacaoItemFornecedorId bigint [pk, increment]

  CotacaoItemId bigint [not null]
  FornecedorId bigint [not null]

  ValorUnitario decimal(16,4) [not null]
  PrazoEntregaDias int
  CondicaoPagamento varchar(150)
  Observacao varchar(255)

  CriadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]
  AtualizadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]

  indexes {
    (CotacaoItemId, FornecedorId) [unique]
  }
}


// ======================================================
// PEDIDO DE COMPRA
// ======================================================

Table PedidoCompra {
  PedidoCompraId bigint [pk, increment]

  FornecedorId bigint [not null]
  UsuarioId bigint [not null]
  CotacaoId bigint

  DataPedido date [not null]
  DataPrevistaEntrega date
  DataRecebimento timestamp

  Status StatusPedidoCompra [not null, default: 'rascunho']

  /*
  Subtotal decimal(16,4) [not null, default: 0]
  Desconto decimal(16,4) [not null, default: 0]
  Frete decimal(16,4) [not null, default: 0]
  OutrasDespesas decimal(16,4) [not null, default: 0]
  */

  Total decimal(16,4) [not null, default: 0]

  Observacao text

  CriadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]
  AtualizadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]

  indexes {
    FornecedorId
    UsuarioId
    CotacaoId
    Status
    DataPedido
  }
}


// ======================================================
// ITEM DO PEDIDO DE COMPRA
// ======================================================

Table PedidoCompraItem {
  PedidoCompraItemId bigint [pk, increment]

  PedidoCompraId bigint [not null]
  ProdutoId bigint [not null]
  UnidadeMedidaId bigint [not null]
  CotacaoItemId bigint

  QuantidadeSolicitada decimal(14,3) [not null]
  QuantidadeRecebida decimal(14,3) [not null, default: 0]

  ValorUnitario decimal(16,4) [not null]

  Total decimal(16,4) [not null, default: 0]

  Observacao varchar(255)

  CriadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]
  AtualizadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]

  indexes {
    PedidoCompraId
    ProdutoId
    UnidadeMedidaId
  }
}


// ======================================================
// ESTOQUE
// ======================================================

Table Estoque {
  EstoqueId bigint [pk, increment]

  ProdutoId bigint [not null, unique]

  QuantidadeDisponivel decimal(14,3) [not null, default: 0]
  ValorTotal decimal(16,4) [not null, default: 0]

  UltimaEntrada timestamp
  UltimaSaida timestamp

  AtualizadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]

  indexes {
    ProdutoId [unique]
    QuantidadeDisponivel
  }
}


// ======================================================
// SAÍDA DE ESTOQUE
// ======================================================

Table SaidaEstoque {
  SaidaEstoqueId bigint [pk, increment]

  UsuarioId bigint [not null]

  DataSaida timestamp [not null, default: `CURRENT_TIMESTAMP`]

  Tipo TipoSaidaEstoque [not null]
  Status StatusSaidaEstoque [not null, default: 'rascunho']

  Motivo varchar(255)
  Observacao text

  CriadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]
  AtualizadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]

  indexes {
    UsuarioId
    DataSaida
    Tipo
    Status
  }
}


// ======================================================
// ITEM DA SAÍDA DE ESTOQUE
// ======================================================

Table SaidaEstoqueItem {
  SaidaEstoqueItemId bigint [pk, increment]

  SaidaEstoqueId bigint [not null]
  ProdutoId bigint [not null]
  UnidadeMedidaId bigint [not null]

  Quantidade decimal(14,3) [not null]

  CustoUnitario decimal(16,4) [not null, default: 0]
  CustoTotal decimal(16,4) [not null, default: 0]

  Lote varchar(100)
  DataValidade date

  Observacao varchar(255)

  CriadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]

  indexes {
    SaidaEstoqueId
    ProdutoId
    UnidadeMedidaId
    Lote
    DataValidade
  }
}


// ======================================================
// MOVIMENTAÇÃO DE ESTOQUE
// ======================================================

Table MovimentacaoEstoque {
  MovimentacaoEstoqueId bigint [pk, increment]

  EstoqueId bigint [not null]
  UsuarioId bigint [not null]

  Tipo TipoMovimentacaoEstoque [not null]
  Origem OrigemMovimentacao [not null]

  Quantidade decimal(14,3) [not null]

  QuantidadeAnterior decimal(14,3) [not null]
  QuantidadePosterior decimal(14,3) [not null]

  CustoUnitario decimal(16,4)
  ValorTotal decimal(16,4)

  Lote varchar(100)
  DataValidade date

  ReferenciaId bigint
  ReferenciaTipo varchar(50)

  Observacao varchar(255)

  CriadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]

  indexes {
    EstoqueId
    UsuarioId
    Tipo
    Origem
    CriadoEm
    (ReferenciaTipo, ReferenciaId)
  }
}


// ======================================================
// AUDITORIA
// ======================================================
/*
É o fluxo responsável por todos os registros de alterações:
Manuais (Vinculadas a um usuário)
Automáticas (Algum processamento realizado pelo sistema)
Serve também como um registro de alterações em LOG
OBS: Confirmar se será somente via banco ou via interface
*/

Table Auditoria {
  AuditoriaId bigint [pk, increment]

  UsuarioId bigint

  Tabela varchar(100) [not null]
  RegistroId bigint

  Tipo TipoAuditoria [not null]

  DadosAnteriores json
  DadosNovos json

  Ip varchar(45)
  UserAgent varchar(500)

  CriadoEm timestamp [not null, default: `CURRENT_TIMESTAMP`]

  indexes {
    UsuarioId
    Tabela
    RegistroId
    Tipo
    CriadoEm
  }
}


// ======================================================
// RELACIONAMENTOS
// ======================================================

// ======================================================
// CATEGORIA / PRODUTO / UNIDADE
// ======================================================

// Categoria -> Produto
Ref: Categoria.CategoriaId < Produto.CategoriaId

// Unidade de medida -> Produto
Ref: UnidadeMedida.UnidadeMedidaId < Produto.UnidadeMedidaId


// ======================================================
// ESTOQUE
// ======================================================

// Produto -> Estoque
// 1 produto possui 1 registro de estoque
Ref: Produto.ProdutoId - Estoque.ProdutoId


// ======================================================
// COTAÇÃO
// ======================================================

// Usuário -> Cotação
Ref: Usuario.UsuarioId < Cotacao.UsuarioId

// Cotação -> Itens
Ref: Cotacao.CotacaoId < CotacaoItem.CotacaoId

// Produto -> Itens da cotação
Ref: Produto.ProdutoId < CotacaoItem.ProdutoId

// Unidade -> Itens da cotação
Ref: UnidadeMedida.UnidadeMedidaId < CotacaoItem.UnidadeMedidaId

// Fornecedor -> Fornecedor escolhido do item
Ref: CotacaoItemFornecedor.CotacaoItemFornecedorId < CotacaoItem.CotacaoItemFornecedorEscolhidoId


// ======================================================
// COTAÇÃO / FORNECEDORES
// ======================================================

// Item da cotação -> Ofertas dos fornecedores
Ref: CotacaoItem.CotacaoItemId < CotacaoItemFornecedor.CotacaoItemId

// Fornecedor -> Ofertas dos fornecedores
Ref: Fornecedor.FornecedorId < CotacaoItemFornecedor.FornecedorId


// ======================================================
// PEDIDO DE COMPRA
// ======================================================

// Cotação -> Pedidos de compra
// Uma cotação pode gerar vários pedidos,
// normalmente um por fornecedor escolhido.
Ref: Cotacao.CotacaoId < PedidoCompra.CotacaoId

// Fornecedor -> Pedido de compra
Ref: Fornecedor.FornecedorId < PedidoCompra.FornecedorId

// Usuário -> Pedido de compra
Ref: Usuario.UsuarioId < PedidoCompra.UsuarioId

// Pedido -> Itens
Ref: PedidoCompra.PedidoCompraId < PedidoCompraItem.PedidoCompraId

// Cotação -> Item do pedido
Ref: CotacaoItem.CotacaoItemId < PedidoCompraItem.CotacaoItemId

// Produto -> Item do pedido
Ref: Produto.ProdutoId < PedidoCompraItem.ProdutoId

// Unidade -> Item do pedido
Ref: UnidadeMedida.UnidadeMedidaId < PedidoCompraItem.UnidadeMedidaId


// ======================================================
// SAÍDA DE ESTOQUE
// ======================================================

// Usuário -> Saída de estoque
Ref: Usuario.UsuarioId < SaidaEstoque.UsuarioId

// Saída -> Itens
Ref: SaidaEstoque.SaidaEstoqueId < SaidaEstoqueItem.SaidaEstoqueId

// Produto -> Itens da saída
Ref: Produto.ProdutoId < SaidaEstoqueItem.ProdutoId

// Unidade -> Itens da saída
Ref: UnidadeMedida.UnidadeMedidaId < SaidaEstoqueItem.UnidadeMedidaId


// ======================================================
// MOVIMENTAÇÃO DE ESTOQUE
// ======================================================

// Estoque -> Movimentações
Ref: Estoque.EstoqueId < MovimentacaoEstoque.EstoqueId

// Usuário -> Movimentações
Ref: Usuario.UsuarioId < MovimentacaoEstoque.UsuarioId


// ======================================================
// AUDITORIA
// ======================================================

// Usuário -> Auditoria
Ref: Usuario.UsuarioId < Auditoria.UsuarioId
