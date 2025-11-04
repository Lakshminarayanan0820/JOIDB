using Newtonsoft.Json.Bson;

namespace JOIEnergy.Services
{
    public interface IDatabaseConnection
    {
        public void InitializeDatabase();
     //   string connectionString { get; set; }
        public void DisplayElectricityReadingsTable();

    }
}
