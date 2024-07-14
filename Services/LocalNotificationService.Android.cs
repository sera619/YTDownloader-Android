using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTDownloaderMAUI.Services
{
    internal class LocalNotificationService : ILocalNotificationService
    {

        private readonly Context _context;
        private const string ChannelId = "LocalNotifications";
        private const string ChannelName = "Local Notifications";
        private int _notificationId = 100;
        public LocalNotificationService()
        {
            _context = global::Android.App.Application.Context;
            CreateNotificationChannel();
        }

        public Task ShowNotificationAsync(string title, string message, DateTime? notifyTime = null)
        {
            var builder = new NotificationCompat.Builder(_context, ChannelId)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.logo_transparent)
                .SetPriority(NotificationCompat.PriorityMax)
                .SetDefaults((int)NotificationDefaults.Sound | (int) NotificationDefaults.Vibrate)
                .SetVisibility(NotificationCompat.VisibilityPublic)
                .SetAutoCancel(true);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                builder.SetCategory(Notification.CategoryMessage);
            }
            var notification = builder.Build();

            if (notifyTime.HasValue)
            {
                var intent = new Intent(_context, typeof(AlarmHandler));
                intent.PutExtra("Title", title);
                intent.PutExtra("Message", message);
                intent.PutExtra("NotificationId", _notificationId);
                    intent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);

                var pendingIntent = PendingIntent.GetBroadcast(_context, _notificationId, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

                var triggerTime = NotifyTimeInMilliseconds(notifyTime.Value);
                var alarmManager = _context.GetSystemService(Context.AlarmService) as AlarmManager;
                alarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, triggerTime, pendingIntent);
            }
            else
            {
                var notificationManager = NotificationManagerCompat.From(_context);
                notificationManager.Notify(_notificationId++, notification);
            }

            return Task.CompletedTask;
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                return;
            }

            var channel = new NotificationChannel(ChannelId, ChannelName, NotificationImportance.High)
            {
                LockscreenVisibility = NotificationVisibility.Public
            };
            channel.EnableVibration(true);
            channel.EnableLights(true);
            var notificationManager = _context.GetSystemService(Context.NotificationService) as NotificationManager;
            notificationManager.CreateNotificationChannel(channel);
        }

        private long NotifyTimeInMilliseconds(DateTime notifyTime)
        {
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
            var epochDiff = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
            var utcAlarmTimeInMillis = utcTime.AddSeconds(-epochDiff).Ticks / 10000;
            return utcAlarmTimeInMillis;
        }



        [BroadcastReceiver(Enabled = true, Label = "Local Notifications Broadcast Receiver")]
        public class AlarmHandler : BroadcastReceiver
        {
            public override void OnReceive(Context context, Intent intent)
            {
                var title = intent.GetStringExtra("Title");
                var message = intent.GetStringExtra("Message");
                var notificationId = intent.GetIntExtra("NotificationId", 0);

                var builder = new NotificationCompat.Builder(context, "LocalNotifications")
                    .SetContentTitle(title)
                    .SetContentText(message)
                    .SetSmallIcon(Resource.Drawable.logo_transparent)
                    .SetAutoCancel(true);

                var notificationManager = NotificationManagerCompat.From(context);
                notificationManager.Notify(notificationId, builder.Build());
            }

        }
    }

}
