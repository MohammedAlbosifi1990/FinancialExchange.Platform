using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace Shared.Core.Domain.Extensions;

public static partial class FilesExtensions
{
    #region ImagesExtensions

    public static (bool Success,string Name) RandomName(this IFormFile? file)
    {
        if (file == null)
            return (false,"");

        var lastDot = file.FileName.LastIndexOf(".", StringComparison.Ordinal);
        var name = file.FileName[..lastDot];
        var extension = file.FileName.Substring(lastDot, file.FileName.Length-lastDot);

        name = name +"_"+ Guid.NewGuid().ToString().Replace("-", "")+extension;
        return (true,name);
    }
    
    public static (bool Success,string Name) RandomName(this string? fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return (false, "");
        var lastDot = fileName.LastIndexOf(".", StringComparison.Ordinal);
        var name = fileName[..lastDot];
        var extension = fileName.Substring(lastDot, fileName.Length-lastDot);

        name = name +"_"+ Guid.NewGuid().ToString().Replace("-", "")+ extension;
        return (true,name);
    }
    
    public static async Task<(bool Success,string? Path)> SaveTo(this IFormFile? file, string path)
    {
        if (string.IsNullOrEmpty(path))
            return (false, null);
        
        if (file==null)
            return (false, "File Is Required");
        var filename = file.RandomName();
        if (!filename.Success)
            return (false,"Some Error With File Name 2222222222222222222222222222222");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        var imagePath = Path.Combine(path, filename.Name);
        await using var stream =File.OpenWrite(imagePath);
        await file.CopyToAsync(stream);
        return (true,imagePath);
    }
    
    public static async Task<(bool Success,string? Path)> Replace(this IFormFile? file, string path, string? oldFileName)
    {
        if (string.IsNullOrEmpty(path))
            return (false, null);
        
        if (file==null)
            return (false, "File Is Required");
        
        var filename = file.RandomName();
        if (!filename.Success)
            return (false,"Some Error With File Name");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        
        if (!string.IsNullOrEmpty(oldFileName))
            File.Delete(oldFileName);
        
        var imagePath = Path.Combine(path, filename.Name);
        await using var stream =File.OpenWrite(imagePath);
        await file.CopyToAsync(stream);
        return (true,imagePath);
    }

    public static  (bool Success, string? Error) Remove(this string path)
    {
        if (!File.Exists(path))
            return (false, "File NotFound");
        File.Delete(path);
        return (true, null);
    }

    public static bool IsImage(this string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                return false;
            if (! MyRegex().IsMatch(name))
                return false;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public static bool IsImage(this IFormFile? file)
    {
        try
        {
           return file != null && file.FileName.IsImage();
        }
        catch (Exception)
        {
            return false;
        }
    }
    [GeneratedRegex(@"^.*\.(jpg|JPG|jpeg|JPEG|png|png)$")]
    private static partial Regex MyRegex();
    
    
    #endregion

    #region Pdf

    public static bool IsPdf(this string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                return false;
            if (! MyPdfRegex().IsMatch(name))
                return false;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    [GeneratedRegex(@"^.*\.(pdf|PDF)$")]
    private static partial Regex MyPdfRegex();

    #endregion

    

}