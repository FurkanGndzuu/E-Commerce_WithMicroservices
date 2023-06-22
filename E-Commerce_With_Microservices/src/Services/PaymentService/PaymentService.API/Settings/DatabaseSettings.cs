namespace PaymentService.API.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string WalletCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
