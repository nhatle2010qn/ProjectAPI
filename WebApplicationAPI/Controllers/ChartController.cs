using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAPI.Interfaces;

namespace WebApplicationAPI.Controllers
{
    [Authorize]
    public class ChartController : BaseApiController
    {
        private readonly IChartService _ChartService;
        public ChartController(IChartService ChartService)
        {
            _ChartService = ChartService;
        }
        [HttpGet("{id:int}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetChartByCategory(int id)
        {
            var chart = await _ChartService.GetTopProductByCategory(id);
            return Ok(chart);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetChartByCategory(string sortDate)
        {
            var chart = await _ChartService.GetChartByCategory(sortDate);
            return Ok(chart);
        }
    }
}