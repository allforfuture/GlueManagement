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
    public partial class GlueManagement : Form
    {
        public GlueManagement()
        {
            InitializeComponent();
            this.Text += "_" + Application.ProductVersion.ToString();
        }


        private void 切换用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Login().ShowDialog();
        }
        private void 添加胶水类型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpen = false;
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Text == "GlueType")
                {
                    isOpen = true;
                }
            }
            if (!isOpen)
            {
                new GlueType().Show();
            }
        }

        private void 添加使用地点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpen = false;
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Text == "GlueUsePlace")
                {
                    isOpen = true;
                }
            }
            if (!isOpen)
            {
                new GlueUsePlace().Show();
            }
        }

        private void 装大胶水ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpen = false;
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Text == "BigGlueItem")
                {
                    isOpen = true;
                }
            }
            if (!isOpen)
            {
                new BigGlueItem().Show();
            }
        }

        private void 装小胶水ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpen = false;
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Text == "SmallGlueItem")
                {
                    isOpen = true;
                }
            }
            if (!isOpen)
            {
                new SmallGlueItem().Show();
            }
        }

        private void 移动胶水ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpen = false;
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Text == "GlueRecord")
                {
                    isOpen = true;
                }
            }
            if (!isOpen)
            {
                new GlueRecord().Show();
            }
        }

        private void 打印标签ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isOpen = false;
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Text == "Print")
                {
                    isOpen = true;
                }
            }
            if (!isOpen)
            {
                new Print().Show();
            }
        }
    }
}