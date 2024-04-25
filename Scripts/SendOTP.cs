using Godot;
using System;
using System.Threading.Tasks;
using Thirdweb;

public partial class SendOTP : Node
{
    private async void _on_button_down()
    {
        var email = UIManager.Instance.EmailInput.Text;
        try
        {
            ThirdwebManager.Instance.InAppWallet = await InAppWallet.Create(
                client: ThirdwebManager.Instance.Client,
                email: email
            );
        }
        catch (Exception e)
        {
            UIManager.Instance.Log = "Unable to create InAppWallet: " + e.Message;
            return;
        }

        if (!await ThirdwebManager.Instance.InAppWallet.IsConnected())
        {
            try
            {
                await ThirdwebManager.Instance.InAppWallet.SendOTP();
            }
            catch (Exception e)
            {
                UIManager.Instance.Log = "Unable to send OTP: " + e.Message;
                return;
            }
            UIManager.Instance.Log = "OTP sent to user!";
            UIManager.Instance.ValidatePanel.Visible = true;
            UIManager.Instance.OTPPanel.Visible = false;
        }
        else
        {
            UIManager.Instance.Log = "Session resumed!";
            UIManager.Instance.LoginPanel.Visible = false;
        }
    }
}
