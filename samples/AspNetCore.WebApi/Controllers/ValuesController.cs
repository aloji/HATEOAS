using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IEnumerable<ValueResponse> values;
        public ValuesController()
        {
            this.values = new List<ValueResponse>()
            {
                new ValueResponse { Id = 1, Value = "1" },
                new ValueResponse { Id = 2, Value = "2" },
            };
        }

        // GET api/values
        [HttpGet(Name = RouteNames.Values_Get)]
        public IActionResult Get()
        {
            return this.HATEOASResult(this.values, (v) => this.Ok(v));
        }

        // GET api/values/5
        [HttpGet("{id}", Name = RouteNames.Values_GetById)]
        public IActionResult Get(int id)
        {
            var value = this.values.FirstOrDefault(x => x.Id == id);
            if (value == null)
                return this.NotFound();

            return this.HATEOASResult(value, (v) => this.Ok(v));
        }

        // POST api/values
        [HttpPost(Name = RouteNames.Values_Post)]
        public void Post([FromBody] int value)
        {
            var valueResponse = new ValueResponse
            {
                Id = value,
                Value = value.ToString()
            }
            ;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
