using Godot;
using System;
using System.Threading.Tasks;
using Thirdweb;

public partial class ThirdwebManager : Node
{
    public ThirdwebClient Client { get; internal set; }
    public InAppWallet InAppWallet { get; internal set; }

    public static ThirdwebManager Instance { get; private set; }

    public override void _Ready()
    {
        if (Instance != null)
        {
            GD.PrintErr("Multiple instances of ThirdwebManager are not allowed");
            return;
        }

        Instance = this;

        string secretKey =
            "ooaaD-bFM0OAvw0hBfSz6fucp3Nxlc94MfqYKpyQvEwQBenYIuhLJhEYoCjQbswUlTAJLfjqxSoo0k2S_KjLEw";
        Client = ThirdwebClient.Create(secretKey: secretKey);

        GD.Print("Thirdweb initialized");
    }
}
