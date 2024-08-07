using Godot;
using System;
using System.Threading.Tasks;
using Thirdweb;

public partial class LoginWithApple : Node
{
    private async void _on_button_down()
    {
        try
        {
            ThirdwebManager.Instance.InAppWallet = await InAppWallet.Create(
                client: ThirdwebManager.Instance.Client,
                authProvider: AuthProvider.Apple,
                storageDirectoryPath: OS.GetUserDataDir()
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
                await ThirdwebManager.Instance.InAppWallet.LoginWithOauth(
                    isMobile: OS.GetName() == "Android" || OS.GetName() == "iOS",
                    browserOpenAction: (url) => OS.ShellOpen(url),
                    mobileRedirectScheme: "com.thirdweb.godot://"
                );
            }
            catch (Exception e)
            {
                UIManager.Instance.Log = "Unable to login: " + e.Message;
                return;
            }
            UIManager.Instance.Log = "Logged in!";
            UIManager.Instance.OTPPanel.Visible = false;
            UIManager.Instance.LoginPanel.Visible = false;
        }
        else
        {
            UIManager.Instance.Log = "Session resumed!";
            UIManager.Instance.LoginPanel.Visible = false;
        }
    }
}
