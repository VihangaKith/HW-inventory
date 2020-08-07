using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Billing.UI
{
    public partial class Alert : Form
    {
        public Alert(string message, AlertType type)
        {
            InitializeComponent();

            msg.Text = message;

            switch (type)
            { 
                case AlertType.success:
                    this.BackColor = Color.SeaGreen;
                    icon.Image = imageList1.Images[0];
                    break;

                case AlertType.cancel:
                    this.BackColor = Color.Crimson;
                    icon.Image = imageList1.Images[1];
                    break;

                case AlertType.warning:
                    this.BackColor = Color.Gray;
                    icon.Image = imageList1.Images[2];
                    break;
            }
        }

        private void Alert_Load(object sender, EventArgs e)
        {
            this.Top = -1*(this.Height);
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 20;

            show.Start();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            closeAlert.Start();
        }

        private void timerout_Tick(object sender, EventArgs e)
        {
            closeAlert.Start();

            
        }

        int interval = 0;
        private void show_Tick(object sender, EventArgs e)
        {

            if (this.Top < 20)
            {
                this.Top += interval;
                interval += 3;
            }
            else 
            {
                show.Stop();
            }
        }

        private void closeAlert_Tick(object sender, EventArgs e)
        {
            if (Opacity > 0)
            {
                this.Opacity -= 0.1;
            }
            else 
            {
                this.Close();
            }
        }
    }

    public enum AlertType 
    {
        success, cancel, warning
    }
}
