using Dapper;
using System.Collections.Generic;

namespace Affixx.Core.Database
{
    public class DapperDynamicParameters
    {
        private DynamicParameters parms;

        public DapperDynamicParameters(Dictionary<string, object> args)
        {
            //foreach (var pair in args) dbArgs.Add(pair.Key, pair.Value);
            parms = new DynamicParameters(args);
        }

        public object Parameters
        {
            get
            {
                return parms;
            }
        }
    }
}
