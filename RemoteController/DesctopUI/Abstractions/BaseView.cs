using DesktopUI.Models;

namespace DesktopUI.Abstractions;

public abstract class BaseView : GroupBox
{
	protected readonly dynamic ViewBag;

    public BaseView(ViewBag viewBag)
    {
        ViewBag = viewBag;
        
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Dock = DockStyle.Fill;
    }
}
