namespace ComplationService.API.Settings
{
    public interface IDatabaseSettings
    {
        public string ComplaintCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
