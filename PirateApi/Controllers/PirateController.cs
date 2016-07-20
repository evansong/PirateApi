using StackExchange.Redis;
using System.Web.Http;

namespace PirateApi.Controllers
{
    [RoutePrefix("api")]
    public class PirateController : ApiController
    {       
        [HttpGet]
        [Route("pirates")]
        public string Get(string phrase)
        {
            using (var connection = ConnectionMultiplexer.Connect("connectionString"))
            {
                var cache = connection.GetDatabase();
                char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
                var tokenized = phrase.Split(delimiterChars);

                for (var i = tokenized.Length - 1; i >= 0; i--)
                {
                    var keyValue = cache.StringGet(tokenized[i]);

                    if (keyValue.HasValue)
                    {
                        tokenized[i] = keyValue.ToString();
                    }
                }

                return string.Join(" ", tokenized);
            }
        }

        [HttpPost]
        [Route("pirates")]
        // POST api/values
        public void Post(string originalValue, string pirateValue)
        {
            using (var connection = ConnectionMultiplexer.Connect("connectionString"))
            {
                var cache = connection.GetDatabase();

                cache.StringSet(originalValue, pirateValue);
            }
        }

        // PUT api/values/5
        [HttpPut]
        [Route("pirates")]
        public void Put(string originalValue, string updatedValue)
        {
            using (var connection = ConnectionMultiplexer.Connect("connectionString"))
            {
                var cache = connection.GetDatabase();

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

            using (var connection = ConnectionMultiplexer.Connect("connectionString"))
            {
                var cache = connection.GetDatabase();

                cache.KeyDelete(originalValue);
            }
        }
    }
}