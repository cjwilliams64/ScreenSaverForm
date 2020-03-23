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


namespace MyScreenSaver
{
    public partial class frmScreenSaver : Form
    {
        List<Image> BGImages = new List<Image>(); //stores array of pictures
        List<BritPic> BritPics = new List<BritPic>(); //stores array of BritPic objects
        Random random = new Random(); //random number generator

        class BritPic
        {
            //holds position in each picture for our screensaver

            public int PicNum;

            public float X; //float: single precision float number

            public float Y;

            //public float Speed;
        }
        public frmScreenSaver()
        {
            InitializeComponent();
        }

        private void frmScreenSaver_KeyDown(object sender, KeyEventArgs e)
        {
            Close();
        }

        private void frmScreenSaver_Load(object sender, EventArgs e)
        {
            //stores list of file names in my pics folder in the images variable
            string[] images = Directory.GetFiles("pics");

            foreach (string image in images)
            {
                //each string in images will create a new bitmap picture in the BGImages array variable (a global variable)
                BGImages.Add(new Bitmap(image));
            }

            for (int i = 0; i < 50; i++)
            {
                //you can add images until you reach 50 images
                BritPic mp = new BritPic();
                mp.PicNum = i % BGImages.Count;
                mp.X = random.Next(0, Width); //gets random num between 0 and the window's width
                mp.Y = random.Next(0, Height); //gets random num between 0 and the window's height

                //mp.Speed = random.Next(100, 300) / 100.0f;

                BritPics.Add(mp); //mp object will be added to BritPics list

            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void frmScreenSaver_Paint(object sender, PaintEventArgs e)
        {
            //over-riding Window's Paint event...providing our own painting instructions
            foreach (BritPic bp in BritPics)
            {
                e.Graphics.DrawImage(BGImages[bp.PicNum], bp.X, bp.Y); //draws images, positioning them on the X and Y axis
              
                bp.X -= 2;

                if (bp.X < -250) //if the object moves off the left edge of screen
                {
                    //move it back to the right edge
                    bp.X = Width + random.Next(20, 100);
                }
            }
        }
    }

    
}
