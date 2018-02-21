using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeTracker.Core;

namespace ShoeProject.Domain.Contracts.Commands
{
    public class AddReceiptPathCommand : ICommand
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public AddReceiptPathCommand(int id, string path)
        {
            Id = id;
            Path = path;
        }
    }
}
