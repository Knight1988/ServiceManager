using ServiceManagerBackEnd.Commons;

namespace ServiceManagerBackEnd.Exceptions;

public class UserNotFoundException : GeneralException
{
    public UserNotFoundException() : base(ErrorCodes.UserNotFound, "User not found")
    {
    }
}