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
        private string NameCache = string.Empty;
        public CountryBLL()
        {
            var config = BLL.Automapper.AutoMapperConfig.ConfigurationAutomapper();
            iMapper = config.CreateMapper();
            this.unitOfWork = new UnitOfWork();
            this.NameCache = this.GetType().Name;
        }

        public CountryVM Create(CountryVM Entity)
        {
            var entity = iMapper.Map<CountryVM, Country>(Entity);
            var Result = iMapper.Map<Country, CountryVM>(unitOfWork.ContryRepository.add(entity));
            if (Result != null)
            {
                CacheManager<List<CountryVM>>.RemoveItemFromCache(NameCache);
            }
            return Result;
        }

        public Task<CountryVM> CreateAsync(CountryVM Entity)
        {
            throw new NotImplementedException();
        }

        public CountryVM GetById(int id)
        {
            return GetList().Where(c => c.Id == id).FirstOrDefault();
        }

        public List<CountryVM> GetList()
        {
            List<CountryVM> result = CacheManager<List<CountryVM>>.TryGetFromCache(NameCache);
            if (result == null)
            {
                result = iMapper.Map<List<Country>, List<CountryVM>>(unitOfWork.ContryRepository.Getall().ToList());
                CacheManager<List<CountryVM>>.TryAddToCacheDefaultTime(NameCache, result);
            }
            return result;
        }

        public Task PutAsync(CountryVM Entity)
        {
            throw new NotImplementedException();
        }
    }
}
