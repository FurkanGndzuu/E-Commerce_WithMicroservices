using CatalogService.API.Abstractions;

namespace CatalogService.API.Services
{
    public class LocalStorageService : IStorageService
    {
        public async  Task<IList<string>> AddFile(IFormFileCollection files, string PathOrContainerName = "")
        {
            IList<string> result = new List<string>();
           
           foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    result.Add(filePath);
                }
            }
           return result;
        }
    }
}
