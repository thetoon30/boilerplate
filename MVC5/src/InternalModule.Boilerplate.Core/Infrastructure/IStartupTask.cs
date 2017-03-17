using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalModule.Boilerplate.Core.Infrastructure
{
    public interface IStartupTask
    {
        /// <summary>
        /// Execute task
        /// </summary>
        void Execute();

        /// <summary>
        /// Order
        /// </summary>
        int Order { get; }
    }
}
