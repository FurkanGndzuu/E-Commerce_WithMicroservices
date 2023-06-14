namespace CatalogService.API.Abstractions
{
    public interface IStorageService
    {
        public Task<IList<string>> AddFile(IFormFileCollection files ,string  PathOrContainerName = "");
    }
}
