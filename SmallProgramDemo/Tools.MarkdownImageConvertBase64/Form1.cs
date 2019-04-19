using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools.MarkdownImageConvertBase64
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "";
            richTextBox1.Text = "";
            pictureBox1.Image = null;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            
            ofd.ShowDialog();
            
            string filename = ofd.FileName;
            textBox1.Text = filename.ToString();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = ImgToBase64String(textBox1.Text);
            pictureBox1.Image = Base64StringToImage(richTextBox1.Text);
        }


        //图片转为base64编码的字符串
        protected string ImgToBase64String(string Imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //threeebase64编码的字符串转为图片
        protected Bitmap Base64StringToImage(string strbase64)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(strbase64);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                //bmp.Save(@"d:\test.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //bmp.Save(@"d:\"test.bmp", ImageFormat.Bmp);
                //bmp.Save(@"d:\"test.gif", ImageFormat.Gif);
                //bmp.Save(@"d:\"test.png", ImageFormat.Png);
                ms.Close();

                return  bmp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string temp = "[id]:data:image/png;base64," + richTextBox1.Text;
            Clipboard.SetDataObject(temp);
            MessageBox.Show("复制Base64编码串到剪切板成功,已经加以MarkDown支持，直接粘贴到MD文件末尾，在需要的的地方使用“![avatar][id]”即可");
        }
    }
}
