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
    public class PersonBLL : IPersonBLL
    {
        private IMapper iMapper = null;
        private UnitOfWork unitOfWork;
        public PersonBLL()
        {
            var config = BLL.Automapper.AutoMapperConfig.ConfigurationAutomapper();
            iMapper = config.CreateMapper();
            this.unitOfWork = new UnitOfWork();
        }
        public PersonVM Create(PersonVM person)
        {
            var newPerson = iMapper.Map<PersonVM, Person>(person);
            return iMapper.Map<Person, PersonVM>(unitOfWork.PersonRepository.add(newPerson));
        }

        public Task<PersonVM> CreateAsync(PersonVM person)
        {
            throw new NotImplementedException();
        }

        public PersonVM GetById(int id)
        {
            return iMapper.Map<Person, PersonVM>(unitOfWork.PersonRepository.FindById(id));
        }

        public List<PersonVM> GetList()
        {

            return iMapper.Map<List<Person>, List<PersonVM>>(unitOfWork.PersonRepository.Getall().ToList());

        }

        public Task PutAsync(PersonVM person)
        {
            throw new NotImplementedException();
        }
    }
}
