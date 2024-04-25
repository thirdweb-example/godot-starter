using Godot;
using Thirdweb;

public partial class UIManager : Node
{
    public static UIManager Instance { get; private set; }

    public Panel LoginPanel { get; private set; }
    public Panel OTPPanel { get; private set; }
    public Panel ValidatePanel { get; private set; }
    public TextEdit EmailInput { get; private set; }
    public TextEdit OTPInput { get; private set; }
    public Label LogLabel { get; private set; }
    public string Log
    {
        get => LogLabel.Text;
        set
        {
            LogLabel.Text = value;
            GD.Print(value);
        }
    }

    public override void _Ready()
    {
        if (Instance != null)
        {
            GD.PrintErr("Multiple instances of UIManager are not allowed");
            return;
        }

        Instance = this;

        string loginPanelPath = "/root/Control/Panel_Login";
        string otpPanelPath = loginPanelPath + "/Panel_SendOTP";
        string validatePanelPath = loginPanelPath + "/Panel_ValidateOTP";
        string logPanelPath = "/root/Control/Panel_Log";

        LoginPanel = GetNode<Panel>(loginPanelPath);
        LoginPanel.Visible = true;

        OTPPanel = GetNode<Panel>(otpPanelPath);
        OTPPanel.Visible = true;

        ValidatePanel = GetNode<Panel>(validatePanelPath);
        ValidatePanel.Visible = false;

        EmailInput = GetNode<TextEdit>(otpPanelPath + "/Input_Email");
        OTPInput = GetNode<TextEdit>(validatePanelPath + "/Input_OTP");

        LogLabel = GetNode<Label>(logPanelPath + "/Label_Log");
    }
}
