#if ANDROID
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
#endif


namespace YTDownloaderMAUI.Src
{
    public static class PermissionHelper
    {
        private const int RequestCode = 1;
#if ANDROID
        private static List<string> GetRequiredPermissions()
        {
            var permissions = new List<string>();
            int sdkInt = (int)Android.OS.Build.VERSION.SdkInt;
            // API 29 or higher
            if (sdkInt >= (int)BuildVersionCodes.Q)
            {
                permissions.Add(Android.Manifest.Permission.AccessMediaLocation);
            }

            // API 31 or higher
            if (sdkInt >= (int)BuildVersionCodes.S)
            {
                permissions.Add(Android.Manifest.Permission.ManageMedia);
            }
            // API 33 or higher

            if (sdkInt >= (int)BuildVersionCodes.Tiramisu)
            {
                permissions.Add(Android.Manifest.Permission.ReadMediaVideo);
                permissions.Add(Android.Manifest.Permission.ReadMediaAudio);
                permissions.Add(Android.Manifest.Permission.ReadMediaImages);
            }
            // older versions
            else
            {
                permissions.Add(Android.Manifest.Permission.ReadExternalStorage);
                permissions.Add(Android.Manifest.Permission.WriteExternalStorage);
            }
            return permissions;
        }

        public static async Task<bool> CheckAndRequestPermissionsAsync()
        {
            var activity = Platform.CurrentActivity;
            if (activity == null)
                return false;

            var permissionsToRequest = new List<string>();
            var requiredPermissions = GetRequiredPermissions();

            foreach (var permission in requiredPermissions)
            {

                if (ContextCompat.CheckSelfPermission(activity, permission) != Permission.Granted)
                {
                    permissionsToRequest.Add(permission);
                }
            }

            if (permissionsToRequest.Count > 0)
            {
                var tcs = new TaskCompletionSource<bool>();
                // register BroadcastReciever to handle event
                activity.RegisterReceiver(new PermissionReceiver(tcs), new IntentFilter(PermissionReceiver.PermissionRequestAction));

                // request permissions from user
                ActivityCompat.RequestPermissions(activity, permissionsToRequest.ToArray(), RequestCode);

                return await tcs.Task;
            }
            return true;
        }

        public class PermissionReceiver : BroadcastReceiver
        {
            public const string PermissionRequestAction = "com.creativedudesstudios.ytdownloader.PERMISSION_REQUEST_RESULT";
            private readonly TaskCompletionSource<bool> _tcs;

            public PermissionReceiver(TaskCompletionSource<bool> tcs)
            {
                _tcs = tcs;
            }

            public override void OnReceive(Context? context, Intent? intent)
            {
                if (intent != null && context != null)
                {

                    var permissions = intent.GetStringArrayExtra("permissions");
                    var grantResults = intent.GetIntArrayExtra("grantResults");

                    bool allGranted = grantResults != null && grantResults.All(result => result == (int)Permission.Granted);
                    _tcs.SetResult(allGranted);

                    context.UnregisterReceiver(this);
                }
            }
        }
#endif
    }
}