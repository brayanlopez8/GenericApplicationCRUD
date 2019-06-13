using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel;

namespace BLL.Interface
{
    public interface IPersonBLL
    {
        List<PersonVM> GetList();
        Task<List<PersonVM>> GetListAsync();
        PersonVM GetById(int id);
        Task<PersonVM> GetByIdAsync(int id);
        PersonVM Create(PersonVM person);
        Task<PersonVM> CreateAsync(PersonVM person);
        Task<PersonVM> PutAsync(PersonVM person);
        PersonVM Put(PersonVM person);
    }
}
