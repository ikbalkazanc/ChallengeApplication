using AutoMapper;
using Challenge.Core.DTO;
using Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Service
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<ChallengeUser, UserDto>().ReverseMap();
            CreateMap<Expression<Func<ChallengeUser, bool>>, Expression<Func<UserDto, bool>>>().ReverseMap();
        }
    }
}
