using System.Collections.Generic;

namespace DalistoTask2.Model
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public bool IsActive { get; set; }

        List<District> Districts { get; set; }
    }
}
