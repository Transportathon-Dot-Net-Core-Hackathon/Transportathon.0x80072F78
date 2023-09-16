using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Core.DTOs.Company;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Shared.Interfaces;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services.ForCompany;

public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextData _httpContextData;
    private readonly UserManager<AspNetUser> _userManager;
    public CompanyService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextData httpContextData, UserManager<AspNetUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextData = httpContextData;
        _userManager = userManager;
    }

    public async Task<CustomResponse<NoContent>> CreateAsync(CompanyCreateDTO companyCreateDTO)
    {
        await _unitOfWork.BeginTransactionAsync();

        AspNetUser aspNetUser = _mapper.Map<AspNetUser>(companyCreateDTO.User);

        await _userManager.CreateAsync(aspNetUser, companyCreateDTO.User.Password);
        await _unitOfWork.SaveAsync();

        var mappedCompany = _mapper.Map<Company>(companyCreateDTO);
        mappedCompany.CompanyUsersId = aspNetUser.Id;

        await _unitOfWork.CompanyRepository.CreateAsync(mappedCompany);
        await _unitOfWork.SaveAsync();

        await _unitOfWork.CommitAsync();

        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
    public async Task<CustomResponse<NoContent>> DeleteAsync(Guid id)
    {
        var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
        if (company == null)
        {
           return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(company));
        }
        await _unitOfWork.CompanyRepository.DeleteAsync(company);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }

    public async Task<CustomResponse<List<CompanyDTO>>> GetAllAsync()
    {
        var companyList = await _unitOfWork.CompanyRepository.GetAllAsync();
        return CustomResponse<List<CompanyDTO>>.Success(StatusCodes.Status200OK, _mapper.Map<List<CompanyDTO>>(companyList));
    }

    public async Task<CustomResponse<CompanyDTO>> GetByIdAsync(Guid id)
    {
        var company =  await _unitOfWork.CompanyRepository.GetByIdAsync(id);
        if (company == null)
        {
            return CustomResponse<CompanyDTO>.Fail(StatusCodes.Status404NotFound, nameof(company));
        }
        var companyDTO = _mapper.Map<CompanyDTO>(company);
        return CustomResponse<CompanyDTO>.Success(StatusCodes.Status200OK , companyDTO);
    }

    public async Task<CustomResponse<List<CompanyDTO>>> MyCompainesAsync()
    {
        var companyList = await _unitOfWork.CompanyRepository.GetAllByFilterAsync(x => x.CompanyUsersId == Guid.Parse(_httpContextData.UserId)
                                                    , null, $"{nameof(Core.Entities.ForCompany.Company.CompanyUsers)}");
        if (companyList == null)
            return CustomResponse<List<CompanyDTO>>.Fail(StatusCodes.Status404NotFound, nameof(Core.Entities.ForCompany.Company));

        var result = _mapper.Map<List<CompanyDTO>>(companyList);

        return CustomResponse<List<CompanyDTO>>.Success(StatusCodes.Status200OK, result);
    }

    public async Task<CustomResponse<NoContent>> UpdateAsync(CompanyUpdateDTO companyUpdateDTO)
    {
        var company = await _unitOfWork.CompanyRepository.AnyAsync(x=>x.Id == companyUpdateDTO.Id);
        if (!company)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(company));
        }
        var user = await _userManager.FindByIdAsync(_httpContextData.UserId);
        if (user == null)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(AspNetUser));
        }
        var result = _mapper.Map<Company>(companyUpdateDTO);
        await _unitOfWork.CompanyRepository.UpdateAsync(result);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
}