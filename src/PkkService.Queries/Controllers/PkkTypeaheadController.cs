using PkkService.Queries.Application.Common;
using PkkService.Queries.Application.Queries.Typeahead;
using PkkService.Queries.Models;
using System.Net;

namespace PkkService.Queries.Controllers
{
    [ApiController]
    [Route("api/typeahead")]
    public class PkkTypeaheadController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PkkTypeaheadController> _logger;

        public PkkTypeaheadController(IMediator mediator, ILogger<PkkTypeaheadController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Возвращает список объектов на переданном слое, содержащих переданный текст
        /// </summary>
        /// <param name="layer">Слой ПКК</param>
        /// <param name="text">Текст для поиска</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{layer}")]
        [ProducesResponseType(typeof(TypeaheadResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.GatewayTimeout)]
        public async Task<IActionResult> FindPkkObjectsAddressOnLayer(LayersType layer, [FromQuery] string text, CancellationToken cancellationToken)
        {
            try
            {
                var pkkObjectsAddress = await _mediator.Send(new PkkObjectsAddressQuery
                {
                    PkkLayer = (int)layer,
                    Text = text
                }, cancellationToken);

                if (pkkObjectsAddress is null)
                    return Ok(new TypeaheadResponseModel());

                return Ok(new TypeaheadResponseModel
                {
                    Total = pkkObjectsAddress.Count(),
                    Results = pkkObjectsAddress?.Select(x => new TypeaheadObjectModel()
                    {
                        Title = x.Title,
                        Value = x.Value
                    })
                });
            }
            catch(UnvaliablePkkServiceException ex)
            {
                _logger.LogError($"PkkTypeaheadService Error: {ex.Message}");

                return StatusCode((int)HttpStatusCode.GatewayTimeout, new());
            }
        }
    }
}
