using DesktopUI.Abstractions;
using DesktopUI.Controllers;
using DesktopUI.Enums;
using DesktopUI.Managers;
using DesktopUI.Models;
using Domain.Shared;
using RemoteController.Domain.Entities;

namespace DesktopUI.Views;

internal class HomeView : BaseView
{
    private TextBox roomIdField;
    private TextBox passwordField;
    private TextBox myRoomIdField;
    private TextBox myPasswordField;

    public HomeView(ViewBag viewBag)
        : base(viewBag)
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Task.Run(() =>
            UIManager.Instance.ChangeUISettings(new BrowserSettings
            {
                Title = "Home",
                SizeMode = SizeMode.Windowed
            }));

        string imagePath = $"images{Path.DirectorySeparatorChar}";

        passwordField = new TextBox();
        roomIdField = new TextBox();
        myRoomIdField = new TextBox();
        myPasswordField = new TextBox();

        var topRightCorner = new PictureBox();
        var optionsButton = new PictureBox();
        var signalImage = new PictureBox();
        var connectButton = new PictureBox();
        var createButton = new PictureBox();
        var passwordFieldBackground = new PictureBox();
        var roomIdFieldBackground = new PictureBox();
        SuspendLayout();
        // 
        // topRightCorner
        // 
        topRightCorner.Image = Image.FromFile($"{imagePath}top_right_corner_main_window.png");
        topRightCorner.Location = new Point(935, 8);
        topRightCorner.Name = "topRightCorner";
        topRightCorner.Size = new Size(130, 130);
        topRightCorner.SizeMode = PictureBoxSizeMode.Zoom;
        topRightCorner.TabIndex = 1;
        topRightCorner.TabStop = false;
        // 
        // optionsButton
        // 
        optionsButton.BackColor = Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(88)))), ((int)(((byte)(159)))));
        optionsButton.Image = Image.FromFile($"{imagePath}button_options.png");
        optionsButton.Location = new Point(975, 20);
        optionsButton.Name = "optionsButton";
        optionsButton.Size = new Size(78, 78);
        optionsButton.SizeMode = PictureBoxSizeMode.StretchImage;
        optionsButton.TabIndex = 2;
        optionsButton.TabStop = false;
        // 
        // signalImage
        // 
        signalImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(88)))), ((int)(((byte)(159)))));
        signalImage.Image = Image.FromFile($"{imagePath}signal_good.png");
        signalImage.Location = new Point(958, 82);
        signalImage.Name = "signalImage";
        signalImage.Size = new Size(38, 42);
        signalImage.SizeMode = PictureBoxSizeMode.Zoom;
        signalImage.TabIndex = 2;
        signalImage.TabStop = false;
        // 
        // connectButton
        // 
        connectButton.BackColor = System.Drawing.Color.Transparent;
        connectButton.Image = Image.FromFile($"{imagePath}button_connect_main_window.png");
        connectButton.Location = new Point(175, 145);
        connectButton.Name = "connectButton";
        connectButton.Size = new Size(665, 320);
        connectButton.SizeMode = PictureBoxSizeMode.Zoom;
        connectButton.TabIndex = 6;
        connectButton.TabStop = false;
        connectButton.Click += ConnectButton_Click;
        // 
        // createButton
        // 
        createButton.BackColor = Color.Transparent;
        createButton.Image = Image.FromFile($"{imagePath}button_create_main_window.png");
        createButton.Location = new Point(609, 322);
        createButton.Name = "createButton";
        createButton.Size = new Size(323, 195);
        createButton.SizeMode = PictureBoxSizeMode.Zoom;
        createButton.TabIndex = 7;
        createButton.TabStop = false;
        createButton.Click += CreateButton_Click;
        // 
        // passwordFieldBackground
        // 
        passwordFieldBackground.Image = Image.FromFile($"{imagePath}field_password_main_window.png");
        passwordFieldBackground.Location = new Point(816, 275);
        passwordFieldBackground.Name = "passwordFieldBackground";
        passwordFieldBackground.Size = new Size(240, 37);
        passwordFieldBackground.SizeMode = PictureBoxSizeMode.Zoom;
        passwordFieldBackground.TabIndex = 8;
        passwordFieldBackground.TabStop = false;
        // 
        // passwordField
        // 
        passwordField.BackColor = Color.White;
        passwordField.BorderStyle = BorderStyle.None;
        passwordField.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        passwordField.Location = new Point(899, 279);
        passwordField.Name = "passwordField";
        passwordField.Size = new Size(145, 22);
        passwordField.TabIndex = 9;
        // 
        // roomIdFieldBackground
        // 
        roomIdFieldBackground.Image = Image.FromFile($"{imagePath}field_roomID_main_window.png");
        roomIdFieldBackground.Location = new Point(257, 113);
        roomIdFieldBackground.Name = "roomIdFieldBackground";
        roomIdFieldBackground.Size = new Size(339, 56);
        roomIdFieldBackground.SizeMode = PictureBoxSizeMode.Zoom;
        roomIdFieldBackground.TabIndex = 0;
        roomIdFieldBackground.TabStop = false;
        // 
        // roomIdField
        // 
        roomIdField.BackColor = Color.White;
        roomIdField.BorderStyle = BorderStyle.None;
        roomIdField.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        roomIdField.Location = new Point(342, 118);
        roomIdField.Name = "roomIdField";
        roomIdField.Size = new Size(228, 22);
        roomIdField.TabIndex = 10;
        // 
        // myRoomIdField
        // 
        myRoomIdField.BackColor = Color.White;
        myRoomIdField.BorderStyle = BorderStyle.None;
        myRoomIdField.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        myRoomIdField.Location = new Point(0, 0);
        myRoomIdField.Name = "passwordField";
        myRoomIdField.Size = new Size(50, 22);
        myRoomIdField.TabIndex = 9;
        myRoomIdField.Enabled = false;
        myRoomIdField.Text = ViewBag.MyRoomId;
        // 
        // myPasswordField
        // 
        myPasswordField.BackColor = Color.White;
        myPasswordField.BorderStyle = BorderStyle.None;
        myPasswordField.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        myPasswordField.Location = new Point(0, 50);
        myPasswordField.Name = "passwordField";
        myPasswordField.Size = new Size(50, 22);
        myPasswordField.TabIndex = 9;
        myPasswordField.Enabled = false;
        myPasswordField.Text = ViewBag.MyPassword;

        Controls.Add(roomIdField);
        Controls.Add(passwordField);
        Controls.Add(passwordFieldBackground);
        Controls.Add(createButton);
        Controls.Add(connectButton);
        Controls.Add(roomIdFieldBackground);
        Controls.Add(signalImage);
        Controls.Add(optionsButton);
        Controls.Add(topRightCorner);

        roomIdField.Text = ViewBag.RoomId;
    }

    private void ConnectButton_Click(object? sender, EventArgs e)
    {
        Guid id = Guid.ParseExact(roomIdField.Text, "N");
        string password = passwordField.Text;

        Task.Run(async () =>
        {
            Result result = (await UIManager.Instance.UseMethodAsync(nameof(HomeController), "ConnectToRoomAsync", new object[] { id, password }) as Result)!;

            if (result.IsSuccess)
            {
                await UIManager.Instance.ShowView(nameof(SenderUserController));
            }
        });
    }

    private void CreateButton_Click(object? sender, EventArgs e)
    {
        Guid id = Guid.ParseExact(roomIdField.Text, "N");
        string password = passwordField.Text;

        Task.Run(async () =>
        {
            Result<Room> result = (await UIManager.Instance.UseMethodAsync(nameof(HomeController), "CreateRoomAsync", new object[] { id, password }) as Result<Room>)!;

            if (result.IsSuccess)
            {
                await UIManager.Instance.ShowView(nameof(ReceiverUserController), parameters: new object[] { result.Value.IpAddress, result.Value.Id, result.Value.MonitorCount });
            }
        });
    }
}
