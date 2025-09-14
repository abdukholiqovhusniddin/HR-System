namespace HR_System.Exceptions;
public class NotFoundException(string message) : Exception(message + "not found");