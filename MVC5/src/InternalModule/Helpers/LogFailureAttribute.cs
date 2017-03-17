using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalModule.Boilerplate.Helpers
{
    public class LogFailureAttribute : JobFilterAttribute, IApplyStateFilter
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LogFailureAttribute));

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            var failedState = context.NewState as FailedState;
            if (failedState != null)
            {
                Logger.FatalFormat("Background job #{0} was failed with an exception. {1}", context.JobId,
                    failedState.Exception.Message);
            }
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
        }
    }
}