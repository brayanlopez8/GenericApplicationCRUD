using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel;

namespace BLL.Interface
{
    public interface ICityBLL
    {
        List<CityVM> GetList();
        CityVM GetById(int id);
        CityVM Create(CityVM Entity);
        CityVM Put(CityVM Entity);
        Task<List<CityVM>> GetListAsync();
        Task<CityVM> GetByIdAsync(int id);
        Task<CityVM> CreateAsync(CityVM Entity);
        Task<CityVM> PutAsync(CityVM Entity);
    }
}
