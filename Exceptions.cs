﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    public class EmptyArgumentException : Exception
    {
        public EmptyArgumentException(string paramName) : base($"{paramName} не может быть пустым") { }
    }

    public class NumberInParamException : Exception
    {
        public NumberInParamException(string paramName) : base($"{paramName} не может содержать цифр") { }
    }

    public class InvalidDateException : Exception
    {
        public InvalidDateException() : base("Некорректная дата") { }
    }

    public class NegativePriceException : Exception
    {
        public NegativePriceException() : base("Цена экспоната не может быть отрицательной") { }
    }
}