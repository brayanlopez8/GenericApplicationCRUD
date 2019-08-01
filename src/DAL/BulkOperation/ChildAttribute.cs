using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BulkOperation
{
    public class ChildAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
