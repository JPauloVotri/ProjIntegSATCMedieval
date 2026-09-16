namespace MedievalApi.Exceptions;

public class BusinessException(string message) : DomainException(message) { }
