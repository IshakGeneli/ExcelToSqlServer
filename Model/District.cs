using System.Collections.Generic;

namespace DalistoTask2.Model
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public bool IsActive { get; set; }

        public List<TaxOffice> TaxOffices { get; set; }
    }
}
