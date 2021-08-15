using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static string img_path = Application.StartupPath +  @"\resource\testimg\";
        string[,] data;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] image_files = GetSearchFile(img_path);

            foreach (string fname in image_files)
            {
                OCRProcess(fname);
            }
        }

        async Task OCRProcess(string fileName)
        {
            Language language = new Language("ko");
            FileStream stream = File.OpenRead(fileName);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream.AsRandomAccessStream());
            SoftwareBitmap bitmap = await decoder.GetSoftwareBitmapAsync();
            OcrEngine engine = OcrEngine.TryCreateFromLanguage(language);
            OcrResult result = await engine.RecognizeAsync(bitmap).AsTask();

            
            string FileNameOnly = Path.GetFileNameWithoutExtension(fileName);
            //string category = Regex.Replace(FileNameOnly, @"\d","");
            string save_text_Ppath = Application.StartupPath + @"\resource\testxt\" + FileNameOnly +".txt";
            Console.WriteLine(save_text_Ppath);
            string[] text = { result.Text };
            File.WriteAllLines(save_text_Ppath, text);
        }

        public string[] GetSearchFile(string _strPath)
        {
            string[] files = { "", };
            try
            {
                files = Directory.GetFiles(_strPath, "*.*", SearchOption.AllDirectories);

            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return files;
        }
    }
}
