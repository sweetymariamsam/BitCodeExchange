using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

using BlockChainApp.Models;
using BlockChainApp.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BlockChainApp.Services
{
    public class BitCoinExchange: IBitCoinExchange
    {
        private readonly IWebAccess _webAccess;
        private readonly Uri _hostUri;

        public BitCoinExchange(IWebAccess webAccess, IConfiguration configuration)
        {
            _webAccess = webAccess;
            _hostUri = new Uri(configuration["BlockChainUri"]);
        }

        /// <summary>
        /// Gets the currency conversion
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<decimal> GetCurrencyConversion(string currency, decimal value)
        {
            var restUri = new Uri(_hostUri, $"tobtc?currency={currency}&value={value}");
            var response = await _webAccess.GetAsyncRequest<string>(restUri);
            if (decimal.TryParse(response, out decimal convertedValue))
            {
                return convertedValue;
            }
            return default;

        }

        public async Task<IEnumerable<BitCoin>> GetTickerInfo()
        {
            var restUri = new Uri(_hostUri, "ticker");
            var response = await _webAccess.GetAsyncRequest<string>(restUri);
            var bitCoinInfoDictionary = JsonConvert.DeserializeObject<IDictionary<string, BitCoin>>(response);

            var bitCoinInfoList = bitCoinInfoDictionary.Select(x =>
            {
                var item = x.Value;
                item.Currency = x.Key;
                return item;
            }).OrderBy(x => x.PeriodSetting);
            return bitCoinInfoList;
        }
    }
}
