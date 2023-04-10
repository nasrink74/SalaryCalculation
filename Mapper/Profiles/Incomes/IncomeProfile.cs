using AutoMapper;
using Common.VieModel;
using Dto.Incoms.Command;
using Dto.Incoms.Query;

namespace Mapper.Profiles.Incomes
{
    public class IncomeProfile:Profile
    {
        public IncomeProfile()
        {
            CreateMap<EditIncomeDto, IncomDto>().ReverseMap();
            CreateMap<EditIncomeDto, GetRecieptDto>().ReverseMap();
            CreateMap<DeleteIncomeDto, GetRecieptDto>().ReverseMap();
            CreateMap<AddIncomeDto, GetRecieptDto>().ReverseMap();
            CreateMap<IncomDto, DeleteIncomeDto>().ReverseMap();
        }
    }
}
