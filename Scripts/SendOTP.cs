using Godot;
using System;
using System.Threading.Tasks;
using Thirdweb;

public partial class SendOTP : Node
{
    private async void _on_button_down()
    {
        TextEdit emailInput = GetNode<TextEdit>("../InputEmail");
        var email = emailInput.Text;
        ThirdwebManager.Instance.InAppWallet = await InAppWallet.Create(
            client: ThirdwebManager.Instance.Client,
            email: email
        );

        if (await ThirdwebManager.Instance.InAppWallet.IsConnected())
        {
            await ThirdwebManager.Instance.InAppWallet.Disconnect();
        }

        if (!await ThirdwebManager.Instance.InAppWallet.IsConnected())
        {
            await ThirdwebManager.Instance.InAppWallet.SendOTP();
            GD.Print("OTP sent");
        }
        else
        {
            GD.Print("Session resumed!");
        }
    }
}
