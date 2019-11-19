using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PointcloudWrapper.Widget;
using System.Runtime.InteropServices;

namespace Sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        AxisX axis;
        private void Form1_Load(object sender, EventArgs e)
        {
            GenRandomImg();
        }

        void GenRandomImg()
        {
            float[] x = new float[1280 * 720];
            Random random = new Random();
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = random.Next(0, 255);
            }
            axis = new AxisX(x, 0, 255);
            pictureBox1.Image = axis.Draw();
            Bitmap bmp = new Bitmap(1280, 720);
            var bitmap = bmp.LockBits(
                   new Rectangle(0, 0, 1280, 720),
                   System.Drawing.Imaging.ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            Marshal.Copy(x.Select(s=>(byte)s).ToArray(), 0, bitmap.Scan0, x.Length);
            bmp.UnlockBits(bitmap);
            pictureBox2.Image = bmp;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GenRandomImg();
        }
    }
}
