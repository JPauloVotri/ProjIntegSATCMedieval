namespace MedievalApi.DTOs.Produto;

public record ProdutoResponse(
    Guid Id,
    Guid CategoriaId,
    string CategoriaNome,
    Guid UnidadeMedidaId,
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
