using System.Data;
using NHibernate.SqlTypes;

namespace Bifrost.NHibernate.UserTypes
{
    public abstract class ValueTypeOfInteger<T> : SimpleImmutableUserTypeBase<T>
    {
        public override SqlType[] SqlTypes
        {
            get { return new[] { new SqlType(DbType.Int32) }; }
        }
    }
}