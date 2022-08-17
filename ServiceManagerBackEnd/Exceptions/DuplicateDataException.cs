using ServiceManagerBackEnd.Commons;

namespace ServiceManagerBackEnd.Exceptions;

public class GeneralException : Exception
{
    public int ErrorCode { get; }
    
    public GeneralException(int errorCode, string message) : base(message)
    {
        this.ErrorCode = errorCode;
    }

    public GeneralException()
    {
        throw new NotImplementedException();
    }
}