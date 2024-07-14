using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTDownloaderMAUI.Models
{
    public class VideoEntry
    {
        public required string Title { get; set; }
        public required string URL { get; set; }
        public required string Duration { get; set; }

    }
}
