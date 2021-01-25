using AutoMapper;
using dBankAPIMySQL.DTOs;
using dBankAPIMySQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL.Profiles
{
    public class OperacjaProfile : Profile
    {
        public OperacjaProfile()
        {
            CreateMap<OperacjaMoneyTransferDTO, Operacja>();
            CreateMap<OperacjaCreateDTO, Operacja>();
            CreateMap<Operacja, OperacjaReadDTO>();
        }
    }

}
