using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace ObjectOrientedSQL.Interfaces
{
    public interface IObjectSQL
    {
        T SelectFirstOrDefault<T>(Expression<Func<T, bool>> where) where T : class;
        List<T> SelectMany<T>(Expression<Func<T, bool>> where) where T : class;
    }
}