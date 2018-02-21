#region Assembly ShoeTracker.Core.dll, v1.0.0.0
// C:\Users\sgoldman\Documents\Visual Studio 2013\Projects\ShoeTracker
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeTracker.Core
{
    public interface IQueryHandler<in TParameter,out TResult> where TParameter : IQuery 
    {
        TResult Retrieve(TParameter query);
    }
}
