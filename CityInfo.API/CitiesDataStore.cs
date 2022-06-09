using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "AMD",
                    Description = "AMD desc",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "Enjoy a lot"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "2nd Central Park",
                            Description =  "Enjoy a lot"
                        }
                    }
                    
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "RJT",
                    Description = "RJT desc",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Zombie Park",
                            Description = "Enjoy a lot"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "2nd Zombie Park",
                            Description =  "Enjoy a lot"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "BRD",
                    Description = "BRD desc",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "Zoo Park",
                            Description = "Enjoy a lot"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 6,
                            Name = "2nd Zoo Park",
                            Description =  "Enjoy a lot"
                        }
                    }
                }
            };

        }
    }
}
