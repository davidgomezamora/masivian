using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Repository
{
    public class OrderBy<TEntity> where TEntity : class, new()
    {
        public Func<TEntity, object> Order { get; set; }
        public bool IsAscending { get; set; }

        public OrderBy() { }
        public OrderBy(Func<TEntity, object> orderBy, bool isAscending)
        {
            this.Order = orderBy;
            this.IsAscending = isAscending;
        }
    }
}
