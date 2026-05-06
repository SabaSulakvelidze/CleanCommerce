using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCommerce.Application.Exceptions
{
    public class NotFoundException(string msg):Exception(msg)
    {
    }
}
