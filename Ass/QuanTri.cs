using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Ass
{
    public partial class QuanTri : Form
    {


        public QuanTri()
        {
            InitializeComponent();
        }
        Connect conn = new Connect();
        SqlDataReader dr;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter da;
        string luu = "";
        private void QuanTri_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void txtMaMonAn_TextChanged(object sender, EventArgs e)
        {
            if (txtMaMonAn.Enabled && luu == "them")
            {
                conn.Open();
                cmd = new SqlCommand("Select Ma_SP From San_Pham Where  Ma_SP = '" + txtMaMonAn.Text + "'", conn.conn);
                dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    lblCheckMaMon.Text = "Mã Đã Tồn Tại";
                    lblCheckMaMon.ForeColor = Color.DarkRed;
                }
                else if (txtMaMonAn.Text == "" || txtMaMonAn.Text == null || txtMaMonAn.TextLength < 3)
                {
                    lblCheckMaMon.Text = "";
                }
                else
                {
                    lblCheckMaMon.Text = "Có Thể Thêm";
                    lblCheckMaMon.ForeColor = Color.DarkGreen;
                }
                conn.Close();
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtMaMonAn.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtTenMonAn.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                cboLoaiMonAn.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                string dongia = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txtDonGia.Text = String.Format("{0:0.00}",dongia);
                //MessageBox.Show(dongia);
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lblCheckMaMon_TextChanged(object sender, EventArgs e)
        {
            if (lblCheckMaMon.Text == "Có Thể Thêm")
            {
                btnLuuMA.Enabled = true;
            }
            else
            {
                btnLuuMA.Enabled = false;
            }
        }
        private void btnLuuMA_Click(object sender, EventArgs e)
        {
            if (luu == "them")
            {
                if (checkmaloai() == false)
                {
                    DialogResult butonb = MessageBox.Show(@"Loại Món Ăn Chưa Tồn Tại
Bạn Có Muốn Thêm Mơi", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (butonb == DialogResult.Yes)
                    {
                        panel1.Visible = true;
                        txtMaLoai.Text = cboLoaiMonAn.Text;
                    }
                    else
                    {
                        panel1.Visible = false;
                    }
                }
                else
                {
                    if (txtMaMonAn.Text == "" || txtMaMonAn.Text == null || cboLoaiMonAn.Text == "" || cboLoaiMonAn.Text == null)
                    {
                        MessageBox.Show("Cần nhập đủ thông tin");
                    }
                    else
                    {
                        conn.Open();
                        cmd = new SqlCommand("Insert Into San_Pham Values ('" + txtMaMonAn.Text + "',N'" + txtTenMonAn.Text + "','" + cboLoaiMonAn.SelectedValue + "','" + txtDonGia.Text + "')", conn.conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm Thành Công");
                        btnLuuMA.Enabled = txtDonGia.Enabled = txtTenMonAn.Enabled = txtMaMonAn.Enabled = cboLoaiMonAn.Enabled = false;
                        button1.Enabled = txtSeach.Enabled = btnXoaMA.Enabled = btnThemMA.Enabled = btnSuaMA.Enabled = true;
                        conn.Close();
                        load();
                    }
                }
            }
            else if (luu == "Sua")
            {
                if (checkmaloai() == false)
                {
                    DialogResult butonb = MessageBox.Show(@"Loại Món Ăn Chưa Tồn Tại
Bạn Có Muốn Thêm Mơi", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (butonb == DialogResult.Yes)
                    {
                        panel1.Visible = true;
                        txtMaLoai.Text = cboLoaiMonAn.Text;
                    }
                    else
                    {
                        panel1.Visible = false;
                    }
                }
                else
                {
                    if (txtMaMonAn.Text == "" || txtMaMonAn.Text == null || cboLoaiMonAn.Text == "" || cboLoaiMonAn.Text == null)
                    {
                        MessageBox.Show("Cần nhập đủ thông tin");
                    }
                    else
                    {
                        conn.Open();
                        cmd = new SqlCommand("Update San_Pham Set Ten_SP = N'" + txtTenMonAn.Text + "',Ma_Loai_SP = '" + cboLoaiMonAn.SelectedValue + "',Don_Gia = '" + txtDonGia.Text + "' Where Ma_SP = '" + txtMaMonAn.Text + "'", conn.conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Sửa thành Công");
                        conn.Close();
                        load();
                        txtDonGia.Enabled = txtTenMonAn.Enabled = txtMaMonAn.Enabled = cboLoaiMonAn.Enabled = false;
                        btnLuuMA.Enabled = false;
                        button1.Enabled = txtSeach.Enabled = dataGridView1.Enabled = btnXoaMA.Enabled = btnThemMA.Enabled = btnSuaMA.Enabled = true;
                    }
                }
            }
        }
        public bool checkmaloai()
        {
            conn.Open();
            cmd = new SqlCommand("Select Ma_Loai_SP from Loai_San_Pham Where Ma_Loai_SP = '" + cboLoaiMonAn.SelectedValue + "'", conn.conn);
            dr = cmd.ExecuteReader();
            {
                if (dr.HasRows == true)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
        }
        private void cboLoaiMonAn_TextChanged(object sender, EventArgs e)
        {
        }
        private void btnThemMA_Click(object sender, EventArgs e)
        {
            luu = "them";
            txtDonGia.Enabled = cboLoaiMonAn.Enabled = txtMaMonAn.Enabled = txtTenMonAn.Enabled = true;
            button1.Enabled= txtSeach.Enabled = btnSeach.Enabled = btnXoaMA.Enabled = btnThemMA.Enabled = btnSuaMA.Enabled = false;
            btnThoat.Enabled = true;        
        }
        private void btnThemLoai_Click(object sender, EventArgs e)
        {
            if (txtTenLoai.Text != "" || txtTenLoai.Text != null)
            {
                conn.Open();
                cmd = new SqlCommand("Insert Into Loai_San_Pham Values ('" + txtMaLoai.Text + "',N'" + txtTenLoai.Text + "',N'" + txtMoTa.Text + "')", conn.conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm Thành Công");
                conn.Close();
                panel1.Visible = false;
                txtMoTa.Clear();
                txtTenLoai.Clear();
                dataGridView1.SelectionChanged -= new EventHandler(dataGridView1_SelectionChanged);
                load();
                dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            }
            else
            {
                MessageBox.Show("Cần nhập đủ thông tin");
            }
        }
        void load()
        {
            conn.Open();
            cmd = new SqlCommand("Select Ma_SP,Ten_SP,Ten_Loai_SP,Don_Gia From San_Pham join Loai_San_Pham on San_Pham.Ma_Loai_SP = Loai_San_Pham.Ma_Loai_SP", conn.conn);
            ds = new DataSet();
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds, "San_Pham");
            cmd = new SqlCommand("Select * From Loai_San_Pham", conn.conn);
            da.SelectCommand = cmd;
            da.Fill(ds, "Loai_San_Pham");
            cboLoaiMonAn.DataSource = ds.Tables[1];
            cboLoaiMonAn.DisplayMember = "Ten_Loai_SP";
            cboLoaiMonAn.ValueMember = "Ma_Loai_SP";
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
        private void btnSuaMA_Click(object sender, EventArgs e)
        {
            luu = "Sua";
            txtDonGia.Enabled = cboLoaiMonAn.Enabled = txtTenMonAn.Enabled = true;
            button1.Enabled = txtSeach.Enabled = btnSeach.Enabled = dataGridView1.Enabled = txtMaMonAn.Enabled = btnThemMA.Enabled = btnSuaMA.Enabled = btnXoaMA.Enabled = false;
            btnThoat.Enabled = true;
            btnLuuMA.Enabled = true;       
        }
        private void btnXoaMA_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                conn.Open();
                cmd = new SqlCommand("Delete From Chi_Tiet_Hoa_Don Where Ma_SP = N'" + txtMaMonAn.Text + "'", conn.conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("Delete From San_Pham Where Ma_SP = N'" + txtMaMonAn.Text + "'", conn.conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa Thành Công");
                conn.Close();
                load();
            }
        }
        private void QuanTri_Load(object sender, EventArgs e)
        {
            conn.GetConnect();
            load();
        }

        private void btnThemMA_EnabledChanged(object sender, EventArgs e)
        {
            if (txtMaMonAn.Enabled && luu == "them")
            {
                conn.Open();
                cmd = new SqlCommand("Select Ma_SP From San_Pham Where  Ma_SP = '" + txtMaMonAn.Text + "'", conn.conn);
                dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    lblCheckMaMon.Text = "Mã Đã Tồn Tại";
                    lblCheckMaMon.ForeColor = Color.DarkRed;
                }
                else if (txtMaMonAn.Text == "" || txtMaMonAn.Text == null || txtMaMonAn.TextLength < 3)
                {
                    lblCheckMaMon.Text = "";
                }
                else
                {
                    lblCheckMaMon.Text = "Có Thể Thêm";
                    lblCheckMaMon.ForeColor = Color.DarkGreen;
                }
                conn.Close();
            }
        }

        private void btnSeach_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("Select Ma_SP,Ten_SP,Ten_Loai_SP,Don_Gia From San_Pham join Loai_San_Pham on San_Pham.Ma_Loai_SP = Loai_San_Pham.Ma_Loai_SP Where Ma_SP Like N'%" + txtSeach.Text + "%'", conn.conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            if (dt.Rows.Count > 0 && dt != null)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                cmd = new SqlCommand("Select Ma_SP,Ten_SP,Ten_Loai_SP,Don_Gia From San_Pham join Loai_San_Pham on San_Pham.Ma_Loai_SP = Loai_San_Pham.Ma_Loai_SP Where Ten_SP Like N'%" + txtSeach.Text + "%'", conn.conn);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count > 0 && dt != null)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    cmd = new SqlCommand("Select Ma_SP,Ten_SP,Ten_Loai_SP,Don_Gia From San_Pham join Loai_San_Pham on San_Pham.Ma_Loai_SP = Loai_San_Pham.Ma_Loai_SP Where Ten_Loai_SP Like N'%" + txtSeach.Text + "%'", conn.conn);
                    dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        try
                        {
                            cmd = new SqlCommand("Select Ma_SP,Ten_SP,Ten_Loai_SP,Don_Gia From San_Pham join Loai_San_Pham on San_Pham.Ma_Loai_SP = Loai_San_Pham.Ma_Loai_SP Where Don_Gia = '" + txtSeach.Text + "'", conn.conn);
                            dt = new DataTable();
                            dt.Load(cmd.ExecuteReader());
                            if (dt.Rows.Count > 0 && dt != null)
                            {
                                dataGridView1.DataSource = dt;
                            }
                        }
                        catch
                        {
                        }
                        finally
                        {
                            MessageBox.Show("Không tìm thấy");
                        }
                    }    
                }
            }
            conn.Close();

        }

        private void txtSeach_TextChanged(object sender, EventArgs e)
        {
            if (txtSeach.Text == "")
            {
                btnSeach.Enabled = false;
            }
            else
            {
                btnSeach.Enabled = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            load();
            txtSeach.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblCheckMaMon.Text = "";
            txtMaLoai.Text = txtTenLoai.Text = txtMoTa.Text = "";
            btnLuuMA.Enabled = txtDonGia.Enabled = txtTenMonAn.Enabled = txtMaMonAn.Enabled = cboLoaiMonAn.Enabled = false;
            dataGridView1.Enabled= button1.Enabled = txtSeach.Enabled = btnXoaMA.Enabled = btnThemMA.Enabled = btnSuaMA.Enabled = true;
            panel1.Visible = false;
            load();
        }

    }
}
