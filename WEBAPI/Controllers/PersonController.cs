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
    public class PersonController : ApiController
    {
        private IPersonBLL _personBLL;
        public PersonController(IPersonBLL personBLL)
        {
            this._personBLL = personBLL;
        }

        // GET: api/Person
        public IEnumerable<PersonVM> Get()
        {
            return _personBLL.GetList();
        }

        //GET: api/PersonAsyc
        [Route("api/GetAllAsync")]
        public async Task<IEnumerable<PersonVM>> GetAsync()
        {
            return await _personBLL.GetListAsync();
        }

        // GET: api/Person/5
        public PersonVM Get(int id)
        {
            return _personBLL.GetById(id);
        }

        // GET: api/PersonAsync/5
        [Route("api/GetAsync")]
        public async Task<PersonVM> GetAsync(int id)
        {
            return await _personBLL.GetByIdAsync(id);
        }

        // POST: api/Person
        public void Post(PersonVM value)
        {
            _personBLL.Create(value);
        }
        // POST: api/PersonAsync
        [Route("api/PostAsyc")]
        
        public async Task PostAsyc(PersonVM value)
        {
            var x = await _personBLL.CreateAsync(value);
        }

        // PUT: api/Person/5
        public IHttpActionResult Put(int id, PersonVM value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            _personBLL.PutAsync(value);

            return Ok<PersonVM>(new PersonVM());


        }

        // DELETE: api/Person/5
        //public void Delete(int id)
        //{
        //}
    }
}
