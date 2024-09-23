using Inlämning_1.Interfaces;
using Inlämning_1.Models;
using System.Runtime.CompilerServices;

namespace Inlämning_1.Services;
public class FileService(string filePath) : IFileService
{
    private readonly string _filePath = filePath;
    public ResultResponse<string> GetFromFile()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                return new ResultResponse<string> { Succeeded = false, Message = "Fil ej hittad" };
            }
            using var sr = new StreamReader(_filePath);
            var content = sr.ReadToEnd();
            return new ResultResponse<string> { Succeeded = true, Message = content};
        }
        catch (Exception ex)
        {
            return new ResultResponse<string> { Succeeded = false, Message = ex.Message };
        }
    }
    public ResultResponse<string> SaveToFile(string product)
    {
        try
        {
            using var sw = new StreamWriter(_filePath, false);
            sw.WriteLine(product);
            return new ResultResponse<string> { Succeeded = true, };
        }
        catch (Exception ex)
        {
            return new ResultResponse<string> { Succeeded = false, Message=ex.Message };
        }
    }
}
