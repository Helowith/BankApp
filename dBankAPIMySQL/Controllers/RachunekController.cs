using AutoMapper;
using dBankAPIMySQL.Data;
using dBankAPIMySQL.DTOs;
using dBankAPIMySQL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace dBankAPIMySQL.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Authorize]
    public class RachunekController : Controller
    {
        private readonly IRachunekRepo _repository;
        private readonly IMapper _mapper;

        public RachunekController(IRachunekRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }


        [HttpPost]
        public ActionResult<RachunekReadDTO> CreateAccount(RachunekFromUserDTO rachunekFromUserDTO)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            var email = GetName(accessToken);
            var user = _repository.GetUserByEmail(email);
            var accountToCreate = _mapper.Map<Rachunek>(rachunekFromUserDTO);
            accountToCreate.IdUzytkownika = user.Id;
            accountToCreate.Saldo = 0;
            accountToCreate.NrRachunku = Resources.GenerateAccountNummber.GetAccountNumber();
            _repository.CreateAccount(accountToCreate);
            _repository.SaveChanges();
            return _mapper.Map<RachunekReadDTO>(accountToCreate);
            
        }
        protected string GetName(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
            var email = tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            return email;
        }


    }
}
