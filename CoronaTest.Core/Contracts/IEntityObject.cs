using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaTest.Core.Contracts
{
    public interface IEntityObject
    {
        public int Id { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
