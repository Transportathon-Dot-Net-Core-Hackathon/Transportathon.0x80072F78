using Transportathon._0x80072F78.Core.DTOs.Identity;
using AutoMapper;
using Transportathon._0x80072F78.Core.Entities.Identity;

namespace Transportathon._0x80072F78.Core.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region User

        CreateMap<AspNetUser, UserDTO>().ReverseMap();

        CreateMap<AspNetUser, CreateUserDTO>().ReverseMap();

        CreateMap<AspNetUser, LoginDTO>().ReverseMap();

        #endregion
    }
}