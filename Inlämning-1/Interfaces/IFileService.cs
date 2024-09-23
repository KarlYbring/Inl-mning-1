using Inlämning_1.Models;

namespace Inlämning_1.Interfaces;

public interface IFileService
{
    public ResultResponse<string> SaveToFile(string content);
    public ResultResponse<string> GetFromFile();
}
