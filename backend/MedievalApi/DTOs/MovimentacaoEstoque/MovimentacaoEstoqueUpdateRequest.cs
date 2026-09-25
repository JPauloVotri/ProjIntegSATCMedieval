using System.ComponentModel.DataAnnotations;
using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.MovimentacaoEstoque;

public record MovimentacaoEstoqueUpdateRequest(
    [Range(1, int.MaxValue, ErrorMessage = "Estoque é obrigatório.")]
    int EstoqueId,

    [Range(1, int.MaxValue, ErrorMessage = "Usuário é obrigatório.")]
    int UsuarioId,

    int? ReferenciaId,

    [MaxLength(50, ErrorMessage = "Referência tipo deve ter no máximo 50 caracteres.")]
    string? ReferenciaTipo,

    [EnumDataType(typeof(TipoMovimentacaoEstoque), ErrorMessage = "Tipo inválido.")]
    TipoMovimentacaoEstoque Tipo,

    [EnumDataType(typeof(OrigemMovimentacao), ErrorMessage = "Origem inválida.")]
    OrigemMovimentacao Origem,

    [Range(0.0001, double.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero.")]
    decimal Quantidade,

    [Range(0, double.MaxValue, ErrorMessage = "Quantidade anterior não pode ser negativa.")]
    decimal QuantidadeAnterior,

    [Range(0, double.MaxValue, ErrorMessage = "Quantidade posterior não pode ser negativa.")]
    decimal QuantidadePosterior,

    [Range(0, double.MaxValue, ErrorMessage = "Custo unitário não pode ser negativo.")]
    decimal? CustoUnitario,

    [Range(0, double.MaxValue, ErrorMessage = "Valor total não pode ser negativo.")]
    decimal? ValorTotal,

    [MaxLength(100, ErrorMessage = "Lote deve ter no máximo 100 caracteres.")]
    string? Lote,

    DateTime? DataValidade,

    [MaxLength(255, ErrorMessage = "Observação deve ter no máximo 255 caracteres.")]
    string? Observacao
);
