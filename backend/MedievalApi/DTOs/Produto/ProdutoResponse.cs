namespace MedievalApi.DTOs.Produto;

public record ProdutoResponse(
    int Id,
    int GrupoProdutoId,
    string GrupoProdutoNome,
    int UnidadeMedidaId,
    string UnidadeMedidaSigla,
    string? Codigo,
    string Nome,
    string? Descricao,
    decimal EstoqueMinimo,
    decimal? EstoqueMaximo,
    decimal CustoMedioEstoque,
    decimal UltimoCusto,
    int? ValidadeDias,
    bool Ativo,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);
