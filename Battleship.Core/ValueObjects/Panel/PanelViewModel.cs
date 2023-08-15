namespace Battleship.Core.ValueObjects.Panel;

public record PanelViewModel
{
    internal PanelViewModel(Panel panel)
    {
        Coordinates = panel.Coordinates;
        StatusValue = panel.StatusValue;
    }

    public PanelStatusValue StatusValue { get; init; }

    public Coordinates Coordinates { get; init; }
}