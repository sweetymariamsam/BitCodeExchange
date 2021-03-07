using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using BlockChainApp.Models;
using BlockChainApp.Controllers;
using BlockChainApp.Services.Interfaces;

namespace BlockChainAppTests
{
    [TestClass]
    public class HomeControllerTests
    {
        private readonly HomeController _homeController;
        private readonly Mock<ILogger<HomeController>> _log = new Mock<ILogger<HomeController>>();
        private readonly Mock<IBitCoinExchange> _bitCoinExchange = new Mock<IBitCoinExchange>();

        public HomeControllerTests()
        {
            _homeController = new HomeController(_log.Object, _bitCoinExchange.Object);
        }

        [TestMethod, TestCategory("AutomatedTests")]
        public async Task GetTickerInfo_Successful_RequestAsync()
        {
            //Arrange

            _bitCoinExchange.Setup(x => x.GetTickerInfo())
                .ReturnsAsync(BitCoinTestData());
            //Act
            var response = await _homeController.GetTickerInfo();

            //Assert
            Assert.AreEqual(true, response.Any(x => x.Currency == "USD"));
        }


        [TestMethod, TestCategory("AutomatedTests")]
        public async Task GetTickerInfo_Fail_RequestAsync()
        {
            //Arrange

            _bitCoinExchange.Setup(x => x.GetTickerInfo())
                .ReturnsAsync(BitCoinTestData());
            //Act
            var response = await _homeController.GetTickerInfo();

            //Assert
            Assert.AreEqual(false, response.Any(x => x.Currency == "INR"));
        }

        [TestMethod, TestCategory("AutomatedTests")]
        public async Task GetCurrencyConversion_Successful_RequestAsync()
        {
            //Arrange
            decimal result = 23.5m;
            _bitCoinExchange.Setup(x => x.GetCurrencyConversion(It.IsAny<string>(), It.IsAny<decimal>())).ReturnsAsync(result);

            //Act
            var response = await _homeController.GetCurrencyConversion("USD", 30);

            //Assert
            Assert.AreEqual(23.5m, response);
        }

        [TestMethod, TestCategory("AutomatedTests")]
        public async Task GetCurrencyConversion_Fail_RequestAsync()
        {
            //Arrange
            decimal result = 23.5m;
            _bitCoinExchange.Setup(x => x.GetCurrencyConversion(It.IsAny<string>(), It.IsAny<decimal>())).ReturnsAsync(result);

            //Act
            var response = await _homeController.GetCurrencyConversion("INR", 30);

            //Assert
            Assert.AreNotEqual(232.5m, response);
        }

        private IEnumerable<BitCoin> BitCoinTestData()
        {
            return new List<BitCoin>() {
                new BitCoin() {
                    PeriodSetting=47190.69m,
                    Currency="USD",
                    Last=47190.69m,
                    Buy=47190.69m,
                    Sell=47190.69m,
                    Symbol="$"
                },
                 new BitCoin() {
                        PeriodSetting=33889.19m,
                        Currency="GBP",
                        Last=33889.19m,
                        Buy=33889.19m,
                        Sell=33889.19m,
                        Symbol="£"
                }
            };
        }
    }
}
