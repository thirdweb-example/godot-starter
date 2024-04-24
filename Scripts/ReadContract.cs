using Godot;
using System;
using System.Threading.Tasks;
using Thirdweb;

public partial class ReadContract : Node
{
    private async void _on_button_down()
    {
        var contract = await ThirdwebContract.Create(
            client: ThirdwebManager.Instance.Client,
            address: "0xBC4CA0EdA7647A8aB7C2061c2E118A18a936f13D",
            chain: 1,
            abi: "function name() view returns (string)"
        );
        string readResult = await ThirdwebContract.Read<string>(contract, "name");
        GD.Print($"Contract read result: {readResult}");
    }
}
