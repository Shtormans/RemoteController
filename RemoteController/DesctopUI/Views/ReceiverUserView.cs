using DesktopUI.Abstractions;
using DesktopUI.Controllers;
using DesktopUI.Enums;
using DesktopUI.Managers;
using DesktopUI.Models;

namespace DesktopUI.Views;

internal class ReceiverUserView : BaseView
{
    public ReceiverUserView(ViewBag viewBag)
        : base(viewBag)
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Task.Run(() =>
            UIManager.Instance.ChangeUISettings(new BrowserSettings
            {
                Title = "Receiver",
                SizeMode = SizeMode.Windowed
            }));

        var label1 = new System.Windows.Forms.Label();
        var button1 = new System.Windows.Forms.Button();

        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(3, 200);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(38, 15);
        label1.TabIndex = 0;
        label1.Text = "Receiver";

        button1.Location = new System.Drawing.Point(341, 385);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(130, 53);
        button1.TabIndex = 1;
        button1.Text = ViewBag.Test;
        button1.UseVisualStyleBackColor = true;
        button1.Click += Button1_Click;

        Controls.Add(label1);
        Controls.Add(button1);
    }

    private void Button1_Click(object? sender, EventArgs e)
    {
        Task.Run(() => UIManager.Instance.ShowView(nameof(HomeController)));
    }
}
