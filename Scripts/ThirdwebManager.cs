using System;
using System.Net.Http;
using System.Threading.Tasks;
using Godot;
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

        Client = ThirdwebClient.Create(
            clientId: "0ac469480494fd6332700a7eb649cf01",
            bundleId: "com.thirdweb.godot"
        );

        if (OS.GetName() == "Android")
        {
            OS.RequestPermissions();
        }

        UIManager.Instance.Log = "ThirdwebManager Initialized!";
    }
}
