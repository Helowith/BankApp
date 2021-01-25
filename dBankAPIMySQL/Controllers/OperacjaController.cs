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
    [Route("api/operation")]
    [ApiController]
    [Authorize]
    public class OperacjaController : Controller
    {
        private readonly IOperacjaRepo _repository;
        private readonly IMapper _mapper;

        public OperacjaController(IOperacjaRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        
        [HttpPost("getall")]
         public ActionResult<IList<OperacjaReadDTO>> GetAllOperations(OperacjaGetAllDTO operacjaGetAllDTO)
        {
            var incomingData = _repository.GetAllOperations(operacjaGetAllDTO.Id);
            var data = _mapper.Map<List<OperacjaReadDTO>>(incomingData);
            return Ok(data);
        }
        [Route("addfounds")]
        [HttpPost]
        public ActionResult<OperacjaReadDTO> AddFounds(OperacjaAddMoneyDTO operacjaAddMoneyDTO)
        {
            var operation = _repository.AddFounds(operacjaAddMoneyDTO);

            return Ok(operation);
        }
        [Route("transfer")]
        [HttpPost]
        public ActionResult<OperacjaReadDTO> MoneyTransfer(OperacjaMoneyTransferDTO operacjaMoneyTransferDTO)
        {
            var operation = _repository.MoneyTransfer(operacjaMoneyTransferDTO);

            return Ok(operation);
        }
        [Route("getfromotherbank")]
        [HttpGet]
        public ActionResult GetNewPaymentsFromOtherBank()
        {
            _repository.GetNewPaymentsToAddToAccounts();
            return Ok();
        }
    }
}
