using ServiceManagerBackEnd.Commons;

namespace ServiceManagerBackEnd.Exceptions;

public class TokenInvalidException : GeneralException
{
    public TokenInvalidException() : base(ErrorCodes.TokenInvalid, "Token invalid")
    {
    }
    
    public TokenInvalidException(Exception innerException) : base(ErrorCodes.TokenInvalid, "Token invalid", innerException)
    {
    }
}