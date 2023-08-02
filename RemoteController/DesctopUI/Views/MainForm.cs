using DesktopUI.Abstractions;
using DesktopUI.Enums;
using DesktopUI.Models;

namespace DesktopUI.Views;

public partial class MainForm : Form, IBrowser
{
    public MainForm()
    {
        InitializeComponent();
    }

    public async Task ShowView(BaseView view)
    {
        this.Invoke(() =>
        {
            this.Controls.Clear();

            this.Controls.Add(view);
        });
    }

    public async Task ChangeSettings(BrowserSettings settings)
    {
        this.Invoke(() =>
        {
            if (settings.Title is not null) 
            {
                this.Text = settings.Title;
            }

            if (settings.SizeMode is not null)
            {
                if (settings.SizeMode == SizeMode.FullSize)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                }
            }

            if (settings.BackgroundColor is not null)
            {
                this.BackColor = settings.BackgroundColor.Value;
            }

            if (settings.ScreenSize is not null)
            {
                this.Size = settings.ScreenSize.Value;
            }
        });
    }

    public async Task WaitCursor(bool wait)
    {
        this.Invoke(() =>
        {
            this.UseWaitCursor = wait;
        });
    }
}
