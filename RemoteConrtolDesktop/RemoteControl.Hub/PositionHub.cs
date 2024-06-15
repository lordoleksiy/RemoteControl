using Microsoft.AspNetCore.SignalR;
using RemoteControlDesktop.Models;
using RemoteControl.Models;

namespace RemoteControl.Hub
{
    public class PositionHub : Hub
    {
        public async Task SendPosition(double x, double y, ClickStatus clickStatus)
        
        {
            Console.WriteLine($"{x} {y} {clickStatus}");
            await Clients.All.SendAsync("ReceiveMessage", x, y, clickStatus);
        }
    }
}
