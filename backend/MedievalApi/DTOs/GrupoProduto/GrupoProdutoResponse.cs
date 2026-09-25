namespace MedievalApi.DTOs.GrupoProduto;

public record GrupoProdutoResponse(
    int Id,
    string Nome,
    string? Descricao,
    bool Ativo,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);
