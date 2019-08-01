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
            var Result = iMapper.Map<Country, CountryVM>(unitOfWork.CountryRepository.add(entity));
            if (Result != null)
            {
                CacheManager<List<CountryVM>>.RemoveItemFromCache(NameCache);
            }
            return Result;
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
                result = iMapper.Map<List<Country>, List<CountryVM>>(unitOfWork.CountryRepository.Getall().ToList());
                CacheManager<List<CountryVM>>.TryAddToCacheDefaultTime(NameCache, result);
            }
            return result;
        }

        public CountryVM Put(CountryVM country)
        {
            var newCountryVM = iMapper.Map<CountryVM, Country>(country);
            return iMapper.Map<Country, CountryVM>(unitOfWork.CountryRepository.AddOrUpdate(newCountryVM));
        }

        public async Task<CountryVM> GetByIdAsync(int id)
        {
            return iMapper.Map<Country, CountryVM>(await unitOfWork.CountryRepository.FindFirstWhereAsync(c => c.Id == id));
        }

        public async Task<CountryVM> CreateAsync(CountryVM Country)
        {
            var newCountry = iMapper.Map<CountryVM, Country>(Country);
            return iMapper.Map<Country, CountryVM>(await unitOfWork.CountryRepository.addAsyc(newCountry));
        }

        public async Task<List<CountryVM>> GetListAsync()
        {
            var x = await unitOfWork.CountryRepository.GetallAsyc();
            return iMapper.Map<List<Country>, List<CountryVM>>(x.ToList());
        }

        public async Task<CountryVM> PutAsync(CountryVM Country)
        {
            var newCountryVM = iMapper.Map<CountryVM, Country>(Country);
            var newCountry = await unitOfWork.CountryRepository.AddOrUpdateAsync(newCountryVM);
            return iMapper.Map<Country, CountryVM>(newCountry);
        }

    }
}
