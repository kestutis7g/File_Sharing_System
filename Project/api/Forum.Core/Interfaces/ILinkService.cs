using Forum.Core.Aggregates.File.Entities;
using Forum.Core.Aggregates.Link.Entities;

namespace Forum.Core.Interfaces;


public interface ILinkService
{
    Task<ICollection<LinkEntity>> GetLinkList();
    Task<LinkEntity?> GetLinkById(Guid id);
    Task<LinkEntity?> GetLinkById(Guid id, string? password);
    Task<LinkEntity> CreateLink(LinkEntity request);
    Task<LinkEntity> UpdateLink(Guid id, LinkEntity request);
    Task DeleteLink(Guid id);
    Task<LinkEntity> HashEntityPassword(LinkEntity request);
    Task<bool> IsPasswordRequired(Guid id);


}
