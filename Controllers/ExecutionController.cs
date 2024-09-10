using MetaExchangeWPF;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MetaExchangeAPI.Models;


namespace MetaExchangeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExecutionController : ControllerBase
    {
        // Endpoint to get the best execution plan
        [HttpPost("execute")]
        public IActionResult GetBestExecution([FromBody] ExecutionRequest request)
        {
            // Load exchange data from JSON files
            List<ExchangeData> exchanges = LoadExchanges();

            // Call the GetBestExecution method from your MetaExchangeLogic class
            bool exceedsLimit;
            decimal totalAvailableFunds;
            var executionPlan = MetaExchangeLogic.GetBestExecution(
                request.OrderType,
                request.Amount,
                exchanges,
                out exceedsLimit,
                out totalAvailableFunds
            );

            if (exceedsLimit)
            {
                return BadRequest(new { message = "Requested amount exceeds available funds", totalAvailableFunds });
            }

            return Ok(executionPlan);
        }

        // Helper function to load the exchanges from JSON files
        private List<ExchangeData> LoadExchanges()
        {
            List<ExchangeData> exchanges = new List<ExchangeData>();
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "exchanges");

            if (Directory.Exists(folderPath))
            {
                foreach (var file in Directory.GetFiles(folderPath, "*.json"))
                {
                    var jsonData = System.IO.File.ReadAllText(file);
                    var exchange = JsonConvert.DeserializeObject<ExchangeData>(jsonData);
                    exchanges.Add(exchange);
                }
            }

            return exchanges;
        }
    }
}
