using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel
{
    public class CountryVM
    {
        public int Id { get; set; }
        public string Iso { get; set; }

        public string Name { get; set; }

        public string PrintableName { get; set; }

        public string Iso3 { get; set; }

        public string NumCode { get; set; }
    }
}
