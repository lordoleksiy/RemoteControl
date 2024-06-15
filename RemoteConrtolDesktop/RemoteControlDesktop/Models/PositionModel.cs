namespace RemoteControlDesktop.Models
{
    public class PositionModel
    {
        public double X { get; set; }
        public double Y { get; set; }

        public ClickStatus Status { get; set; }
    }

    public enum ClickStatus
    {
        None,
        LeftClick,
        RightClick
    }
}
