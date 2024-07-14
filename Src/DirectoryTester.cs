using Android.Content;
using Android.OS;
using Android.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTDownloaderMAUI.Src
{
    internal class DirectoryTester
    {
        public async Task TestDirectoriesAsync()
        {
            string[] directories = {
            Android.OS.Environment.DataDirectory.AbsolutePath,
            Android.OS.Environment.DirectoryDownloads,
            Android.OS.Environment.DirectoryDocuments,
            Android.OS.Environment.DirectoryMusic,
            Android.OS.Environment.DirectoryPictures,
            Android.OS.Environment.DirectoryMovies
        };

            foreach (var directory in directories)
            {
                bool canWrite = await CanWriteToDirectory(directory);
                Console.WriteLine($"Can write to {directory}: {canWrite}");
            }
        }

        private async Task<bool> CanWriteToDirectory(string directory)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
            {
                return await CanWriteToDirectoryScopedStorage(directory);
            }
            else
            {
                return await CanWriteToDirectoryLegacy(directory);
            }
        }

        private async Task<bool> CanWriteToDirectoryLegacy(string directoryPath)
        {
            try
            {
                string testFilePath = Path.Combine(directoryPath, "testfile.txt");
                using (var stream = new StreamWriter(testFilePath, false))
                {
                    await stream.WriteAsync("This is a test.");
                }

                File.Delete(testFilePath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to {directoryPath}: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> CanWriteToDirectoryScopedStorage(string directory)
        {
            try
            {
                var contentResolver = Android.App.Application.Context.ContentResolver;
                var values = new ContentValues();
                values.Put(MediaStore.IMediaColumns.DisplayName, "testfile.txt");
                values.Put(MediaStore.IMediaColumns.MimeType, "text/plain");
                values.Put(MediaStore.IMediaColumns.RelativePath, directory);

                var uri = contentResolver.Insert(MediaStore.Files.GetContentUri("external"), values);
                if (uri == null)
                {
                    return false;
                }

                using (var stream = contentResolver.OpenOutputStream(uri))
                using (var writer = new StreamWriter(stream))
                {
                    await writer.WriteAsync("This is a test.");
                }

                // Clean up: delete the test file
                contentResolver.Delete(uri, null, null);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to {directory}: {ex.Message}");
                return false;
            }
        }
    }
}
