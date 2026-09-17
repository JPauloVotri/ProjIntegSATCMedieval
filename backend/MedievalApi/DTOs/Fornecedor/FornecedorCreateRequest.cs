using System.ComponentModel.DataAnnotations;

namespace MedievalApi.DTOs.Fornecedor;

public record FornecedorCreateRequest(
    [Required(ErrorMessage = "Razão social é obrigatória.")]
    [MaxLength(200, ErrorMessage = "Razão social deve ter no máximo 200 caracteres.")]
    string RazaoSocial,

    [MaxLength(200, ErrorMessage = "Nome fantasia deve ter no máximo 200 caracteres.")]
    string? NomeFantasia,

    [MaxLength(20, ErrorMessage = "Documento deve ter no máximo 20 caracteres.")]
    string? Documento,

    [MaxLength(20, ErrorMessage = "Inscrição estadual deve ter no máximo 20 caracteres.")]
    string? InscricaoEstadual,

    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    [MaxLength(150, ErrorMessage = "E-mail deve ter no máximo 150 caracteres.")]
    string? Email,

    [MaxLength(20, ErrorMessage = "Telefone deve ter no máximo 20 caracteres.")]
    string? Telefone,

    [MaxLength(150, ErrorMessage = "Nome do contato deve ter no máximo 150 caracteres.")]
    string? NomeContato,

    [MaxLength(10, ErrorMessage = "CEP deve ter no máximo 10 caracteres.")]
    string? Cep,

    [MaxLength(200, ErrorMessage = "Logradouro deve ter no máximo 200 caracteres.")]
    string? Logradouro,

    [MaxLength(20, ErrorMessage = "Número deve ter no máximo 20 caracteres.")]
    string? Numero,

    [MaxLength(100, ErrorMessage = "Complemento deve ter no máximo 100 caracteres.")]
    string? Complemento,

    [MaxLength(100, ErrorMessage = "Bairro deve ter no máximo 100 caracteres.")]
    string? Bairro,

    [MaxLength(100, ErrorMessage = "Cidade deve ter no máximo 100 caracteres.")]
    string? Cidade,

    [MaxLength(2, ErrorMessage = "Estado deve ter 2 caracteres.")]
    string? Estado,

    [MaxLength(100, ErrorMessage = "Dias de entrega deve ter no máximo 100 caracteres.")]
    string? DiasEntrega,

    [MaxLength(100, ErrorMessage = "Condição de pagamento deve ter no máximo 100 caracteres.")]
    string? CondicaoPagamento
);
