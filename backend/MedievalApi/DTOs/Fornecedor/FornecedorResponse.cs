namespace MedievalApi.DTOs.Fornecedor;

public record FornecedorResponse(
    int Id,
    string RazaoSocial,
    string? NomeFantasia,
    string? Documento,
    string? InscricaoEstadual,
    string? Email,
    string? Telefone,
    string? NomeContato,
    string? Cep,
    string? Logradouro,
    string? Numero,
    string? Complemento,
    string? Bairro,
    string? Cidade,
    string? Estado,
    string? DiasEntrega,
    string? CondicaoPagamento,
    bool Ativo,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);