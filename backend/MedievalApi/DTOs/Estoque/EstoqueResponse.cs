namespace MedievalApi.DTOs.Estoque;

public record EstoqueResponse(
    int Id,
    int ProdutoId,
    string ProdutoNome,
    decimal QuantidadeDisponivel,
    decimal ValorTotal,
    DateTime? UltimaEntrada,
    DateTime? UltimaSaida,
    DateTime AtualizadoEm
);
