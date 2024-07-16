using Godot;
using System;
using System.Threading.Tasks;
using Thirdweb;

public partial class ActionGrid : Node
{
    Button _getAddressButton;
    Button _readContractButton;
    Button _signButton;
    Button _disconnectButton;
    Button _createSmartWalletButton;

    public override void _Ready()
    {
        _getAddressButton = GetNode<Button>("Button_GetAddress");
        _getAddressButton.Connect("pressed", new Callable(this, nameof(GetAddress)));

        _readContractButton = GetNode<Button>("Button_ContractRead");
        _readContractButton.Connect("pressed", new Callable(this, nameof(ReadContract)));

        _signButton = GetNode<Button>("Button_PersonalSign");
        _signButton.Connect("pressed", new Callable(this, nameof(Sign)));

        _disconnectButton = GetNode<Button>("Button_Disconnect");
        _disconnectButton.Connect("pressed", new Callable(this, nameof(Disconnect)));

        _createSmartWalletButton = GetNode<Button>("Button_CreateSmartWallet");
        _createSmartWalletButton.Connect("pressed", new Callable(this, nameof(CreateSmartWallet)));
    }

    private async void GetAddress()
    {
        if (!await IsConnected())
        {
            UIManager.Instance.Log = "Not connected!";
            return;
        }

        try
        {
            var address = await ThirdwebManager.Instance.InAppWallet.GetAddress();
            UIManager.Instance.Log = $"Address: {address}";
        }
        catch (Exception e)
        {
            UIManager.Instance.Log = $"Error: {e.Message}";
        }
    }

    private async void ReadContract()
    {
        try
        {
            var contract = await ThirdwebContract.Create(
                client: ThirdwebManager.Instance.Client,
                address: "0xBC4CA0EdA7647A8aB7C2061c2E118A18a936f13D",
                chain: 1
            );
            string readResult = await ThirdwebContract.Read<string>(contract, "name");
            UIManager.Instance.Log = $"Contract name: {readResult}";
        }
        catch (Exception e)
        {
            UIManager.Instance.Log = $"Error: {e.Message}";
        }
    }

    private async void Sign()
    {
        if (!await IsConnected())
        {
            UIManager.Instance.Log = "Not connected!";
            return;
        }

        try
        {
            var sig = await ThirdwebManager.Instance.InAppWallet.PersonalSign("Hello, World!");
            UIManager.Instance.Log = $"Signature: {sig}";
        }
        catch (Exception e)
        {
            UIManager.Instance.Log = $"Error: {e.Message}";
        }
    }

    private async void Disconnect()
    {
        if (!await IsConnected())
        {
            UIManager.Instance.Log = "Not connected!";
            return;
        }

        try
        {
            await ThirdwebManager.Instance.InAppWallet.Disconnect();
            UIManager.Instance.Log = "Disconnected!";
            UIManager.Instance.LoginPanel.Visible = true;
            UIManager.Instance.OTPPanel.Visible = true;
            UIManager.Instance.EmailInput.Text = "";
            UIManager.Instance.ValidatePanel.Visible = false;
            UIManager.Instance.OTPInput.Text = "";
        }
        catch (Exception e)
        {
            UIManager.Instance.Log = $"Error: {e.Message}";
        }
    }

    private async Task<bool> IsConnected()
    {
        return ThirdwebManager.Instance.InAppWallet != null
            && await ThirdwebManager.Instance.InAppWallet.IsConnected();
    }

    private async void CreateSmartWallet()
    {
        if (!await IsConnected())
        {
            UIManager.Instance.Log = "Not connected!";
            return;
        }

        try
        {
            var smartWallet = await SmartWallet.Create(
                personalWallet: ThirdwebManager.Instance.InAppWallet,
                chainId: 421614,
                gasless: true
            );
            UIManager.Instance.Log = $"Smart wallet created: {await smartWallet.GetAddress()}";
        }
        catch (Exception e)
        {
            UIManager.Instance.Log = $"Error: {e.Message}";
        }
    }
}
