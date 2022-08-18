namespace ServiceManagerBackEnd.Exceptions;

public class GeneralException : Exception
{
    public int ErrorCode { get; }
    
    public GeneralException(int errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }

    public GeneralException(int errorCode, string message, Exception innerException) : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}