using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EJobsMarket
{
    public static class WorkaroundVSAwaitWarning
    {
        // https://stackoverflow.com/a/29319061/17173876
        public static void DoNotAwait(this Task task) { }
    }
}
