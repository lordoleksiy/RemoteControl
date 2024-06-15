using RemoteControlDesktop.Interfaces;
using RemoteControlDesktop.Models;
using WindowsInput;

namespace RemoteControlDesktop.Services
{
    public class CoordProcessor(InputSimulator simulator) : ICoordProcessor
    {
        private readonly InputSimulator _inputSimulator;
        public void Process(PositionModel model)
        {
            if (model.X != 0 && model.Y != 0)
            {
                _inputSimulator.Mouse.MoveMouseBy((int)(model.X * 1.4), (int)(model.Y * 1.4));
            }
            if (model.Status == ClickStatus.LeftClick)
            {
                _inputSimulator.Mouse.LeftButtonClick();
            }
            if (model.Status == ClickStatus.RightClick)
            {
                _inputSimulator.Mouse.RightButtonClick();
            }
        }
    }
}
