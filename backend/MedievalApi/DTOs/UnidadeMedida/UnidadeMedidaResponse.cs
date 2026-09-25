namespace MedievalApi.DTOs.UnidadeMedida;

public record UnidadeMedidaResponse(
    int Id,
    string Nome,
    string Sigla,
    string Tipo,
    bool Ativo,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);
