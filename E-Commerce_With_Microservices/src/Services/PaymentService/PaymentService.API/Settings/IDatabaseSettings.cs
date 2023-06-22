namespace PaymentService.API.Settings
{
    public interface IDatabaseSettings
    {
        public string WalletCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
