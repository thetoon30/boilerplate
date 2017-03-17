using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalModule.Boilerplate.Core.Helper
{
    public class PagedResult<T>
    {
        private readonly ReadOnlyCollection<T> _result;
        private readonly int _total;

        public PagedResult(IList<T> result, int total)
        {
            CheckArgument.IsNotNull(result, "result");
            CheckArgument.IsNotNegative(total, "total");

            _result = new ReadOnlyCollection<T>(result);
            _total = total;
        }

        public IList<T> Result
        {
            get { return _result; }
        }

        public int Total
        {
            get { return _total; }
        }

        public bool IsEmpty
        {
            get { return _result.Count == 0; }
        }
    }
}
