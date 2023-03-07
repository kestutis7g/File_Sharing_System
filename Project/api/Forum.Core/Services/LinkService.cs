using Microsoft.Extensions.Options;
using Forum.Core.Aggregates.Link.Entities;
using Forum.Core.Interfaces;
using Forum.Shared.Interfaces;
using Forum.Shared.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Forum.Core.Aggregates.Link.Specs;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Forum.Core.Services;

public class LinkService : ILinkService
{
    public LinkService(IRepository<LinkEntity> linkRepo)
    {
        LinkRepo = linkRepo ?? throw new ArgumentNullException(nameof(linkRepo));
    }
    private IRepository<LinkEntity> LinkRepo { get; }


    public async Task<ICollection<LinkEntity>> GetLinkList()
    {
        return await LinkRepo.ListAsync();
    }

    public async Task<LinkEntity?> GetLinkById(Guid id, string? password)
    {
        LinkEntity entity = await LinkRepo.GetByIdAsync(id);
        if(entity.Password != null)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: entity.Salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            if(hashed == entity.Password)
                return entity;
            else
                return null;
        }
        else
        {
            return entity;
        }
        
    }

    public async Task<LinkEntity?> GetLinkById(Guid id)
    {
        return await LinkRepo.GetByIdAsync(id);
    }

    public async Task<LinkEntity> CreateLink(LinkEntity request)
    {
        return await LinkRepo.AddAsync(request); 
    }

    public async Task<LinkEntity> HashEntityPassword(LinkEntity request)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: request.Password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        request.Password = hashed;
        request.Salt = salt;

        return request;
    }

    public async Task<LinkEntity> UpdateLink(Guid id, LinkEntity request)
    {
        var link = await GetLinkById(id);
        if (link is null)
        {
            throw new ArgumentNullException(nameof(link));
        }
        link.Update(request);
        await LinkRepo.SaveChangesAsync();
        return link;

    }

    public async Task DeleteLink(Guid id)
    {
        var link = await GetLinkById(id);
        if (link is null)
        {
            throw new ArgumentNullException(nameof(link));
        }
        await LinkRepo.DeleteAsync(link);

    }

    public async Task<bool> IsPasswordRequired(Guid id)
    {
        LinkEntity? entity = await LinkRepo.GetByIdAsync(id);
        if(entity is null)
        {
            return false;
        }

        return entity.Password != null;
    }
    
}
