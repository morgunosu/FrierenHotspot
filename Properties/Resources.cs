using System.Threading;

namespace FrierenHotspot.Properties;

public static class Resources
{
    public static bool IsRu => Thread.CurrentThread.CurrentUICulture.Name == "ru";

    public static string AppTitle => "VIRTUAL ROUTER";
    public static string LabelSsid => IsRu ? "Название сети (SSID)" : "Network Name (SSID)";
    public static string LabelPassword => IsRu ? "Пароль (мин. 8 символов)" : "Password (min. 8 chars)";
    public static string LabelAdapter => IsRu ? "Ваш источник интернета" : "Your Internet Source";
    
    public static string TipSsid => IsRu ? "Имя сети, которое увидят другие устройства." : "The network name other devices will see.";
    public static string TipPassword => IsRu ? "Минимум 8 символов. Только английские буквы и цифры." : "Minimum 8 characters. English letters and numbers only.";
    public static string TipAdapter => IsRu ? "Адаптер, через который ваш ПК получает интернет." : "The adapter where your PC gets internet.";
    
    public static string BtnStart => IsRu ? "ЗАПУСТИТЬ СЕТЬ" : "START HOTSPOT";
    public static string BtnStop => IsRu ? "ОСТАНОВИТЬ" : "STOP";
    public static string BtnShare => IsRu ? "Инструкция: Как дать доступ" : "Help: How to Share Internet";
    public static string TerminalTitle => IsRu ? "ТЕРМИНАЛ ПРОЦЕССОВ" : "TERMINAL LOG";
    public static string StatusReady => IsRu ? "Система готова. Настройте и нажмите Старт." : "System ready. Configure and click Start.";

    public static string HelpTitle => IsRu ? "ВАЖНО: Доступ к Интернету" : "IMPORTANT: Internet Sharing";
    public static string HelpLine1 => IsRu ? "Windows не умеет автоматически раздавать интернет на новую сеть." : "Windows cannot automatically share internet to a new network.";
    public static string HelpLine2 => IsRu ? "Когда вы нажмете кнопку СТАРТ, откроется окно сетевых подключений." : "When you click START, the network connections window will open.";
    public static string HelpStepTitle => IsRu ? "Вам нужно вручную:" : "You need to manually:";
    public static string HelpStep1 => IsRu ? "1. Найти ваш основной интернет (Wi-Fi или кабель)." : "1. Find your main internet adapter (Wi-Fi or Cable).";
    public static string HelpStep2 => IsRu ? "2. Правая кнопка мыши -> Свойства -> Вкладка 'Доступ'." : "2. Right-click -> Properties -> 'Sharing' tab.";
    public static string HelpStep3 => IsRu ? "3. Поставить галочку 'Разрешить другим пользователям...'." : "3. Check 'Allow other network users to connect...'.";
    public static string HelpStep4 => IsRu ? "4. В списке выбрать созданную сеть (обычно 'Подключение по локальной сети *')." : "4. Select the created network (usually 'Local Area Connection *').";
    public static string HelpCloseBtn => IsRu ? "Понятно, закрыть" : "Got it, close";
}