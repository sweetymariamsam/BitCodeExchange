using System.Threading.Tasks;
using System.Collections.Generic;

using BlockChainApp.Models;

namespace BlockChainApp.Services.Interfaces
{
    public interface IBitCoinExchange
    {
        Task<IEnumerable<BitCoin>> GetTickerInfo();
        Task<decimal> GetCurrencyConversion(string currency, decimal value);
    }
}
