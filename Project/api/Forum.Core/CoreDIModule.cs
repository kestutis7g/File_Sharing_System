using Autofac;
using Forum.Core.Interfaces;
using Forum.Core.Services;

namespace Forum.Core;

public class CoreDIModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CommentService>()
               .As<ICommentService>()
               .InstancePerLifetimeScope();

        builder.RegisterType<GroupPostService>()
               .As<IGroupPostService>()
               .InstancePerLifetimeScope();

        builder.RegisterType<GroupUserService>()
               .As<IGroupUserService>()
               .InstancePerLifetimeScope();

        builder.RegisterType<GroupService>()
               .As<IGroupService>()
               .InstancePerLifetimeScope();

        builder.RegisterType<PostService>()
               .As<IPostService>()
               .InstancePerLifetimeScope();

        builder.RegisterType<UserService>()
               .As<IUserService>()
               .InstancePerLifetimeScope();

        builder.RegisterType<JwtTokenService>()
               .As<IJwtTokenService>()
               .InstancePerLifetimeScope();

        builder.RegisterType<FileService>()
               .As<IFileService>()
               .InstancePerLifetimeScope();

        builder.RegisterType<LinkService>()
               .As<ILinkService>()
               .InstancePerLifetimeScope();
    }
}
