using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using NHibernate;

namespace HLogger
{
    public class HDbDataParameter : IDbDataParameter
    {

        #region IDbDataParameter Members

        public byte Precision { get; set; }
        public byte Scale { get; set; }
        public int Size { get; set; }
        public DbType DbType { get; set; }
        public ParameterDirection Direction { get; set; }
        public string ParameterName { get; set; }
        public string SourceColumn { get; set; }
        public DataRowVersion SourceVersion { get; set; }
        public object Value { get; set; }
     
        public bool IsNullable
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
