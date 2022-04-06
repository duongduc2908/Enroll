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
using MultiFaceRec.Shared;
using DirectShowLib;
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
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            InitScreen();
            InitPanelBackdrop();

            InitVideo();
            InitPinLogo();
            InitLabelMarquee();
            InitPanelSetting();
            InitComboBox();
        }

        private void InitComboBox()
        {
            comboBoxServer.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxSetting item1 = new ComboBoxSetting();
            item1.Value = "http://10.142.10.197:8080";
            item1.Text = "Hạ Long - " + item1.Value;

            ComboBoxSetting item2 = new ComboBoxSetting();
            item2.Value = "http://10.210.50.92:8080";
            item2.Text = "Phú Quốc - " + item1.Value;

            ComboBoxSetting item3 = new ComboBoxSetting();
            item3.Value = "http://10.204.60.45:8080";
            item3.Text = "Nha Trang - " + item1.Value;

            ComboBoxSetting item4 = new ComboBoxSetting();
            item4.Value = "http://10.212.190.95:8080";
            item4.Text = "Nam Hội An - " + item1.Value;

            var listServer = new List<ComboBoxSetting>();
            listServer.Add(item1);
            listServer.Add(item2);
            listServer.Add(item3);
            listServer.Add(item4);
            string savedPrimaryServer = Properties.Settings.Default.primary_server.ToString();
            for (int i = 0; i < listServer.Count; i++)
            {
                comboBoxServer.Items.Add(listServer[i]);
                if (listServer[i].Value.ToString() == savedPrimaryServer)
                {
                    comboBoxServer.SelectedIndex = i;
                }
            }

            


            var devices = new List<DsDevice>(DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice));
            foreach (var device in devices)
            { 
                ComboBoxSetting item = new ComboBoxSetting();
                item.Text = device.Name;
                item.Value = device.ClassID;
                comboBoxCamera.Items.Add(item);

                string savedVideoInput = Properties.Settings.Default.video_input.ToString();
                if (savedVideoInput == device.ClassID.ToString())
                {
                    comboBoxCamera.SelectedIndex = comboBoxCamera.FindStringExact(device.Name);
                }
            }
        }

        private void InitPanelSetting()
        {
            panelSetting.Visible = false;
            panelSetting.Location = new Point(
               (Width - panelSetting.Size.Width) / 2,
               (Height - panelSetting.Size.Height) / 2);
        }

        private void InitLabelMarquee()
        {
            string space = "                  ";
            string txt = "Welcome to Vinpearl"+ space+"欢 迎 来 到 Vinpearl" + space + "Chào mừng bạn đến với Vinpearl" + space + "добро пожаловать в Vinpearl" + space + "ようこそ Vinpearlへ";
            labelMarquee.Text = txt.ToUpper();
            labelMarquee.Top = 10;
            timer1.Enabled = true;
            timer1.Interval = 20;
        }

        private void InitPinLogo()
        {
            pictureBox1.ImageLocation = getPathSrc("full_logo.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox2.ImageLocation = getPathSrc("close.png");
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox3.ImageLocation = getPathSrc("logo-white.png");
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox4.ImageLocation = getPathSrc("close.png");
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
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
            adVideoPlayer.URL = getPathSrc("vinpearl-ad.mp4");
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
            if(panelSetting.Visible)
            {
                toggleSettingModelOpen();
            }
            else if(panel1.Visible)
            {
                togglePinModelVisible();
            }
            else
            {
                togglePinModelVisible();

            }
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(labelMarquee.Left < 0 && Math.Abs(labelMarquee.Left) > labelMarquee.Width )
            {
                labelMarquee.Left = this.Width;
            }
            labelMarquee.Left -= 2;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            toggleSettingModelOpen();
            if (panel1.Visible)
            {
                togglePinModelVisible();
            }
        }

        private void toggleSettingModelOpen()
        {
           
            panelSetting.Visible = !panelSetting.Visible;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            toggleSettingModelOpen();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string cameraId = (comboBoxCamera.SelectedItem as ComboBoxSetting).Value.ToString();
            Properties.Settings.Default["video_input"] = cameraId;

            string serverUrl = (comboBoxServer.SelectedItem as ComboBoxSetting).Value.ToString();
            Properties.Settings.Default["primary_server"] = serverUrl;

            Properties.Settings.Default.Save();
        }
    }
}
