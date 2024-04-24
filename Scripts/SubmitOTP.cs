using Godot;
using System;
using System.Threading.Tasks;
using Thirdweb;

public partial class SubmitOTP : Node
{
    private async void _on_button_down()
    {
        TextEdit otpInput = GetNode<TextEdit>("../InputOTP");
        var otp = otpInput.Text;
        await ThirdwebManager.Instance.InAppWallet.SubmitOTP(otp);
        GD.Print("Logged in!");
    }
}
