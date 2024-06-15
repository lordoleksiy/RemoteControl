using Microsoft.Extensions.Logging;
using System;
using System.Windows.Forms;

namespace RemoteControlDesktop.ViewModel
{
    public class NotifyIconViewModel : IDisposable
    {
        private NotifyIcon notifyIcon;
        private bool disposedValue = false;

        public NotifyIconViewModel(ILogger<NotifyIconViewModel> logger)
        {
            try
            {
                notifyIcon = new NotifyIcon();
                notifyIcon.Icon = new System.Drawing.Icon("./Resources/logo.ico");
                notifyIcon.Text = "Remote Control";
                notifyIcon.Visible = true;
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
            };
        }

        public void AddToQueryMethod(EventHandler action)
        {
            notifyIcon.Click += action;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    notifyIcon.Visible = false;
                    notifyIcon.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~NotifyIconViewModel()
        {
            Dispose(disposing: false);
        }
    }
}
