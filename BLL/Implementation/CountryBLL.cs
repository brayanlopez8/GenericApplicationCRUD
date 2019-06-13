using AutoMapper;
using BLL.Interface;
using BLL.Utitliy;
using DAL.UnitOfWork;
using ENT.Ent;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel;

namespace BLL.Implementation
{
    public class CountryBLL : ICountryBLL
    {
        private IMapper iMapper = null;
        private UnitOfWork unitOfWork;
        public CountryBLL()
        {
            var config = BLL.Automapper.AutoMapperConfig.ConfigurationAutomapper();
            iMapper = config.CreateMapper();
            this.unitOfWork = new UnitOfWork();
        }

        public CountryVM Create(CountryVM Entity)
        {
            var entity = iMapper.Map<CountryVM, Country>(Entity);
            return iMapper.Map<Country, CountryVM>(unitOfWork.ContryRepository.add(entity));
        }

        public Task<PersonVM> CreateAsync(CountryVM Entity)
        {
            throw new NotImplementedException();
        }

        public CountryVM GetById(int id)
        {
            return GetList().Where(c => c.Id == id).FirstOrDefault();
        }

        public List<CountryVM> GetList()
        {
            string NameChache = $"{new StackFrame(1).GetMethod().ReflectedType.FullName}{new StackFrame(1).GetMethod().Name}";
            List<CountryVM> result = CacheManager<List<CountryVM>>.TryGetFromCache(NameChache);
            if (result == null)
            {
                result = iMapper.Map<List<Country>, List<CountryVM>>(unitOfWork.ContryRepository.Getall().ToList());
                CacheManager<List<CountryVM>>.TryAddToCacheDefaultTime(NameChache, result);
            }
            return result;
        }

        public Task PutAsync(CountryVM Entity)
        {
            throw new NotImplementedException();
        }
    }
}
