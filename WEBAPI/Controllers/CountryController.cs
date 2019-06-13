using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel.ViewModel;

namespace WEBAPI.Controllers
{
    public class CountryController : ApiController
    {
        private ICountryBLL _CountryBLL;
        public CountryController(ICountryBLL CountryBLL)
        {
            this._CountryBLL = CountryBLL;
        }
        // GET: api/Country
        public IEnumerable<CountryVM> Get()
        {
            return _CountryBLL.GetList();
        }

        // GET: api/Country/5
        public CountryVM Get(int id)
        {
            return _CountryBLL.GetById(id);
        }

        // POST: api/Country
        public void Post(CountryVM value)
        {
            _CountryBLL.Create(value);
        }

        // PUT: api/Country/5
        public void Put(int id, [FromBody]string value)
        {
        }

        //DELETE: api/Country/5
        public void Delete(int id)
        {
        }
    }
}
