using System;
using System.Collections.Generic;
using System.Text;

namespace domain.Commands
{
    public class DeleteGenericCommand
    {
        public int Id { get; }
        public DeleteGenericCommand(int id) => Id = id;
    }
}
