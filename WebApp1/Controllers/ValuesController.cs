using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Fabric;
using Newtonsoft.Json;
using System.Fabric.Description;

namespace WebApp1.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            using (var client = new FabricClient())
            {
                var replicas = client.QueryManager.GetReplicaListAsync(new Guid("927faf78-c01f-4bac-af8f-7bdb0264245b")).Result;
                var json = JsonConvert.SerializeObject(replicas);
                return new string[] {json.ToString()};
            }
              
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            using (var client = new FabricClient())
            {
                var services = client.QueryManager.GetServiceListAsync(new Uri("fabric:/minecraft")).Result;
                var service = services.FirstOrDefault(e => e.ServiceName.AbsolutePath.Contains("minecraft"));
                var updateDescription = new StatelessServiceUpdateDescription();
                updateDescription.InstanceCount = id;
                client.ServiceManager.UpdateServiceAsync(new Uri(service.ServiceName.AbsoluteUri), updateDescription);

            }
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
