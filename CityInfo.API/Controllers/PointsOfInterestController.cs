using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers{
    
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly CitiesDataStore _citiesDataStore;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService mailService,
            CitiesDataStore citiesDataDtore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _citiesDataStore = citiesDataDtore ?? throw new ArgumentNullException(nameof(citiesDataDtore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                _logger.LogInformation($"City Id {cityId} wasn't found");
                return NotFound();
            }

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{pointOfInterestID}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest( int cityId, int pointOfInterestID)
        {
            try
            {
                //throw new Exception("Example sample");

                var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

                if (city == null)
                {
                    return NotFound();
                }

                var pointOfInterest = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestID);

                if (pointOfInterest == null)
                {
                    return NotFound();
                }

                return Ok(pointOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception occured {cityId} ",ex);
                return StatusCode(500, "A problem happend");
            }
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(
         int cityId, PointOfInterestCreationDto pointOfInterest)
        {

            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest();
                //}

                var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

                if (city == null)
                {
                    return NotFound();
                }

                var maxPointOfIntereset = _citiesDataStore.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

                var finalPointOfInterest = new PointOfInterestDto()
                {
                    Id = ++maxPointOfIntereset,
                    Name = pointOfInterest.Name,
                    Description = pointOfInterest.Description
                };

                city.PointsOfInterest.Add(finalPointOfInterest);

                return CreatedAtRoute(
                    "GetPointOfInterest",
                    new
                    {
                        cityId = cityId,
                        pointOfInterestID = finalPointOfInterest.Id
                    },
                    finalPointOfInterest);
                //return finalPointOfInterest;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }


        [HttpPut("{pointofinterestid}")]
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId,
            PointOfInteresetForUpdateDto pointOfInterest)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }


            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
        }

        [HttpPatch("{pointofinterestid}")]
        public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
            JsonPatchDocument<PointOfInteresetForUpdateDto> patchDocument)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch =
                new PointOfInteresetForUpdateDto()
                {
                    Name = pointOfInterestFromStore.Name,
                    Description = pointOfInterestFromStore.Description
                };

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{pointofinterestid}")]
        public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointOfInterestFromStore);
            _mailService.Send("Jigar Mail Subject", "Jigar Mail Message");
            return NoContent();
        }

    }
}
