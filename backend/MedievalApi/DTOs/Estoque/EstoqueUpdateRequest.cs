using System.ComponentModel.DataAnnotations;

namespace MedievalApi.DTOs.Estoque;

public record EstoqueUpdateRequest(
    [Range(1, int.MaxValue, ErrorMessage = "Produto é obrigatório.")]
    int ProdutoId,

    [Range(0, double.MaxValue, ErrorMessage = "Quantidade disponível não pode ser negativa.")]
    decimal QuantidadeDisponivel,

    [Range(0, double.MaxValue, ErrorMessage = "Valor total não pode ser negativo.")]
    decimal ValorTotal,

    DateTime? UltimaEntrada,

    DateTime? UltimaSaida
);
