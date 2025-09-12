using System;
using System.Collections.Generic;
using System.Text;

using MediatR;

namespace domain.Queries
{
    public class GetByIdGenericQuery<T> : IRequest<T>
    {
        public int Id { get; set; }
        public GetByIdGenericQuery(int id) => Id = id;
    }
}

