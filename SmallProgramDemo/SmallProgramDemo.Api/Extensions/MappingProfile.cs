using AutoMapper;
using SmallProgramDemo.Core.Entities;
using SmallProgramDemo.Infrastructure.Resources;

namespace SmallProgramDemo.Api.Extensions
{
    /// <summary>
    /// 配置ResourceModel与EntityModel的属性对应类
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //配置Post到PostResource的映射
            CreateMap<Post, PostResource>()
                //配置指定属性到属性的映射
                .ForMember(
                target => target.UpdateTime,
                entity => entity.MapFrom(src => src.LastModified));

            CreateMap<PostResource, Post>()
                .ForMember(
                target => target.LastModified,
                entity => entity.MapFrom(src => src.UpdateTime));
        }
    }
}
