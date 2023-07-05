using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using ShoeProject.Domain.DTO;
using ShoeProject.Domain.Repository;
using ShoeTracker.Core;

namespace ShoeTracker.Controllers
{
    [RoutePrefix("api/Values")]
    public class ValuesController : ApiController
    {
        //comment
        private IShoeTrackerRepository shoeTrackerRepository;

        private ShoeTrackerDatabaseRepo shoeTrackerDataRepository;

        public ValuesController()
        {
            this.shoeTrackerDataRepository =
                new ShoeTrackerDatabaseRepo(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            this.shoeTrackerRepository = new ShoeTrackerRepository(shoeTrackerDataRepository);
        }

        // GET api/<controller>
        [Route("")]
        [HttpGet]
        public IEnumerable<Names> GetNames()
        {
            return shoeTrackerRepository.GetNames();
        }

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}
