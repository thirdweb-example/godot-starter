using Godot;
using System;
using System.Threading.Tasks;
using Thirdweb;

public partial class SubmitOTP : Node
{
    private async void _on_button_down()
    {
        var otp = UIManager.Instance.OTPInput.Text;
        try
        {
            await ThirdwebManager.Instance.InAppWallet.LoginWithOtp(otp);
        }
        catch (Exception e)
        {
            UIManager.Instance.Log = "Unable to submit OTP: " + e.Message;
            return;
        }
        UIManager.Instance.Log = "Logged in!";
        UIManager.Instance.LoginPanel.Visible = false;
    }
}
