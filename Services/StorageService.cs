using Android.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTDownloaderMAUI.Services
{
    public static class StorageService
    {
        public static (long Available, long Total) GetExternalStorageInfo()
        {
            var context = Android.App.Application.Context;
            var externalFilesDir = context.GetExternalFilesDir(null);

            if (externalFilesDir != null)
            {
                var stat = new StatFs(externalFilesDir.Path);
                long blockSize = stat.BlockSizeLong;
                long totalBlocks = stat.BlockCountLong;
                long availableBlocks = stat.AvailableBlocksLong;

                return (availableBlocks * blockSize, totalBlocks * blockSize);
            }

            return (0, 0);
        }

        public static (long Available, long Total) GetInternalStorageInfo()
        {
            var stat = new StatFs(Android.OS.Environment.DataDirectory.Path);
            long blockSize = stat.BlockSizeLong;
            long totalBlocks = stat.BlockCountLong;
            long availableBlocks = stat.AvailableBlocksLong;

            return (availableBlocks * blockSize, totalBlocks * blockSize);
        }
    }
}
