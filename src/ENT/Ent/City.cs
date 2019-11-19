using ENT.ParentEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT.Ent
{
    public class City : GenericEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
