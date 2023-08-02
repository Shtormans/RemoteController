using DesktopUI.Abstractions;
using DesktopUI.Controllers;
using DesktopUI.Enums;
using DesktopUI.Managers;
using DesktopUI.Models;
using RemoteController.Domain.Enums;
using WindowsInput;

namespace DesktopUI.Views;

internal class SenderUserView : BaseView
{
    private PictureBox pictureBox1;

    public SenderUserView(ViewBag viewBag) 
        : base(viewBag)
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Task.Run(() =>
            UIManager.Instance.ChangeUISettings(new BrowserSettings
            {
                Title = "Sender",
                SizeMode = SizeMode.FullSize
            }));

        var label1 = new System.Windows.Forms.Label();
        var button1 = new System.Windows.Forms.Button();
        pictureBox1 = new PictureBox();
        //
        // label1
        //
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(3, 200);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(38, 15);
        label1.TabIndex = 0;
        label1.Text = "Sender";
        //
        // button1
        //
        button1.Location = new System.Drawing.Point(341, 385);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(130, 53);
        button1.TabIndex = 1;
        button1.Text = "Back";
        button1.UseVisualStyleBackColor = true;
        button1.Click += Button1_Click;
        // 
        // pictureBox1
        // 
        pictureBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
        pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        pictureBox1.Location = new System.Drawing.Point(250, 85);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new System.Drawing.Size(1800, 700);
        pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
        pictureBox1.TabIndex = 1;
        pictureBox1.TabStop = false;
        pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(pictureBox1_MouseMove!);
        pictureBox1.

        Controls.Add(label1);
        Controls.Add(button1);
        Controls.Add(pictureBox1);

        this.Focus();
        this.KeyDown += SenderUserView_KeyDown;
        this.KeyUp += SenderUserView_KeyUp;
        pictureBox1.MouseDown += SenderUserView_MouseDown;
        pictureBox1.MouseUp += PictureBox1_MouseUp; ;
    }

    private void PictureBox1_MouseUp(object? sender, MouseEventArgs e)
    {
        var keys = ViewBag.Keys as Dictionary<KeyboardKeys, KeyStates>;
    }

    private void SenderUserView_MouseDown(object? sender, MouseEventArgs e)
    {
        var keys = ViewBag.Keys as Dictionary<KeyboardKeys, KeyStates>;
    }

    private void SenderUserView_KeyUp(object? sender, KeyEventArgs e)
    {
        var keys = ViewBag.Keys as Dictionary<KeyboardKeys, KeyStates>;
    }

    private void SenderUserView_KeyDown(object? sender, KeyEventArgs e)
    {
        var keys = ViewBag.Keys as Dictionary<KeyboardKeys, KeyStates>;
    }

    private void Button1_Click(object? sender, EventArgs e)
    {
        Task.Run(() => UIManager.Instance.ShowView(nameof(HomeController)));
    }

    private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
    {
        Point cursorRelativelyToImage = this.PointToClient(Cursor.Position);
        cursorRelativelyToImage.Offset(-pictureBox1.Location.X, -pictureBox1.Location.Y);
        float ratio = Math.Min((float)pictureBox1.Width / (float)pictureBox1.Image.Width, (float)pictureBox1.Height / (float)pictureBox1.Image.Height);
        int imageCurrentWidth = (int)(pictureBox1.Image.Width * ratio);
        int imageCurrentHeight = (int)(pictureBox1.Image.Height * ratio);
        cursorRelativelyToImage.Offset(-(pictureBox1.Width - imageCurrentWidth) / 2, -(pictureBox1.Height - imageCurrentHeight) / 2);
        Point result = new Point(pictureBox1.Image.Width * cursorRelativelyToImage.X / imageCurrentWidth, pictureBox1.Image.Height * cursorRelativelyToImage.Y / imageCurrentHeight);

        ViewBag.MousePosition = result;
    }
}
