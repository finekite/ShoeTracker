using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeTracker.Core
{
    public interface ICommandDispatcher
    {
        int Dispatch<TParameter>(TParameter command) where TParameter : ICommand;
    }
}
