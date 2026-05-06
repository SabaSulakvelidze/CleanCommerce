using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCommerce.Application.Exceptions
{
    public class UnauthorizedException(string msg): Exception(msg)
    {
    }
}
