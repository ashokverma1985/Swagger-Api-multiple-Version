using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebapiApp.Controllers
{
    //https://stackoverflow.com/questions/44376295/how-to-configure-multipleapiversions-in-swashbuckle-using-aspnet-apiversioning?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
    [ApiVersion("1")]
    [ApiVersion("2")]
    //[RoutePrefix("api/v{version:apiVersion}/Values")]
    public class ValuesController : ApiController
    {
        
        [MapToApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/values/Get")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [MapToApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/values/Get/5")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/values")]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [MapToApiVersion("2.0")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [MapToApiVersion("2.0")]
        public void Delete(int id)
        {
        }
    }
}
