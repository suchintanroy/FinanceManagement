﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagement.IFinaceRepository.Exceptions
{
    public class UserNotFoundException : IOException
    {

        public UserNotFoundException(string message) : base(message) { }

    }

}
