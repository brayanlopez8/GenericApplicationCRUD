using AutoMapper;
using ENT.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel;

namespace BLL.Automapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration ConfigurationAutomapper()
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Person, PersonVM>();
                cfg.CreateMap<PersonVM,Person>();

                cfg.CreateMap<Country, CountryVM>();
                cfg.CreateMap<CountryVM, Country>();

            });

            return config;
        }
    }
}
