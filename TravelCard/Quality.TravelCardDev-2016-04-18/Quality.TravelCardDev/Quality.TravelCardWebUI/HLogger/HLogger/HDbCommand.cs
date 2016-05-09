using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Data.SqlClient;
using NHibernate;

namespace HLogger
{
    public class HDbCommand: IDbCommand
    {
        private ISession _session;
        public HDbCommand(ISession session)
        {
            //init NHibirnate session variable
            _session = session;
        }

        #region IDbCommand Members

        public string CommandText { get; set; }
        public int CommandTimeout { get; set; }
        public CommandType CommandType { get; set; }
        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }


        public IDbDataParameter CreateParameter()
        {
            //create our parameter
            HDbDataParameter p = new HDbDataParameter();
            return p;
        }

        private HDbParameterCollection _parameters = new HDbParameterCollection();
        public IDataParameterCollection Parameters
        {
            get { return _parameters ; }
        }

        public void Prepare()
        {
        }

        public int ExecuteNonQuery()
        {
            //get iquery object
            IQuery query = _session.CreateSQLQuery(CommandText);

            //add all parameters
            foreach (HDbDataParameter p in Parameters)
                query = query.SetParameter(p.ParameterName, p.Value);

            //execute
            return query.List().Count;
        }

      
        public UpdateRowSource UpdatedRowSource
        {
            get
            {
                throw new NotImplementedException("get UpdatedRowSource");
            }
            set
            {
                throw new NotImplementedException("set UpdatedRowSource");
            }
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            throw new NotImplementedException("ExecuteReader");
        }

        public IDataReader ExecuteReader()
        {
            throw new NotImplementedException("ExecuteReader");
        }

        public object ExecuteScalar()
        {
            throw new NotImplementedException("ExecuteScalar");
        }

        public void Cancel()
        {
            throw new NotImplementedException("Cancel");
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            //throw new NotImplementedException("Dispose");
        }

        #endregion
    }
}
