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
        /// <summary>
        /// Endpoint to get the best execution plan based on the provided order type and amount.
        /// </summary>
        /// <param name="request">An ExecutionRequest object containing OrderType and Amount.</param>
        /// <returns>An IActionResult containing the execution plan or an error message.</returns>
        [HttpPost("execute")]
        public IActionResult GetBestExecution([FromBody] ExecutionRequest request)
        {
            // Validate the request data.
            if (request == null || string.IsNullOrWhiteSpace(request.OrderType) || request.Amount <= 0)
            {
                return BadRequest(new { message = "Invalid request data." });
            }

            // Load exchange data from JSON files.
            List<ExchangeData> exchanges = LoadExchanges();

            if (exchanges == null || exchanges.Count == 0)
            {
                // Return a 500 Internal Server Error if exchanges could not be loaded.
                return StatusCode(500, new { message = "Failed to load exchange data." });
            }

            // Variables to capture output parameters.
            bool exceedsLimit;
            decimal totalAvailableFunds;

            // Call the GetBestExecution method from the MetaExchangeLogic class.
            var executionPlan = MetaExchangeLogic.GetBestExecution(
                request.OrderType,
                request.Amount,
                exchanges,
                out exceedsLimit,
                out totalAvailableFunds
            );

            if (exceedsLimit)
            {
                // Return a 400 Bad Request if the requested amount exceeds available funds.
                return BadRequest(new
                {
                    message = "Requested amount exceeds available funds.",
                    totalAvailableFunds
                });
            }

            // Return the execution plan as a 200 OK response.
            return Ok(executionPlan);
        }

        /// <summary>
        /// Helper method to load exchanges from JSON files located in the "exchanges" folder.
        /// </summary>
        /// <returns>A list of ExchangeData objects loaded from the JSON files.</returns>
        private List<ExchangeData> LoadExchanges()
        {
            var exchanges = new List<ExchangeData>();

            // Get the path to the "exchanges" folder.
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "exchanges");

            // Check if the directory exists.
            if (Directory.Exists(folderPath))
            {
                // Get all JSON files in the directory.
                foreach (var file in Directory.GetFiles(folderPath, "*.json"))
                {
                    try
                    {
                        // Read the content of each JSON file.
                        var jsonData = System.IO.File.ReadAllText(file);

                        // Deserialize the JSON data into an ExchangeData object.
                        var exchange = JsonConvert.DeserializeObject<ExchangeData>(jsonData);

                        if (exchange != null)
                        {
                            exchanges.Add(exchange);
                        }
                        else
                        {
                            // Case where deserialization fails.
                        }
                    }
                    catch (Exception ex)
                    {
                        // Exceptions during file reading or deserialization.
                    }
                }
            }
            else
            {
                // Case where the "exchanges" directory does not exist.
            }

            return exchanges;
        }
    }
}
