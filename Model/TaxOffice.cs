namespace DalistoTask2.Model
{
    public class TaxOffice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
    }
}
