using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;

namespace exin.FrameWork.Core.Utils
{
    /// <summary>
    /// 生成二维码工具类
    /// </summary>
    public static class QrCodeUtil
    {
        public static void GetQrCode()
        {
            QRCodeGenerator qrGenertor = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenertor.CreateQrCode("内容", QRCodeGenerator.ECCLevel.Q);
            QRCode qrcode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrcode.GetGraphic(20);

            qrCodeImage.Save("E:\\button.jpeg", ImageFormat.Jpeg);
        }
    }
}
