using System;

namespace Accountant.BLL.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException()
            : base("Username or password is incorrect.")
        {
        }
    }
}
