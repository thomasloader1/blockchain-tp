using BlockchainTest.Compilers;
using BlockchainTest.Models;
using BlockchainTest.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlockchainTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EthereumService _ethereumService;
        public HomeController(ILogger<HomeController> logger,EthereumService ethereumService)
        {
            _logger = logger;
            _ethereumService = ethereumService;
        }
        [HttpGet]
        public async Task<IActionResult> DeployContract()
        {
            // Ruta del archivo de contrato Solidity
            var contractPath = "./Contracts/SimpleStorage.sol";

            // Compilar el contrato
            var (abi, bytecode) = await Solidity.CompileContract(contractPath);

            // Desplegar el contrato en Ethereum
            var contractAddress = await _ethereumService.DeployContractAsync(abi, bytecode);

            ViewBag.ContractAddress = contractAddress;
            return View();
        }

        public async Task<IActionResult> Index()
        {
            //var ethereumData = await _ethereumService.GetBlockAndTransactionDataAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
