using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiFaceRec
{
    public partial class StandbyForm : Form
    {
        public float screenWidth = 0;
        public float screenHeight = 0;
        public bool isPinModalOpen = false;
        public string pinCode = "";
        public StandbyForm()
        {

            InitializeComponent();
            InitScreen();
            InitPanelBackdrop();

            InitVideo();
            InitPinLogo();
        }

        private void InitPinLogo()
        {
            pictureBox1.ImageLocation = getPathSrc("full_logo.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox2.ImageLocation = getPathSrc("close.png");
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private string getPathSrc(string file)
        {
            return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")).ToString() + "Resources\\" + file;
        }


        private void InitPanelBackdrop()
        {
            panel1.Visible = isPinModalOpen;
            panel1.Location = new Point(
                (Width - panel1.Size.Width) /2 ,
                (Height - panel1.Size.Height) / 2);

        }

        private void InitScreen()
        {
            Rectangle screen = Screen.FromControl(this).Bounds;
            screenWidth = screen.Width;
            screenHeight = screen.Height;
            Width = screen.Width;
            Height = screen.Height;
        }

        private void InitVideo()
        {
            adVideoPlayer.URL = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")).ToString() + "Resources\\vinpearl-ad.mp4";
            adVideoPlayer.settings.autoStart = true;
            adVideoPlayer.Height = Height;
            adVideoPlayer.Width = Width;
            adVideoPlayer.stretchToFit = true;
            adVideoPlayer.settings.setMode("loop", true);
            adVideoPlayer.settings.mute = true;
            adVideoPlayer.uiMode = "none";
            adVideoPlayer.PlayStateChange += player_PlayStateChange;
            adVideoPlayer.ClickEvent += adVideoPlayer_Click;
            adVideoPlayer.DoubleClickEvent += adVideoPlayer_DbClick;
        }

        private void player_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            adVideoPlayer.Ctlcontrols.currentPosition = 0;
            if (e.newState == 3)//Playing
            {
                //adVideoPlayer.fullScreen = true;
            }
        }
        
        private void togglePinModelVisible()
        {
            isPinModalOpen = !isPinModalOpen;
            panel1.Visible = isPinModalOpen;
        }

        private void adVideoPlayer_Click(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {
            togglePinModelVisible();
        }

        private void adVideoPlayer_DbClick(object sender, AxWMPLib._WMPOCXEvents_DoubleClickEvent e)
        {
            //
        }

        private void handlePinEnter(string pin)
        {

            switch (pin)
            {
                case "Del":
                    if (pinCode.Length == 0) break;
                    pinCode = pinCode.Substring(0, pinCode.Length - 1);
                    break;
                default:
                    pinCode += pin;
                    break;

            }
            string tempPin = "";
            for (int i = 0; i < pinCode.Length; i++)
            {
                tempPin += "*";
            }
            textBoxPinCode.Text = tempPin;

            if (pinCode.Length != 4) return;
            // validate 
            string date = DateTime.Now.ToString("ddMM");
            if (pinCode != date)
            {
                //invalid
                textBoxPinCode.Text = "";
                pinCode = "";
                return;
            }
            // valid and navigate to enroll
            Form enrollForm = new Form1();
            enrollForm.Show();
            this.Close();
        }



       
        private void button1_Click(object sender, EventArgs e)
        {
            handlePinEnter("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            handlePinEnter("2");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            handlePinEnter("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            handlePinEnter("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            handlePinEnter("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            handlePinEnter("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            handlePinEnter("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            handlePinEnter("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            handlePinEnter("9");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            handlePinEnter("#");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            handlePinEnter("0");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            handlePinEnter("Del");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            togglePinModelVisible();
        }
    }
}
