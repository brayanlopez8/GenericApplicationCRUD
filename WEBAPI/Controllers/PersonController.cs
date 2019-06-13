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
    /// PersonController class
    /// </summary>
    public class PersonController : ApiController
    {
        private IPersonBLL _personBLL;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="personBLL"></param>
        public PersonController(IPersonBLL personBLL)
        {
            this._personBLL = personBLL;
        }

        /// <summary>
        /// GET: api/Person
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok<IEnumerable<PersonVM>>(_personBLL.GetList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// GET: api/PersonAsyc
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/GetAllAsync")]
        public async Task<IHttpActionResult> GetAsync()
        {
            try
            {
                var x = await _personBLL.GetListAsync();
                return Ok<List<PersonVM>>(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        /// <summary>
        /// GET: api/Person/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok<PersonVM>(_personBLL.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// GET: api/PersonAsync/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/GetAsync")]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            try
            {
                var x = await _personBLL.GetByIdAsync(id);
                return Ok<PersonVM>(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// POST: api/Person
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public IHttpActionResult Post(PersonVM value)
        {
            try
            {
                return Ok<PersonVM>(_personBLL.Create(value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// POST: api/PersonAsync
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/PostAsyc")]
        public async Task<IHttpActionResult> PostAsyc(PersonVM value)
        {
            try
            {
                var x = await _personBLL.CreateAsync(value);
                return Ok<PersonVM>(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// PUT: api/Person/5
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Put(PersonVM value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }
                var x = _personBLL.Put(value);

                return Ok<PersonVM>(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// PUT: api/Person/5
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/PutAsyc")]
        public async Task<IHttpActionResult> PutAsync(PersonVM value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }
                var x = await _personBLL.PutAsync(value);

                return Ok<PersonVM>(x);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // DELETE: api/Person/5
        //public void Delete(int id)
        //{
        //}
    }
}
