using Forum.Core.Aggregates.File.Entities;

namespace Forum.Core.Interfaces;


public interface IFileService
{
    Task<ICollection<FileEntity>> GetFileList();
    Task<ICollection<FileEntity>> GetUserFiles(Guid id);
    Task<FileEntity?> GetFileById(Guid id);
    Task<FileEntity> CreateFile(FileEntity request);
    Task<FileEntity> UpdateFile(Guid id, FileEntity request);
    Task DeleteFile(Guid id);
    Task HardDeleteFile(Guid id);

}
