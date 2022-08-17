namespace ServiceManagerBackEnd.Commons;

public static class ErrorCodes
{
    public const int None = 0;
    public const int InternalServerError = 500;
    public const int NotImplemented = 501;
    // system wise error codes start with 1000
    public const int DataExist = 1000;
    // user error codes start with 2000
    public const int UserAndPasswordNotMatch = 2000;
    public const int TokenInvalid = 2001;
}