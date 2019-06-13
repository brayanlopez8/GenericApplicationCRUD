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
        PersonVM GetById(int id);
        PersonVM Create(PersonVM person);
        Task<PersonVM> CreateAsync(PersonVM person);
        Task PutAsync(PersonVM person);
    }
}
