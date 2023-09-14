using Transportathon._0x80072F78.Core.DTOs.Identity;
using AutoMapper;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.DTOs.Company;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Core.Entities.Offer;

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

        #region Company
        CreateMap<Company,CompanyDTO>().ReverseMap();
        CreateMap<Company,CompanyCreateDTO>().ReverseMap();
        CreateMap<Company,CompanyUpdateDTO>().ReverseMap();
        CreateMap<Comment,CommentDTO>().ReverseMap();
        CreateMap<Comment, CommentCreateDTO>().ReverseMap();
        CreateMap<Comment, CommentUpdateDTO>().ReverseMap();
        CreateMap<Driver,DriverDTO>().ReverseMap();
        CreateMap<Driver, DriverCreateDTO>().ReverseMap();
        CreateMap<Driver, DriverUpdateDTO>().ReverseMap();
        CreateMap<Team,TeamDTO>().ReverseMap();
        CreateMap<Team, TeamCreateDTO>().ReverseMap();
        CreateMap<Team, TeamUpdateDTO>().ReverseMap();
        CreateMap<TeamWorker,TeamWorkerDTO>().ReverseMap();
        CreateMap<TeamWorker, TeamWorkerCreateDTO>().ReverseMap();
        CreateMap<TeamWorker, TeamWorkerUpdateDTO>().ReverseMap();
        CreateMap<Vehicle,VehicleDTO>().ReverseMap();
        CreateMap<Vehicle, VehicleCreateDTO>().ReverseMap();
        CreateMap<Vehicle, VehicleUpdateDTO>().ReverseMap();

        #endregion
        CreateMap<Address, AddressDTO>().ReverseMap();
        CreateMap<Address, AddressCreateDTO>().ReverseMap();
        CreateMap<Address, AddressUpdateDTO>().ReverseMap();
        CreateMap<TransportationRequest, TransportationRequestDTO>().ReverseMap();
        CreateMap<TransportationRequest, TransportationRequestCreateDTO>().ReverseMap();
        CreateMap<TransportationRequest, TransportationRequestUpdateDTO>().ReverseMap();
        CreateMap<Offer, OfferDTO>().ReverseMap();
        CreateMap<Offer, OfferCreateDTO>().ReverseMap();
        CreateMap<Offer, OfferUpdateDTO>().ReverseMap();
    }
}