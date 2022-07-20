using System.Collections.Generic;

namespace DalistoTask2.Model
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; } = true;

        List<City> Cities { get; set; }
    }
}
