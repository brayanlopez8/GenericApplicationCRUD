using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel;

namespace BLL.Interface
{
    public interface ICountryBLL
    {
        List<CountryVM> GetList();
        CountryVM GetById(int id);
        CountryVM Create(CountryVM Entity);
        CountryVM Put(CountryVM Entity);
        Task<List<CountryVM>> GetListAsync();
        Task<CountryVM> GetByIdAsync(int id);
        Task<CountryVM> CreateAsync(CountryVM Entity);
        Task<CountryVM> PutAsync(CountryVM Entity);
    }
}
