using AutoMapper;
using dBankAPIMySQL.DTOs;
using dBankAPIMySQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL.Profiles
{
    public class RachunekProfile : Profile
    {
        public RachunekProfile()
        {
            CreateMap<RachunekFromUserDTO, Rachunek>();
            CreateMap<RachunekCreateDTO, Rachunek>();
            CreateMap<Rachunek, RachunekReadDTO>();
        }
    }

}
