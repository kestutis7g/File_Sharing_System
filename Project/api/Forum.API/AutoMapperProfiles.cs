using AutoMapper;
using Forum.Core.Common;
using Forum.API.Models;
using Forum.Core.Aggregates.Comment.Entities;
using Forum.Core.Aggregates.Group.Entities;
using Forum.Core.Aggregates.GroupPost.Entities;
using Forum.Core.Aggregates.GroupUser.Entities;
using Forum.Core.Aggregates.Post.Entities;
using Forum.Core.Aggregates.User.Entities;

using Forum.Core.Aggregates.File.Entities;
using Forum.Core.Aggregates.Link.Entities;

using Forum.API.Models.File;

namespace Forum.API;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap(typeof(IPaginatedCollection<>), typeof(PaginatedList<>));
        CreateMap<CommentEntity, CommentModel>().ReverseMap();
        CreateMap<GroupEntity, GroupModel>().ReverseMap();
        CreateMap<GroupPostEntity, GroupPostModel>().ReverseMap();
        CreateMap<GroupUserEntity, GroupUserModel>().ReverseMap();
        CreateMap<PostEntity, PostModel>().ReverseMap();
        CreateMap<UserEntity, UserModel>().ReverseMap();

        CreateMap<FileEntity, FileModel>().ReverseMap();
        CreateMap<LinkEntity, LinkModel>().ReverseMap();


    }
}
