using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace ShoeTracker.Core
{
    /// <summary>
    /// command dispatcher class
    /// </summary>
    public class CommandDispatcher : ICommandDispatcher
    {
        /// <summary>
        /// the kernel
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kernel"></param>
        public CommandDispatcher(IKernel kernel)
        {
            this.kernel = kernel;
        }

        /// <summary>
        /// Find the command handler and executes it
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <param name="command"></param>
        public int Dispatch<TParameter>(TParameter command) where TParameter : ICommand
        {
            var handler = kernel.Get<ICommandHandler<TParameter>>();
            return handler.HandleCommand(command);
        }
    }
}
