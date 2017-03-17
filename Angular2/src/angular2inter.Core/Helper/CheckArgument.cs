using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace angular2inter.Core.Helper
{
    public class CheckArgument
    {
        public static void IsNotNegative(int argument, string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        public static void IsNotNegative(decimal argument, string argumentName)
        {
            if (argument < 0m)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        public static void IsNotNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void IsNotEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException(string.Format("\"{0}\" cannot be blank.", argumentName), argumentName);
            }
        }

        public static void IsNotEmpty<T>(ICollection<T> argument, string argumentName)
        {
            IsNotNull(argument, argumentName);

            if (argument.Count == 0)
            {
                throw new ArgumentException("Collection cannot be empty.", argumentName);
            }
        }
    }
}
