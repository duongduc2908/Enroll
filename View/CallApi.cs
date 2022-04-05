using Simple_Face_Recognition_App.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Face_Recognition_App.View
{
    public partial class CallApi : Form
    {
        public CallApi()
        {
            InitializeComponent();
        }

        private async void btnGetAll_Click(object sender, EventArgs e)
        {
            var responce = await ResHelper.GetALL("users");
            txtResponce.Text = ResHelper.beautifyJson(responce.ToString()); 
        }

        private async void btnPost_Click(object sender, EventArgs e)
        {
            //var responce = await ResHelper.Post(txtName.Text,txtJob.Text);
            //txtResponce.Text = ResHelper.beautifyJson(responce.ToString());
        }

        private async void btnGet_Click(object sender, EventArgs e)
        {
            //var responce = await ResHelper.Get(txtID.Text);
            //txtResponce.Text = ResHelper.beautifyJson(responce.ToString());
        }

        private async void btnPut_Click(object sender, EventArgs e)
        {
            //var responce = await ResHelper.Put(txtID.Text,txtName.Text,txtJob.Text);
            //txtResponce.Text = ResHelper.beautifyJson(responce.ToString());
        }
    }
}
