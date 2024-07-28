# 📱 YT Downloader - Android

![](Resources/Splash/splash.svg)

YT Downloader is a cross-platform application built with .NET MAUI that allows users to download audio from YouTube videos and playlists. It is focused to work on Android.

**Please Note:**
> **This software is at an early stage of development. The software receives regular updates, currently almost daily. I, S3R43o3, am currently the sole developer, so please be patient. 
> Furthermore, I am far from being a professional with the MAUI framework. 
> For this reason I apologize for any bugs and errors that may occur. 
> Please contact me if you recognize any bugs or errors.
>If you are a developer and would like to help me with this project, please see the Contributing section below.**

## ✨ Features

- Download audio from individual YouTube videos
- Download audio from entire YouTube playlists
- Support for various YouTube URL formats
- Clipboard integration for easy URL input
- Android scoped storage support for devices running Android 10 and above
- A desktop version of this software can be found in the [YT Downloader WinUI 3 Repository](https://github.com/sera619/YoutubeDownloader_WinUI3)

## 🚀 Getting Started

### 📥 Installation

1. **Enable Unknown Sources:**
  
    **Notice** This is necessary because the app is hosted here and not via the Google Play Store. Google therefore does not sign the app itself and it is marked as unsafe and not installed. ([Click](https://www.airdroid.com/app-management/install-unknown-apps-android/) for more information.):
   
   - Before installing the APK, you need to allow installations from unknown sources. To do this, go to your Android device's settings
     - Open **Settings**
     - Go to **Security** or **Privacy** (depending on your device)
     - Enable **Unknown Sources** or **Install unknown apps** and allow the browser or file manager you're using to install APKs.

2. **Download and Install the APK:**
   - Download the APK file from the release section of this repository or from the provided link.
   - Once downloaded, open the APK file to begin the installation process.
   - Follow the on-screen instructions to install the app.

## 🎯 Usage

1. Launch the application
2. Enter a YouTube video or playlist URL in the provided input field
3. Click 'Add' to add the video(s) to the download queue
4. Click 'Download' to start downloading the audio files

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

### 🛠️ Prerequisites

- .NET 7.0 or later
- Visual Studio 2022 or later with MAUI workload installed

### 📚 Dependencies
- [YoutubeExplode](https://github.com/Tyrrrz/YoutubeExplode): Library for interacting with YouTube

## 📄 License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## 🙏 Acknowledgments

- Thanks to all contributors who have helped with this project
- Special thanks to the creators of YoutubeExplode for their excellent library

## ⚠️ Disclaimer

This application is for personal use only. Please respect YouTube's terms of service and copyright laws when using this application.

## 📝 Release Notes

### Version 3.1.9 (Release Version)
- implementing feature to open download directory
- update help information about settings
- implementing storage capacity overview
- fix keyboard still showing after add video to list or paste url in


For older releases click the tab below.

<details>
<summary>Older releases</summary>


### Version 3.1.6 (Release Version)
- update Toolbaritem size
- implement HomeViewModel 
- add start button on homepage
- add a logo animation on HomePage
- add option for toggle HomePage animation
- update SettingsService for new option homepage animation

### Version 3.1.4 (Release Version)
- implementing cancel download feature


### Version 3.1.2 (Release Version)
- Add music icon for entrys
- fixing serveral display bugs


### Version 3.1.1 (Release Version)
- implementing settings
- setup settings option "Check for updates on start"
- add dynamic version and helptext generation
- setup settings option "Check for updates", to manually check for updates
- add Menu Tab "Settings"
- implementing dynamic save from user settings
- fix app icon display errors on popup/header
- fix download statusmessage display
- update YT Popup layout


### Version 3.0.1 (Release Version)
- implement versionchecking and updatehandler

### Version 2.9.9 (Release Version)
- Improved menu navigation
- update of the help page for the use of shortened urls
- various GUI color and display fixes

### Version 2.9.8 (Release Version)
- enable usage of shortend youtube video url´s

### Version 2.9.1 (Release Version)
- Adding a help page to help getting a valid youtube url

### Version 2.8.2 (Hotfix)
- fix url bug where urls without 'www.' in front off are flagged as invalid 

### Version 2.8.1 (Initial Release)
- Basic functionality for downloading audio from YouTube videos and playlists
- Android and Windows support
</details>

## 🗺️ Roadmap

- Implement download progress tracking :ballot_box_with_check:
- Add option to select audio quality
- Setup cancel download :ballot_box_with_check:
- Implement a feature to resume interrupted downloads
- Implement user settings :ballot_box_with_check:


## 🆘 Support

If you encounter any problems or have any questions, please open an issue on the GitHub repository.