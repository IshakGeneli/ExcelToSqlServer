using DalistoTask2.ExcelModels;
using DalistoTask2.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DalistoTask2
{
    internal class Program
    {
        static List<T> GetData<T>(string file)
        {

            using (ExcelPackage package = new ExcelPackage(new FileInfo(file)))
            {
                List<T> list = new List<T>();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var sheet = package.Workbook.Worksheets["Sheet1"];

                return list = new Program().GetExcelData<T>(sheet);

            }
        }
        static void Main(string[] args)
        {
            #region Coountry
            var excelCountryList = GetData<ExcelCountry>(@"C:\Users\ASUS\source\repos\DalistoTask2\Books\ulke.xlsx");

            List<Country> list = new List<Country>();

            foreach (var excelCountry in excelCountryList)
            {
                var c = new Country()
                {
                    Code = excelCountry.Code,
                    Name = excelCountry.Name
                };
                list.Add(c);
            }
            #endregion

            #region City
            var excelCityList = GetData<ExcelCity>(@"C:\Users\ASUS\source\repos\DalistoTask2\Books\il.xlsx");

            List<City> list2 = new List<City>();

            foreach (var excelCity in excelCityList)
            {
                var c = new City()
                {
                    CountryId = excelCity.CountryId,
                    Name = excelCity.Name
                };
                list2.Add(c);
            }
            #endregion

            #region District
            List<City> cities = new List<City>();
            using (AppContext context = new AppContext())
            {
                cities = context.Cities.ToList();
            }

            var excelDistrictList = GetData<ExcelDistrict>(@"C:\Users\ASUS\source\repos\DalistoTask2\Books\ilce.xlsx");

            var list3 = (from district in excelDistrictList
                         join city in cities on district.CityName equals city.Name
                         select new District()
                         {
                             Name = district.Name,
                             CityId = city.Id
                         }).ToList();


            #endregion

            #region TaxOffice
            List<District> districts = new List<District>();

            using (AppContext context = new AppContext())
            {
                districts = context.Districts.ToList();
            }

            var excelTaxOfficeList = GetData<ExcelTaxOffice>(@"C:\Users\ASUS\source\repos\DalistoTask2\Books\vergi.xlsx");

            var list4 = (from taxOffice in excelTaxOfficeList
                         join distirct in districts on taxOffice.DistrictName equals distirct.Name
                         select new TaxOffice()
                         {
                             Name = taxOffice.Name,
                             DistrictId = distirct.Id
                         }).ToList();


            #endregion

            using (AppContext context = new AppContext())
            {
                context.Countries.AddRange(list);
                context.Cities.AddRange(list2);
                context.Districts.AddRange(list3);
                context.TaxOffices.AddRange(list4);
                //context.SaveChanges();
            }
        }


        private List<T> GetExcelData<T>(ExcelWorksheet worksheet)
        {
            List<T> list = new List<T>();
            var columnInfo = Enumerable.Range(1, worksheet.Dimension.Columns).ToList().Select(n =>

                new { Index = n, ColumnName = worksheet.Cells[1, n].Value.ToString() }
            );

            for (int row = 2; row < worksheet.Dimension.Rows; row++)
            {
                T obj = (T)Activator.CreateInstance(typeof(T));
                foreach (var prop in typeof(T).GetProperties())
                {
                    int col = columnInfo.SingleOrDefault(c => c.ColumnName == prop.Name).Index;
                    var val = worksheet.Cells[row, col].Value;
                    var propType = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType(val, propType));

                }
                list.Add(obj);
            }
            return list;
        }

    }

}
