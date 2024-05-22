using Lagrange.Core.Common.Interface;
using Lagrange.Core.Common;
using Lagrange.Core.Common.Interface.Api;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

Console.WriteLine("Hello, This is Lagrange. Version: Latest stable 0.2.1");
Console.WriteLine("Scan Image.png to log in");
BotDeviceInfo _deviceInfo = BotDeviceInfo.GenerateInfo();
BotKeystore _keyStore = new BotKeystore();
var bot = BotFactory.Create(new BotConfig(), _deviceInfo, _keyStore);
var qrCode = await bot.FetchQrCode();
if (qrCode.HasValue)
{
    var (url, QrCode) = qrCode.Value;
    MemoryStream ms = new MemoryStream(QrCode);
    Image image = System.Drawing.Image.FromStream(ms);
    System.IO.FileInfo info = new System.IO.FileInfo("Image.png");
    System.IO.Directory.CreateDirectory(info.Directory.FullName);
    File.WriteAllBytes("Image.png", QrCode);
}

Console.WriteLine("Log success!");
await bot.LoginByQrCode();
_keyStore = bot.UpdateKeystore();