using System;
using System.Collections.Generic;
using System.Text;

namespace domain.Queries
{
    public class GetGenericQuery
    {
        public int Id { get; }
        public GetGenericQuery(int id) => Id = id;
    }
}
