using Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VAL;

namespace GMLBillingSystem
{
    public partial class BrandM : Form
    {
        public BrandM()
        {
            InitializeComponent();
        }

        private void BrandM_Load(object sender, EventArgs e)
        {
            txtBid.Text = DAL.ID("select max(Bid) from BrandM", "BRD0000");
            dataGridView1.DataSource = DAL.show("select * from BrandM");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure do you want to close this form", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtBname.Text = "";
            txtBid.Text = DAL.ID("select max(Bid) from BrandM", "BRD0000");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBname.Text == "")
            {
                MessageBox.Show("Enter All Mandatory Fields????");
            }
            else
            {
                DialogResult r = MessageBox.Show("Are You sure you want to Save the Record", "Warnig", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    bool b = DAL.insert("insert into BrandM values('" + txtBid.Text + "','" + txtBname.Text + "')");
                    if (b == true)
                    {
                        MessageBox.Show("Record Inserted Successfully!!!!!!!");
                        txtBname.Text = "";
                        txtBid.Text = DAL.ID("select max(Bid) from BrandM", "BRD0000");
                        dataGridView1.DataSource = DAL.show("select * from BrandM");
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtBname.Text == "")
            {
                MessageBox.Show("Enter All Mandatory Fields????");
            }
            else
            {
                DialogResult r = MessageBox.Show("Are You sure you want to Update the Record", "Warnig", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    bool b = DAL.update("update BrandM set Bname='" + txtBname.Text + "' where Bid='" + txtBid.Text + "'");
                    if (b == true)
                    {
                        MessageBox.Show("Record Updated Successfully!!!!!!!");
                        txtBname.Text = "";
                        txtBid.Text = DAL.ID("select max(Bid) from BrandM", "BRD0000");
                        dataGridView1.DataSource = DAL.show("select * from BrandM");
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Are You sure you want to Delete the Record", "Warnig", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r == DialogResult.Yes)
            {
                bool b = DAL.delete("delete from BrandM where Bid='" + txtBid.Text + "'");
                if (b == true)
                {
                    MessageBox.Show("Record Deleted Successfully!!!!!!!");
                    txtBname.Text = "";
                    txtBid.Text = DAL.ID("select max(Bid) from BrandM", "BRD0000");
                    dataGridView1.DataSource = DAL.show("select * from BrandM");
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = null;
            foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
            {
                cell = selectedCell;
                break;
            }
            if (cell != null)
            {
                DataGridViewRow row = cell.OwningRow;
                txtBid.Text = row.Cells["Bid"].Value.ToString();
                txtBname.Text = row.Cells["Bname"].Value.ToString();
            }
        }

        private void txtBname_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Validation.char_on_keypress(txtBname);            
        }
    }
}
