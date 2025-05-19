using AutoMapper;
using SalesTrack.Application.Handlers.Authentication.Commands.SignUpUser;
using SalesTrack.Application.Handlers.Products.Commands.AddProduct;
using SalesTrack.Contracts.Dto;

namespace SalesTrack.Api.Mappings;

public class AuthenticationMappingProfile : Profile
{
    public AuthenticationMappingProfile()
    {
        CreateMap<SignupFormDTO, SingUpUserCommand>();
    }
}
