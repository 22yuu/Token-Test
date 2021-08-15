using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using System.IO;

namespace testocr2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        async Task OCRProcess()
        {
            Language language = new Language("ko");

            FileStream stream = File.OpenRead(@"test2.jpg");

            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream.AsRandomAccessStream());
            SoftwareBitmap bitmap = await decoder.GetSoftwareBitmapAsync();

            OcrEngine engine = OcrEngine.TryCreateFromLanguage(language);
            OcrResult result = await engine.RecognizeAsync(bitmap).AsTask();

            Clipboard.SetText(result.Text);
        }

        private void OCRProcess(object sender, EventArgs e)
        {
            OCRProcess();
        }
    }
}
