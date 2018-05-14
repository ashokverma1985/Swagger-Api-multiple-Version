using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebapiApp.Controllers.V1
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    public class AbcController : ApiController
    {

        [MapToApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/Abc/Get")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
