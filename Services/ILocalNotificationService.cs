using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTDownloaderMAUI.Services
{
    internal interface ILocalNotificationService
    {
        Task ShowNotificationAsync(string title, string message, DateTime? notifyTime = null);
    }
}
