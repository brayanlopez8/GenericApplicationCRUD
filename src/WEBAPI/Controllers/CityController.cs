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
    /// CityController class
    /// </summary>
    public class CityController : ApiController
    {
        private ICityBLL _CityBLL;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CityBLL"></param>
        public CityController(ICityBLL CityBLL)
        {
            this._CityBLL = CityBLL;
        }

        /// <summary>
        /// GET: api/GetAllCity
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/cities")]
        public IEnumerable<CityVM> GetAll()
        {
            return _CityBLL.GetList();
        }

        /// <summary>
        /// GET: api/GetCity/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/cities/{id}")]
        public CityVM Get(int id)
        {
            return _CityBLL.GetById(id);
        }

        /// <summary>
        /// POST: api/PostCity
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Route("api/cities")]
        public void Post(CityVM value)
        {
            _CityBLL.Create(value);
        }

        /// <summary>
        /// PUT: api/PutCity/5
        /// </summary>        
        /// <param name="value"></param>
        [HttpPut]
        [Route("api/cities")]
        public IHttpActionResult Put(CityVM value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }
                var x = _CityBLL.Put(value);

                return Ok<CityVM>(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        ///// <summary>
        ///// DELETE: api/City/5
        ///// </summary>
        ///// <param name="id"></param>
        //[HttpDelete]
        //public void Delete(int id)
        //{
        //}

        #region Metoths Async

        /// <summary>
        /// GET: api/GetAllAsynccities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/citiesAsync")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            try
            {
                var x = await _CityBLL.GetListAsync();
                return Ok<List<CityVM>>(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// GET: api/GetAsyncCity/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/citiesAsync/{id}")]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            try
            {
                var x = await _CityBLL.GetByIdAsync(id);
                return Ok<CityVM>(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// POST: api/PostAsyncCity
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Route("api/citiesAsync")]
        public async Task<IHttpActionResult> PostAsync(CityVM value)
        {
            try
            {
                var x = await _CityBLL.CreateAsync(value);
                return Ok<CityVM>(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            };
        }

        /// <summary>
        /// PUT: api/PutAsyncCity/5
        /// </summary>        
        /// <param name="value"></param>
        [HttpPut]
        [Route("api/citiesAsync")]
        public async Task<IHttpActionResult> PutAsync(CityVM value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }
                var x = await _CityBLL.PutAsync(value);

                return Ok<CityVM>(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        ///// <summary>
        ///// DELETE: api/City/5
        ///// </summary>
        ///// <param name="id"></param>
        //[HttpDelete]
        //[Route("DeleteAsync")]
        //public IHttpActionResult DeleteAsync(int id)
        //{
        //    try
        //    {
        //        var x = await _CityBLL.DeleteAsyn(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.ToString());
        //    }
        //}
        #endregion
    }
}
