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
        readonly List<ValueResponse> values;
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
        public IActionResult Post([FromBody] int value)
        {
            var valueResponse = new ValueResponse
            {
                Id = value,
                Value = value.ToString()
            };
            this.values.Add(valueResponse);

            return this.HATEOASResult(valueResponse, 
                (v) => this.CreatedAtRoute(RouteNames.Values_GetById, new { id = value } ,v));
        }

        // PUT api/values/5
        [HttpPut("{id}", Name = RouteNames.Values_Update)]
        public IActionResult Put(int id, [FromBody] string value)
        {
            var valueResponse = this.values.FirstOrDefault(x => x.Id == id);
            if (valueResponse == null)
                return this.NotFound();

            return this.NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}", Name = RouteNames.Values_Delete)]
        public void Delete(int id)
        {
            this.values.RemoveAll(x => x.Id == id);
        }
    }
}
