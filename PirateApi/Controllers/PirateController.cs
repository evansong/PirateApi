using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PirateApi.Controllers
{
    [RoutePrefix("api")]
    public class PirateController : ApiController
    {
        public PirateController()
        {
        }
        
        [HttpGet]
        [Route("pirates")]
        public string Get(string phrase)
        {
            // Connection refers to a property that returns a ConnectionMultiplexer
            // as shown in the previous example.
            using (ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("connectionString"))
            {
                IDatabase cache = connection.GetDatabase();
                char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
                var tokenized = phrase.Split(delimiterChars);

                for (var i = tokenized.Length - 1; i >= 0; i--)
                {
                    var keyValue = cache.StringGet(tokenized[i]);

                    if (keyValue.HasValue)
                    {
                        tokenized[i] = keyValue.ToString();
                    }

                    //foreach (var tokenize in phrase.Split(delimiterChars))
                    //{

                    //}
                }

                return string.Join(" ", tokenized);
            }
        }

        [HttpPost]
        [Route("pirates")]
        // POST api/values
        public void Post(string originalValue, string pirateValue)
        {
            // Connection refers to a property that returns a ConnectionMultiplexer
            // as shown in the previous example.
            using (ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("connectionString"))
            {
                IDatabase cache = connection.GetDatabase();

                cache.StringSet(originalValue, pirateValue);
            }
        }

        // PUT api/values/5
        [HttpPut]
        [Route("pirates")]
        public void Put(string originalValue, string updatedValue)
        {
            // Connection refers to a property that returns a ConnectionMultiplexer
            // as shown in the previous example.

            using (ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("connectionString"))
            {
                IDatabase cache = connection.GetDatabase();

                cache.StringSet(originalValue, updatedValue);
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("pirates")]
        public void Delete(string originalValue)
        {
            // Connection refers to a property that returns a ConnectionMultiplexer
            // as shown in the previous example.

            using (ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("connectionString"))
            {
                IDatabase cache = connection.GetDatabase();

                cache.KeyDelete(originalValue);
            }
        }
    }
}
