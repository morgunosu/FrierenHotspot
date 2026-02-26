# 🌐 Frieren.Net — Virtual Wi-Fi Router

A minimalist and sophisticated Windows utility that transforms your PC or laptop into a fully functional Wi-Fi hotspot. Built with **C# (WPF / .NET 8)**, it combines system-level power with a clean, ethereal aesthetic.

![UI Preview](https://via.placeholder.com/800x450/0A0A0C/FF4081?text=Frieren.Net+Interface+Preview) 

## ✨ Features

* **Ultra-Modern Design:** Move away from boring legacy Windows windows. Enjoy a sleek dark theme with neon accents, custom glow effects, and a polished UI.
* **Background Operation:** Stay focused. The app minimizes to the system tray (near the clock), staying out of your way while keeping your connection active.
* **Live Localization (EN/RU):** Switch interface and console languages on the fly without restarting the application.
* **Smart Terminal:** Integrated process logs that show every stage of the network startup in real-time.
* **Fully Asynchronous:** No "Not Responding" freezes. All system commands are executed on background threads for a buttery-smooth experience.

---

## 🚀 How to Install and Use

1.  Go to the [Releases](../../releases) section on the right and download the latest `FrierenHotspot_Setup.exe`.
2.  Install and launch the application.
3.  Choose a unique Network Name (**SSID**) and a secure **Password**.
4.  Click **START HOTSPOT**.
5.  ⚠️ **Important:** Click the **"Instruction"** button inside the app. It will show you how to enable Windows Internet Connection Sharing (ICS) in just 3 clicks.

---

## 🛠 Manual Build (For Developers)

If you want to compile the project yourself, ensure you have the **.NET 8 SDK** installed.

```bash
# Clone the repository
git clone [https://github.com/YOUR_NICKNAME/FrierenHotspot.git](https://github.com/YOUR_NICKNAME/FrierenHotspot.git)

# Navigate to the project folder
cd FrierenHotspot

# Run the app
dotnet run
