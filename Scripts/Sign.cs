using Godot;
using System;
using System.Threading.Tasks;
using Thirdweb;

public partial class Sign : Node
{
    private async void _on_button_down()
    {
        var sig = await ThirdwebManager.Instance.InAppWallet.PersonalSign("Hello, World!");
        GD.Print("Signature: " + sig);
    }
}
