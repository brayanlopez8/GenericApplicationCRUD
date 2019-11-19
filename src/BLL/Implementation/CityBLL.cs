using AutoMapper;
using BLL.Automapper;
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
    public class CityBLL : ICityBLL
    {
        private IMapper iMapper = null;
        private UnitOfWork unitOfWork;
        public CityBLL()
        {
            var config = AutoMapperConfig.ConfigurationAutomapper();
            iMapper = config.CreateMapper();
            this.unitOfWork = new UnitOfWork();
        }
        public CityVM Create(CityVM City)
        {
            var newCity = iMapper.Map<CityVM, City>(City);
            return iMapper.Map<City, CityVM>(unitOfWork.CityRepository.add(newCity));
        }

        public async Task<CityVM> CreateAsync(CityVM City)
        {
            var newCity = iMapper.Map<CityVM, City>(City);
            return iMapper.Map<City, CityVM>(await unitOfWork.CityRepository.addAsyc(newCity));
        }

        public CityVM GetById(int id)
        {
            return iMapper.Map<City, CityVM>(unitOfWork.CityRepository.FindById(id));
        }

        public async Task<CityVM> GetByIdAsync(int id)
        {
            return iMapper.Map<City, CityVM>(await unitOfWork.CityRepository.FindFirstWhereAsync(c => c.Id == id));
        }

        public List<CityVM> GetList()
        {

            return iMapper.Map<List<City>, List<CityVM>>(unitOfWork.CityRepository.Getall().ToList());

        }

        public async Task<List<CityVM>> GetListAsync()
        {
            var x = await unitOfWork.CityRepository.GetallAsyc();
            return iMapper.Map<List<City>, List<CityVM>>(x.ToList());
        }

        public CityVM Put(CityVM City)
        {
            var newCityVM = iMapper.Map<CityVM, City>(City);
            return iMapper.Map<City, CityVM>(unitOfWork.CityRepository.AddOrUpdate(newCityVM));
        }

        public async Task<CityVM> PutAsync(CityVM City)
        {
            var newCityVM = iMapper.Map<CityVM, City>(City);
            var newCity = await unitOfWork.CityRepository.AddOrUpdateAsync(newCityVM);
            return iMapper.Map<City, CityVM>(newCity);
        }
    }
}
