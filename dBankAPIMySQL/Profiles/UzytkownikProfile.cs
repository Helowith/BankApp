using AutoMapper;
using dBankAPIMySQL.DTOs;
using dBankAPIMySQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL.Profiles
{
    public class UzytkownikProfile : Profile
    {
        public UzytkownikProfile()
        {
            CreateMap<UzytkownikCreateDTO, Uzytkownik>();
            CreateMap<UzytkownikLoginDTO, Uzytkownik>();
            CreateMap<Uzytkownik, UzytkownikReadDTO>();
        }
    }
}
