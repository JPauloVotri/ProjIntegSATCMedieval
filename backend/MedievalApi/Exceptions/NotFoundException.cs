namespace MedievalApi.Exceptions;

public class NotFoundException(string message) : DomainException(message)
{
    public NotFoundException(string recurso, int id)
        : this($"{recurso} com id '{id}' não encontrado.") { }
}
