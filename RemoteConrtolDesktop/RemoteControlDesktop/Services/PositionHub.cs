using Microsoft.AspNetCore.SignalR;
using RemoteControlDesktop.Interfaces;
using RemoteControlDesktop.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace RemoteControlDesktop.Services
{
    public class PositionHub(Dispatcher dispatcher, ICoordProcessor processor) : Hub
    {
        public bool IsConnected { get; set; } = false;
        private readonly Dispatcher _dispatcher = dispatcher;
        private readonly ICoordProcessor _processor = processor;
        private readonly CancellationToken token;
        public event Action<string> ConnectionChangedEvent;

        public async Task SendPosition(double x, double y)

        {
            _dispatcher.Invoke(() =>
            {
                _processor.Process(new PositionModel()
                {
                    Status = ClickStatus.None,
                    X = x,
                    Y = y,
                });
            });
        }

        //public async Task<bool> ConnectAsync()
        //{
        //    while (true)
        //    {
        //        try
        //        {
        //            await _connection.StartAsync();
        //            Debug.Assert(_connection.State == HubConnectionState.Connected);
        //            SetConnectionState(true);
        //            return true;
        //        }
        //        catch when (token.IsCancellationRequested)
        //        {
        //            SetConnectionState(false);
        //            return false;
        //        }
        //        catch
        //        {
        //            SetConnectionState(false);
        //            Debug.Assert(_connection.State == HubConnectionState.Disconnected);
        //            await Task.Delay(5000);
        //        }
        //    }

    }

    //public async Task DisconnectAsync()
    //{
    //    await _connection.StopAsync();
    //    SetConnectionState(false);
    //}

    //private void SetConnectionState(bool state, string reason = "")
    //{
    //    IsConnected = state;
    //    ConnectionChangedEvent?.Invoke(reason);
    //}
}
