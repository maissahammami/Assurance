using System;
using System.Collections.Generic;
using System.Text;
using domain.Models;


namespace domain.Commands
{
    public class AddGenericCommand<T> where T : class
    {
        public T Entity { get; }
        public AddGenericCommand(T entity) => Entity = entity;
    }
}








//namespace domain.Commands
//{
//    public class AddGenericCommand<T> where T : class
//    {
//        public T Entity { get; }

//        public AddGenericCommand(T entity)
//        {
//            Entity = entity;
//        }
//    }
//}
