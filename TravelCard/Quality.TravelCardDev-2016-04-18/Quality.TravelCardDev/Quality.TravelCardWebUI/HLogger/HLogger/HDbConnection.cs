using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Cfg;

namespace HLogger
{
    public class HDbConnection : System.Data.IDbConnection
    {

        private ISessionFactory _sessionFactory;
        private ISession Session { get; set; }

        public HDbConnection()
        {
            //Configure NHibernate
            Configuration config = new Configuration();
            config.Configure();
            config.AddAssembly(
                //System.Reflection.Assembly.GetExecutingAssembly()
                "HLoggerLibrary"
                );

            _sessionFactory = config.BuildSessionFactory();
        }

       

        #region IDbConnection Members

        public System.Data.IDbTransaction BeginTransaction(System.Data.IsolationLevel il)
        {
            //begin NHibernate transaction
            return new HDbTransaction(Session.BeginTransaction(), il);
        }

        public System.Data.IDbTransaction BeginTransaction()
        {
            //begin NHibernate transaction
            return new HDbTransaction(Session.BeginTransaction());
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException("ChangeDatabase");
        }


        public void Open()
        {
            //open NHibernate session
            Session = _sessionFactory.OpenSession();
        }

        public System.Data.ConnectionState State
        {
            get
            {
                //return connection state
                return Session.Connection.State;
            }
        }

        public void Close()
        {
            //Close session
            Session.Close();
        }

        public string ConnectionString { get; set; }


        public int ConnectionTimeout
        {
            get { throw new NotImplementedException("ConnectionTimeout"); }
        }

        public System.Data.IDbCommand CreateCommand()
        {
            //return our command
            return new HDbCommand(Session);
        }

        public string Database
        {
            get 
            { 
                throw new NotImplementedException("Database"); 
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            //dispose session
            if(Session.IsOpen) Session.Close();
            Session.Dispose();
        }

        #endregion
    }
}
