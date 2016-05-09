using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HLogger
{
    public class HDbParameterCollection : IDataParameterCollection 
    {

        IList<HDbDataParameter> parameters = new List<HDbDataParameter>();

        #region IDataParameterCollection Members

        public bool Contains(string parameterName)
        {
            return parameterName.Contains(parameterName);
        }

        public int IndexOf(string parameterName)
        {
            throw new NotImplementedException("parameterName");
        }

        public void RemoveAt(string parameterName)
        {
            throw new NotImplementedException("parameterName");
        }

        public object this[string parameterName]
        {
            get
            {
                return (from p in parameters
                       where p.ParameterName == parameterName
                       select p).SingleOrDefault();
                       
            }
            set
            {
                throw new NotImplementedException("set Indexator");
            }
        }

        #endregion

        #region IList Members

        public int Add(object value)
        {
            parameters.Add(value as HDbDataParameter);
            return parameters.Count-1;
        }

        public void Clear()
        {
            throw new NotImplementedException("Clear");
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException("Contains");
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException("IndexOf");
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException("Insert");
        }

        public bool IsFixedSize
        {
            get { throw new NotImplementedException("IsFixedSize"); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException("IsReadOnly"); }
        }

        public void Remove(object value)
        {
            throw new NotImplementedException("Remove");
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException("RemoveAt");
        }

        public object this[int index]
        {
            get
            {
                throw new NotImplementedException("get this index");
            }
            set
            {
                throw new NotImplementedException("set this index");
            }
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException("Copy to");
        }

        public int Count
        {
            get 
            {
                return parameters.Count;
            }
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException("IsSynchronized"); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException("SyncRoot"); }
        }

        #endregion

        #region IEnumerable Members

        public System.Collections.IEnumerator GetEnumerator()
        {
            return parameters.GetEnumerator();
        }

        #endregion
    }
}
