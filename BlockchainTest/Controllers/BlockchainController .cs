using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Web3;

namespace BlockchainTest.Controllers;

public class BlockchainController : Controller
{
    // Dirección del nodo de Ganache
    private static readonly string ganacheUrl = "http://127.0.0.1:7545";

    // Clave privada de la cuenta de Ganache (reemplaza con una válida)
    private static readonly string privateKey = "";

    // Instancia de Web3
    private readonly Web3 web3;

    public BlockchainController()
    {
        // Configura la conexión con Ganache
        web3 = new Web3(new Nethereum.Web3.Accounts.Account(privateKey), ganacheUrl);
    }

    // Obtiene el balance de una cuenta
    public async Task<ActionResult> GetBalance(string accountAddress)
    {
        var balanceWei = await web3.Eth.GetBalance.SendRequestAsync(accountAddress);
        var balanceEther = Web3.Convert.FromWei(balanceWei);

        ViewBag.Balance = balanceEther;
        return View();
    }

    // Realiza una transacción simulando un depósito o retiro
    public async Task<ActionResult> SendTransaction(string toAddress, decimal amountEther)
    {
        // Convierte el monto de ether a wei
        var amountWei = Web3.Convert.ToWei(amountEther);

        // Crea y envía la transacción
        var transaction = await web3.Eth.GetEtherTransferService()
            .TransferEtherAndWaitForReceiptAsync(toAddress, amountEther);

        ViewBag.TransactionHash = transaction.TransactionHash;
        return View();
    }


}
