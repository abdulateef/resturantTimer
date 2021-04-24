using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResturantOpenningHours.API.Querry;
using ResturantOpenningHours.Model.ApplicationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResturantOpenningHours.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateFormater : ControllerBase
    {
        private readonly ILogger<DateFormater> _logger;
        private readonly IMediator _mediator;


        public DateFormater(ILogger<DateFormater> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        [HttpPost, Route("GetOpenHours")]
        /// <summary>  
        /// This action enables users to format Unix time into UTC date time and returns the open hours for the business provided 
        /// </summary>  
        /// <param name="OpenningAndClosingHoursReqest">Model to Format unix time from JSON</param>  
        /// <returns>Returns the created customer</returns>  
        /// <response code="200">Returned if operation was successful</response>  
        /// <response code="400">Returned if the model couldn&#8217;t be parsed </response>  
        /// <response code="422">Returned when the validation failed</response>
        public IActionResult GetOpenHours([FromBody] OpenningAndClosingHoursReqest Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                      _logger.LogInformation("Model is Valid");
                    var Querry = new GetOpenningAndClosingHoursQuerry(Model);
                    var result = _mediator.Send(Querry);
                    if(result.Result != null || !result.IsFaulted) return new OkObjectResult(result.Result);
                   return BadRequest();

                }
                return BadRequest(ModelState);

            }
            catch
            {
                return BadRequest();

            }
        }
        
    }
}
