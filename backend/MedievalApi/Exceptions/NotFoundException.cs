namespace MedievalApi.Exceptions;

public class NotFoundException(string message) : DomainException(message)
{
    public NotFoundException(string recurso, Guid id)
        : this($"{recurso} com id '{id}' não encontrado.") { }
}
