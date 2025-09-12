using System;
using System.Collections.Generic;
using System.Text;

namespace domain.Commands
{
    public class PutGenericCommand<T> where T : class
    {
        public T Entity { get; }
        public PutGenericCommand(T entity) => Entity = entity;
    }
}

