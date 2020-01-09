using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace GlueManagement
{
    public partial class GlueManagementBackup : Form
    {
        public GlueManagementBackup()
        {
            InitializeComponent();
            this.Text += "_" + Application.ProductVersion.ToString();
            //txtGroup.Text = txtWorkLocation.Text = txtModel.Text = txtLot.Text = lblGlueID.Text = "233";
            //picQRcode.Image = BarcodeHelper.QRcode(lblGlueID.Text, picQRcode.Width, picQRcode.Height);
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (txtGroup.Text==""||txtWorkLocation.Text==""||txtModel.Text==""||txtLot.Text=="")
            {
                MessageBox.Show("组别、工位、型号、批号不能为空", "打印标签", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //数据库操作
            StringBuilder sql = new StringBuilder();
            sql.Append(string.Format("Insert Into glue Values (Nextval('id_seq'),'{0}','{1}','{2}','{3}');",
                txtGroup.Text,txtWorkLocation.Text,txtModel.Text,txtLot.Text));
            sql.Append("Select Max(glue_id) From glue;");
            DataTable dt = new DataTable();
            new DBFactory().ExecuteDataTable(sql.ToString(), ref dt);
            if (dt.Rows.Count > 0)
            {
                lblGlueID.Text =  dt.Rows[0]["max"].ToString();
                picQRcode.Image = BarcodeHelper.QRcode(lblGlueID.Text, picQRcode.Width, picQRcode.Height);
            }
            else
            {
                MessageBox.Show("存在错误,操作无效", "数据库", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //确认打印
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void TxtStart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter||txtStartID.Text=="") return;
            if (txtStartLine.Text=="") { MessageBox.Show("线别不能为空","开始使用",MessageBoxButtons.OK,MessageBoxIcon.Information); return; }
            #region 检验
            //1.需要存在与glue表
            string sql = "Select*From glue Where glue_id="+txtStartID.Text;
            DataTable dt = new DataTable();
            DBFactory db = new DBFactory();
            db.ExecuteDataTable(sql, ref dt);
            if (dt.Rows.Count==0)
            {
                MessageBox.Show("该ID号不存在于数据库，不能开始使用", "开始使用",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            //2.需要不存在于glue_use表
            sql = "Select*From glue_use Where glue_id=" + txtStartID.Text;
            dt = new DataTable();
            db = new DBFactory();
            db.ExecuteDataTable(sql, ref dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("该ID号已经存在于使用表，不能开始使用", "开始使用", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            //3.插入glue_use表
            sql = string.Format("Insert Into glue_use Values ({0},'{1}','{2}',Now(),'{3}')",txtStartID.Text,Login.User, "deliver",txtStartLine.Text);
            int changeInt=db.ExecuteSQL( sql);
            MessageBox.Show(string.Format("增加{0}条记录",changeInt));
        }

        private void TxtEnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter||txtEndID.Text=="") return;
            #region 检验
            //1.需要存在与glue表
            string sql = "Select*From glue Where glue_id=" + txtEndID.Text;
            DataTable dt = new DataTable();
            DBFactory db = new DBFactory();
            db.ExecuteDataTable(sql, ref dt);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("该ID号不存在于数据库，不能结束使用", "结束使用", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //2.1需要存在于glue_use表且action='deliver'
            sql = "Select*From glue_use Where glue_id=" + txtEndID.Text+ " And action='deliver'";
            dt = new DataTable();
            db = new DBFactory();
            db.ExecuteDataTable(sql, ref dt);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("该ID号没被使用，不能结束使用", "结束使用", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //2.2该ID需要在glue_use表里的action='IN'记录为0
            sql = "Select*From glue_use Where glue_id=" + txtEndID.Text + " And action='return'";
            dt = new DataTable();
            db = new DBFactory();
            db.ExecuteDataTable(sql, ref dt);
            if (dt.Rows.Count !=0)
            {
                MessageBox.Show("该ID号已被结束使用，不能再次结束使用", "结束使用", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            //3.插入glue_use表
            sql = string.Format("Insert Into glue_use Values ({0},'{1}','{2}',Now(),'')", txtEndID.Text, Login.User, "return");
            int changeInt = db.ExecuteSQL(sql);
            MessageBox.Show(string.Format("增加{0}条记录", changeInt));
        }

        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //TxtBorderStyle(BorderStyle.None);
            //Bitmap bitmap = new Bitmap(pnlPrint.Width, pnlPrint.Height);
            //pnlPrint.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            //bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
            //e.Graphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);


            //Pen pen = new Pen(Color.Black,2);
            //int x = 10; int y = 150; int r = 52;
            ////row
            //e.Graphics.DrawLine(pen, new Point(x, (x + r * 0)), new Point(y, (x + r * 0)));
            //e.Graphics.DrawLine(pen, new Point(x, (x + r * 1)), new Point(y, (x + r * 1)));
            //e.Graphics.DrawLine(pen, new Point(x, (x + r * 2)), new Point(y, (x + r * 2)));
            //e.Graphics.DrawLine(pen, new Point(x, (x + r * 3)), new Point(y, (x + r * 3)));
            //e.Graphics.DrawLine(pen, new Point(x, (x + r * 4)), new Point(y, (x + r * 4)));
            //e.Graphics.DrawLine(pen, new Point(x, (x + r * 5)), new Point(y, (x + r * 5)));
            ////clo
            //e.Graphics.DrawLine(pen, new Point(x, (x + r * 0)), new Point(x, (x + r * 5)));
            //e.Graphics.DrawLine(pen, new Point(y, (x + r * 0)), new Point(y, (x + r * 5)));
            //TxtBorderStyle(BorderStyle.Fixed3D);
            //void TxtBorderStyle(BorderStyle borderStyle)
            //{
            //    txtGroup.BorderStyle = txtWorkLocation.BorderStyle = txtModel.BorderStyle = txtLot.BorderStyle = borderStyle;
            //}



            Font f = new Font("Verdana", 8f);
            Brush b = new SolidBrush(Color.Black);
            int x = 0, y = 210, add_y = 15;
            e.Graphics.DrawString(txtGroup.Text, f, b, new Point(x, (y + add_y * 0)));
            e.Graphics.DrawString(txtWorkLocation.Text, f, b, new Point(x, (y + add_y * 1)));
            e.Graphics.DrawString(txtModel.Text, f, b, new Point(x, (y + add_y * 2)));
            e.Graphics.DrawString(txtLot.Text, f, b, new Point(x, (y + add_y * 3)));
            e.Graphics.DrawString(lblGlueID.Text, f, b, new Point(x, (y + add_y * 4)));
            //picQRcode.Image
            e.Graphics.DrawImage(picQRcode.Image, new Point(x, (y + add_y * 5)));
            //e.Graphics.RotateTransform(45, System.Drawing.Drawing2D.MatrixOrder.Append);
            //e.Graphics.DrawLine(pen, new Point(10,10), new Point(200,10));
        }

        private void 切换用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Login().ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Login.User);
        }

        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpen = false;
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "Query")
                {
                    isOpen = true;
                }
            }
            if (!isOpen)
            {
                new Query().Show();
            }
        }
    }
}
