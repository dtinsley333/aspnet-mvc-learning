using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using NHibernate;

namespace HLogger
{
    public class HDbTransaction : IDbTransaction
    {
        private ITransaction _transaction;
        private IsolationLevel _isolationLevel;

        public HDbTransaction(ITransaction transaction)
        {
            _transaction = transaction;
        }

        public HDbTransaction(ITransaction transaction, IsolationLevel isolationLevel)
            : this(transaction)
        {
            _isolationLevel = isolationLevel;
        }

        #region IDbTransaction Members

        public void Commit()
        {
            _transaction.Commit();
        }

        public IDbConnection Connection
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public IsolationLevel IsolationLevel
        {
            get { return _isolationLevel; }
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _transaction.Dispose();
        }

        #endregion
    }
}
