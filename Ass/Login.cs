using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

namespace Ass
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        Connect conn = new Connect();
        public SqlDataReader dr;
        public SqlCommand cmd;
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("Select * from Tai_Khoan Where Ma_TaiKhoan = '"+textBox1.Text+"' and MatKhau = '"+textBox2.Text+"'", conn.conn);
            dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    label5.Text = "";
                    this.Hide();
                    QuanTri quantri = new QuanTri();
                    quantri.Show();
                    if (checkBox1.Checked)
                    {
                        FileStream stream = new FileStream(Application.StartupPath + @"\config.dat", FileMode.Create);
                        BinaryWriter binstr = new BinaryWriter(stream);
                        binstr.Write(textBox1.Text);
                        binstr.Write(textBox2.Text);
                        binstr.Write(checkBox1.Checked);
                        binstr.Close();
                        return;
                    }
                    else if (checkBox1.Checked == false)
                    {
                        File.Delete("config.dat");
                    }
                }
                else
                {
                    label5.Text = "Sai Tài Khoản Hoặc Mật Khẩu";
                    textBox2.Clear();
                }
            conn.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            conn.GetConnect();
            BinaryReader binrd = new BinaryReader(new FileStream(Application.StartupPath + @"\config.dat", FileMode.Open));
            string taikhoan = binrd.ReadString();
            string pass = binrd.ReadString();
            bool showonnextst = binrd.ReadBoolean();
            if (showonnextst)
            {
                checkBox1.Checked = true;
                textBox1.Text = taikhoan;
                textBox2.Text = pass;
            }
            binrd.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
