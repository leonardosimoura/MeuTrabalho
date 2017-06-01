using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Domain.Shared.Models
{
    public class ModelBase<TEntity> : IEqualityComparer<TEntity>
    {
        public override int GetHashCode()
        {
            return 0;
        }
        public bool Equals(TEntity x, TEntity y)
        {
            return EqualityComparer<TEntity>.Default.Equals(x, y);
        }

        public int GetHashCode(TEntity obj)
        {
            return 0;
        }
    }
}
