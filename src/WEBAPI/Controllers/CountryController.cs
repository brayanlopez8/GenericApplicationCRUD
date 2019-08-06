using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ViewModel.ViewModel;

namespace WEBAPI.Controllers
{
    /// <summary>
    /// CountryController class
    /// </summary>
    public class CountryController : ApiController
    {
        private ICountryBLL _CountryBLL;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CountryBLL"></param>
        public CountryController(ICountryBLL CountryBLL)
        {
            this._CountryBLL = CountryBLL;
        }

        /// <summary>
        /// GET: api/GetAllCountry
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/countries")]
        public IEnumerable<CountryVM> GetAll()
        {
            return _CountryBLL.GetList();
        }

        /// <summary>
        /// GET: api/GetCountry/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/countries/{id}")]
        public CountryVM Get(int id)
        {
            return _CountryBLL.GetById(id);
        }

        /// <summary>
        /// POST: api/PostCountry
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Route("api/countries")]
        public void Post(CountryVM value)
        {
            _CountryBLL.Create(value);
        }

        /// <summary>
        /// PUT: api/PutCountry/5
        /// </summary>        
        /// <param name="value"></param>
        [HttpPut]
        [Route("api/countries")]
        public IHttpActionResult Put(CountryVM value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }
                var x = _CountryBLL.Put(value);

                return Ok<CountryVM>(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        ///// <summary>
        ///// DELETE: api/Country/5
        ///// </summary>
        ///// <param name="id"></param>
        //[HttpDelete]
        //public void Delete(int id)
        //{
        //}

        #region Metoths Async

        /// <summary>
        /// GET: api/GetAllAsyncCountries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/countriesAsync")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            try
            {
                var x = await _CountryBLL.GetListAsync();
                return Ok<List<CountryVM>>(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// GET: api/GetAsyncCountry/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/countriesAsync/{id}")]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            try
            {
                var x = await _CountryBLL.GetByIdAsync(id);
                return Ok<CountryVM>(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// POST: api/PostAsyncCountry
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Route("api/countriesAsync")]
        public async Task<IHttpActionResult> PostAsync(CountryVM value)
        {
            try
            {
                var x = await _CountryBLL.CreateAsync(value);
                return Ok<CountryVM>(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            };
        }

        /// <summary>
        /// PUT: api/PutAsyncCountry/5
        /// </summary>        
        /// <param name="value"></param>
        [HttpPut]
        [Route("api/countriesAsync")]
        public async Task<IHttpActionResult> PutAsync(CountryVM value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }
                var x = await _CountryBLL.PutAsync(value);

                return Ok<CountryVM>(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        ///// <summary>
        ///// DELETE: api/Country/5
        ///// </summary>
        ///// <param name="id"></param>
        //[HttpDelete]
        //[Route("DeleteAsync")]
        //public IHttpActionResult DeleteAsync(int id)
        //{
        //    try
        //    {
        //        var x = await _CountryBLL.DeleteAsyn(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.ToString());
        //    }
        //}
        #endregion
    }
}
