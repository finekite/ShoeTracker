using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ShoeTracker.Core
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TParameter, TResult>(TParameter query) where TParameter : IQuery;
    }
}
