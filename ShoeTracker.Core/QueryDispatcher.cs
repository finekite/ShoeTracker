using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NUnit.Framework.Internal;

namespace ShoeTracker.Core
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IKernel kernel;

        public QueryDispatcher(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public TResult Dispatch<TParameter, TResult>(TParameter query) where TParameter : IQuery
        {
            var handler = kernel.Get<IQueryHandler<TParameter, TResult>>();
            return handler.Retrieve(query);
        }
    }
}
