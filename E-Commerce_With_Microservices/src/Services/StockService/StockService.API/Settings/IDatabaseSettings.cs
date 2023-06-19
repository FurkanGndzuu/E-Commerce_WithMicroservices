namespace StockService.API.Settings
{
    public interface IDatabaseSettings
    {
        public string StockCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
