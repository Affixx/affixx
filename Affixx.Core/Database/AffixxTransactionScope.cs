using System;
using System.Transactions;

namespace Affixx.Core.Database
{
    public class AffixxTransactionScope : IDisposable
    {
        private readonly TransactionScope _scope;

        public AffixxTransactionScope(TimeSpan? timeout = null)
        {
            var options = new TransactionOptions {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = timeout ?? TimeSpan.FromSeconds(30)
            };
            _scope = new TransactionScope(TransactionScopeOption.Required, options);
        }

        public void Complete()
        {
            _scope.Complete();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
