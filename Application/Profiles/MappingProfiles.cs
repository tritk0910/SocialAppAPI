using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Post, PostDto>();
        CreateMap<PostDto, Post>();
        CreateMap<Comment, CommentDto>();
        CreateMap<CommentDto, Comment>();
    }
}