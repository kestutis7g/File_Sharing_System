using Microsoft.Extensions.Options;
using Forum.Core.Aggregates.File.Entities;
using Forum.Core.Interfaces;
using Forum.Shared.Interfaces;
using Forum.Shared.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Forum.Core.Aggregates.File.Specs;
using Microsoft.AspNetCore.Diagnostics;

namespace Forum.Core.Services;

public class FileService : IFileService
{
    public FileService(IRepository<FileEntity> fileRepo)
    {
        FileRepo = fileRepo ?? throw new ArgumentNullException(nameof(fileRepo));
    }
    private IRepository<FileEntity> FileRepo { get; }


    public async Task<ICollection<FileEntity>> GetFileList()
    {
        return await FileRepo.ListAsync();
    }

    public async Task<FileEntity?> GetFileById(Guid id)
    {
        return await FileRepo.GetByIdAsync(id);
    }

    public async Task<FileEntity> CreateFile(FileEntity request)
    {
        return await FileRepo.AddAsync(request); 
    }

    public async Task<FileEntity> UpdateFile(Guid id, FileEntity request)
    {
        var file = await GetFileById(id);
        if(file is null)
        {
            throw new ArgumentNullException(nameof(file));
        }
        file.Update(request);
        await FileRepo.SaveChangesAsync();
        return file;

    }

    public async Task DeleteFile(Guid id)
    {
        var file = await GetFileById(id);
        if (file is null)
        {
            throw new ArgumentNullException(nameof(file));
        }
        //file.MarkDeleted();
        await FileRepo.SaveChangesAsync();

    }

    public async Task HardDeleteFile(Guid id)
    {
        var file = await GetFileById(id);
        if (file is null)
        {
            throw new ArgumentNullException(nameof(file));
        }
        await FileRepo.DeleteAsync(file);

    }
}
