using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dispashopta
{
    class info
    {
        public string Name;
        public long Memory;
        public int Id;

        public info(string name, int id, long memory)
        {
            Name = name;
            Memory = memory;
            Id = id;
        }
    }
}
