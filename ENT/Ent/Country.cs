using ENT.ParentEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT.Ent
{
    public class Country : GenericEntity
    {
        [Column("Iso")]
        public string Iso { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("PrintableName")]
        public string PrintableName { get; set; }

        [Column("Iso3")]
        public string Iso3 { get; set; }

        [Column("NumCode")]
        public string NumCode { get; set; }
    }
}
