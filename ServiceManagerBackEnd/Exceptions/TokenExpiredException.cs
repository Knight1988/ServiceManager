using ServiceManagerBackEnd.Commons;

namespace ServiceManagerBackEnd.Exceptions;

public class TokenExpiredException : GeneralException
{
    public TokenExpiredException() : base(ErrorCodes.TokenExpired, "Token expired")
    {
    }
}