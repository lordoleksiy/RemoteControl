using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RemoteControlDesktop.Services;
using System;

namespace RemoteControlDesktop.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private readonly PositionHub _posHub;
        private string _roomName = "";
        public string RoomName
        {
            get => _roomName;
            set
            {
                SetProperty(ref _roomName, value);
                OnPropertyChanged(nameof(RoomName));
            }
        }
        public EventHandler HideEvent { get; set; }
        public RelayCommand HideWindowCommand { get; set; }
        public AsyncRelayCommand DisconnectCommand { get; set; }

        public MainViewModel(PositionHub hub)
        {
            _posHub = hub;
            HideWindowCommand = new RelayCommand(() => HideEvent?.Invoke(this, EventArgs.Empty));
        }

        private bool CanCreateRoom()
        {
            if (RoomName.Length < 6 || _posHub.IsConnected)
            {
                return false;
            }
            return true;
        }
    }
}
