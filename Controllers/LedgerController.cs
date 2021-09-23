using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dto.Ledger;
using API.Entities.Ledger;
using API.Helper;
using API.Interface;
using API.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nas_Pos.Interface;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LedgerController : ControllerBase
    {
        private readonly IGenericRepository<Ledger> _repo;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderRepo;
        public LedgerController(IGenericRepository<Ledger> repo, IMapper mapper, IOrderService orderRepo)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet("ledger")]
        public async Task<ActionResult<IReadOnlyList<GetLedgerDto>>> GetLedger()
        {
            var spec = new LedgerWithOrderAndTransaction();
            var obj = await _repo.ListAsyncWithSpec(spec);
            var mapObj = _mapper.Map<IReadOnlyList<GetLedgerDto>>(obj);
            return Ok(mapObj);
        }
        [HttpPost("addTransaction/{ledgerId}")]
        public async Task<ActionResult> postLedger(int ledgerId,PostTransactionDto model)
        {
                if(!ModelState.IsValid) return BadRequest();
                try{
                    var Transaction = _mapper.Map<Transaction>(model);
                await _orderRepo.AddTransaction(ledgerId,Transaction);
                return Ok(new ApiErrorResponse(ErrorStatusCode.UpdateSuccess));
                }
                catch{
                    return BadRequest(new ApiErrorResponse(ErrorStatusCode.InvalidRequest));
                }

        }

        
    }
}