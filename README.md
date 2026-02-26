# 🌐 Frieren.Net — Virtual Wi-Fi Router

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET_8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Windows](https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white)

A minimalist and sophisticated Windows utility that transforms your PC or laptop into a fully functional Wi-Fi hotspot. Built with **C# (WPF / .NET 8)**, it combines system-level power with a clean, ethereal aesthetic.

![UI Preview](https://morgun.s-ul.eu/2o5gxXap) 

## ✨ Features

* **Ultra-Modern Design:** Move away from boring legacy Windows windows. Enjoy a sleek dark theme with neon accents, custom glow effects, and a polished UI.
* **Background Operation:** Stay focused. The app minimizes to the system tray (near the clock), staying out of your way while keeping your connection active.
* **Live Localization (EN/RU):** Switch interface and console languages on the fly without restarting the application.
* **Smart Terminal:** Integrated process logs that show every stage of the network startup in real-time.
* **Fully Asynchronous:** No "Not Responding" freezes. All system commands are executed on background threads for a buttery-smooth experience.

---

## 🚀 How to Install and Use

1. Go to the [Releases](https://github.com/morgunosu/FrierenHotspot/releases) section and download the latest `FrierenHotspot_Setup.exe`.
2. Install and launch the application.
3. Choose a unique Network Name (**SSID**) and a secure **Password**.
4. Click **START HOTSPOT**.
5. ⚠️ **Important:** Click the **"Help: How to Share Internet"** button inside the app. It will show you how to enable Windows Internet Connection Sharing (ICS) in just 3 steps.

---

## 🛠 Manual Build (For Developers)

If you want to compile the project yourself, ensure you have the **.NET 8 SDK** installed.

```bash
# Clone the repository
git clone [https://github.com/morgunosu/FrierenHotspot.git](https://github.com/morgunosu/FrierenHotspot.git)

# Navigate to the project folder
cd FrierenHotspot

# Run the app
dotnet run

