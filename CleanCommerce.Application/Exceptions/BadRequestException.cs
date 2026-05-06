using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCommerce.Application.Exceptions
{
    public class BadRequestException(string msg): Exception(msg)
    {
    }
}
