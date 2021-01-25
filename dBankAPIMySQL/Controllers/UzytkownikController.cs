using AutoMapper;
using dBankAPIMySQL.Data;
using dBankAPIMySQL.DTOs;
using dBankAPIMySQL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace dBankAPIMySQL.Controllers
{
    [Route("api")]
    [ApiController]
    public class UzytkownikController : ControllerBase
    {
        private const string SECRET_KEY = "abcmfikmiqewmfwifm";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(UzytkownikController.SECRET_KEY));
        private readonly IUzytkownikRepo _repository;
        private readonly IMapper _mapper;

        public UzytkownikController(IUzytkownikRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("userdata")]
        [Authorize]
        public ActionResult<IEnumerable<Object>> GetDataByToken()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            var email = GetName(accessToken);
            var data = _repository.GetUserByEmail(email);
            return Ok(data);
        }

        [HttpPost("register")]
        public ActionResult<UzytkownikReadDTO> CreateUser(UzytkownikCreateDTO uzytkownikCreateDTO)
        {
            var userItems = _mapper.Map<Uzytkownik>(uzytkownikCreateDTO);
            _repository.CreateUser(userItems);
            _repository.SaveChanges();
            return Ok(_mapper.Map<UzytkownikReadDTO>(userItems));
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Post(UzytkownikLoginDTO uzytkownikLoginDTO)
        {
            var user = _repository.GetUserByEmailToLogin(uzytkownikLoginDTO.Email);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                if(user.Haslo == uzytkownikLoginDTO.Haslo)
                {
                    return new ObjectResult(GenerateToken(uzytkownikLoginDTO.Email));
                }
                else
                {
                    return BadRequest();
                }
            }
        }
        
        
        private string GenerateToken(string email)
        {
             var token = new JwtSecurityToken(
             claims: new Claim[]
                 {
                    new Claim(ClaimTypes.Name, email)
                 },
              notBefore: new DateTimeOffset(DateTime.Now).DateTime,
              expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
              signingCredentials: new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256)
              );
              return new JwtSecurityTokenHandler().WriteToken(token);
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
