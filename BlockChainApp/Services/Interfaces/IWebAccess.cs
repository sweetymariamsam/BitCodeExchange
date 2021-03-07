using System;
using System.Threading.Tasks;

namespace BlockChainApp.Services.Interfaces
{
    public interface IWebAccess
    {
        Task<string> GetAsyncRequest<T>(Uri restUri);
    }
}
