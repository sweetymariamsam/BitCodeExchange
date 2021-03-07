using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using BlockChainApp.Models;
using BlockChainApp.Services.Interfaces;

namespace BlockChainApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBitCoinExchange _bitCoinExchange;

        public HomeController(ILogger<HomeController> logger, IBitCoinExchange bitCoinExchange)
        {
            _logger = logger;
            _bitCoinExchange = bitCoinExchange;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets the Ticker Information
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BitCoin>> GetTickerInfo()
        {
            try
            {
                return await _bitCoinExchange.GetTickerInfo();
            }
            catch (Exception ex)
            {

                _logger.LogError("Error in GetTickerInfo: ", ex.Message);
                throw;
            }

        }

        /// <summary>
        /// Gets the currency conversion
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<decimal> GetCurrencyConversion(string currency, decimal value)
        {
            try
            {
                return await _bitCoinExchange.GetCurrencyConversion(currency, value);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetCurrencyConversion: ", ex.Message);
                throw;
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
