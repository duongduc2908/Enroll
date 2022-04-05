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

namespace Simple_Face_Recognition_App.View
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            LoadImages(new DirectoryInfo(@"C:\Users\haint\Downloads\Simple Face Recognition App\Assets\"));

            Timer t = new Timer();
            t.Interval = 1000 / 25; // 25 FPS
            t.Tick += WhenTimerTicks;
            t.Start();
        }

        List<Bitmap> _images = new List<Bitmap>();
        int _currentImageIndex = 0;

        int CurrentImageIndex
        {
            get { return _currentImageIndex; }
            set
            {
                _currentImageIndex = value;
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(() => { pictureBox1.Image = _images[_currentImageIndex]; }));
                }
                else
                {
                    pictureBox1.Image = _images[_currentImageIndex];
                }
            }
        }

        Bitmap LoadImage(Stream stream)
        {
            return new Bitmap(stream, false);
        }

        public void LoadImages(DirectoryInfo dInfo)
        {
            foreach (FileInfo fInfo in dInfo.GetFiles())
            {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(() => { _images.Add(LoadImage(fInfo.Open(FileMode.Open))); }));
                }
                else
                {
                    _images.Add(LoadImage(fInfo.OpenRead()));
                }
            }
        }

        void WhenTimerTicks(object sender, EventArgs e)
        {
            if (CurrentImageIndex < _images.Count)
                CurrentImageIndex++;
        }
    }
}
