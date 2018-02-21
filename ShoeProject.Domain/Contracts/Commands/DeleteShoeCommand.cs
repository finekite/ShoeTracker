using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeTracker.Core;

namespace ShoeProject.Domain.Contracts.Commands
{
    public class DeleteShoeCommand : ICommand
    {
        public int Id { get; set; }

        public DeleteShoeCommand(int id)
        {
            Id = id;
        }
    }
}
