# YT Downloader - Android

![](Resources/Splash/splash.svg)

YouTube Downloader MAUI is a cross-platform application built with .NET MAUI that allows users to download audio from YouTube videos and playlists. It is focused to work on Android.

## Features

- Download audio from individual YouTube videos
- Download audio from entire YouTube playlists
- Support for various YouTube URL formats
- Clipboard integration for easy URL input
- Android scoped storage support for devices running Android 10 and above
- A desktop version of this software can be found in the [YT Downloader WinUI 3 Repository](https://github.com/sera619/YoutubeDownloader_WinUI3)

## Getting Started

### Prerequisites

- .NET 7.0 or later
- Visual Studio 2022 or later with MAUI workload installed

### Installation

1. **Enable Unknown Sources:**
   - Before installing the APK, you need to allow installations from unknown sources. To do this, go to your Android device's settings:
     - Open **Settings**
     - Go to **Security** or **Privacy** (depending on your device)
     - Enable **Unknown Sources** or **Install unknown apps** and allow the browser or file manager you're using to install APKs.

2. **Download and Install the APK:**
   - Download the APK file from the release section of this repository or from the provided link.
   - Once downloaded, open the APK file to begin the installation process.
   - Follow the on-screen instructions to install the app.

## Usage

1. Launch the application
2. Enter a YouTube video or playlist URL in the provided input field
3. Click 'Add' to add the video(s) to the download queue
4. Click 'Download All' to start downloading the audio files

## Dependencies

- [YoutubeExplode](https://github.com/Tyrrrz/YoutubeExplode): Library for interacting with YouTube

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

- Thanks to all contributors who have helped with this project
- Special thanks to the creators of YoutubeExplode for their excellent library

## Disclaimer

This application is for personal use only. Please respect YouTube's terms of service and copyright laws when using this application.

## Release Notes

### Version 1.0.0 (Initial Release)
- Basic functionality for downloading audio from YouTube videos and playlists
- Android and Windows support

## Roadmap

- Implement download progress tracking
- Add option to select audio quality
- Setup cancel download and adding
- Implement a feature to resume interrupted downloads

## Support

If you encounter any problems or have any questions, please open an issue on the GitHub repository.
