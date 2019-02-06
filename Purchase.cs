using Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillingWorkstation
{
    public partial class Purchase : Form
    {
        public Purchase()
        {
            InitializeComponent();
        }

        public int srno()
        {
            int j, i = 0;
            i = dgvPurchase.Rows.Count;
            if (i == 0)
            {
                j = 0;
            }
            else
            {
                j = i;
            }
            return j;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure do you want to close this form", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        public void Clear()
        {
            txtCGST.Text = "0";
            txtSGST.Text = "0";
            txtIGST.Text = "0";
            txtHsnC.Text = "";
            txtProName.Text = "";
            txtProQty.Text = "";
            txtRate.Text = "";
            txtAmountPaid.Text = "";
            //cbCutomerId.Text = "-Select-";
            cbProductId.Text = "-Select-";
            dtpSOD.Text = "";
            lblAqty.Text = "0";
            lblProCost.Text = "0";
            //lblFcost.Text = "0.0";
            //lblPenA.Text = "0.0";
            //txtPoID.Text = DAL.ID("select max(Poid) from PurchaseMaster", "PUD0000");
        }

        public void ClearAll()
        {
            txtCGST.Text = "0";
            txtSGST.Text = "0";
            txtIGST.Text = "0";
            txtHsnC.Text = "";
            txtProName.Text = "";
            txtProQty.Text = "";
            txtRate.Text = "";
            txtAmountPaid.Text = "";
            cbCutomerId.Text = "-Select-";
            cbProductId.Text = "-Select-";
            dtpSOD.Text = "";
            lblAqty.Text = "0";
            lblProCost.Text = "0";
            lblFcost.Text = "0";
            lblPenA.Text = "0";
            txtPoID.Text = DAL.ID("select max(Poid) from PurchaseMaster", "PUD0000");
        }

        public void Grand_Total()
        {
            int n = dgvPurchase.Rows.Count;
            int j;
            Double total = 0;
            if (n > 0)
            {
                for (j = 0; j < n; j++)
                {
                    double s;
                    Double.TryParse(dgvPurchase.Rows[j].Cells["Product_Cost"].Value.ToString(), out s);
                    total = total + s;
                }

               lblFcost.Text = total.ToString();
            }
            else
            {
               lblFcost.Text = "0";
            }
        }

        public void ProCal()
        {
            double a, b, c;
            int qty,aq;

            int.TryParse(lblAqty.Text,out aq);
            int.TryParse(txtProQty.Text, out qty);

            qty = aq + qty;
            lblAqty.Text = qty.ToString();

            double.TryParse(txtProQty.Text, out a);
            double.TryParse(txtRate.Text, out b);

            double CGST, SGST, IGST,TGST,Total;
            double.TryParse(txtCGST.Text, out CGST);
            //CGST = Convert.ToDouble(txtCGST.Text);
            double.TryParse(txtSGST.Text, out SGST);
            //SGST = Convert.ToDouble(txtSGST.Text);
            double.TryParse(txtIGST.Text, out IGST);
            //IGST = Convert.ToDouble(txtIGST.Text);
            
            //Complete GST
            TGST = CGST + SGST + IGST;

            Total = a * b;
            c = Total * (TGST/100);
            Total = Total + c;
            lblProCost.Text = Total.ToString();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();   
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Purchase Report";
            // storing header part in Excel  
            for (int i = 1; i < dgvPurchase.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dgvPurchase.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dgvPurchase.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dgvPurchase.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dgvPurchase.Rows[i].Cells[j].Value.ToString();
                }
            }
            // save the application 
            
            //workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            //app.Quit();
        }

        private void Purchase_Load(object sender, EventArgs e)
        {
            txtPoID.Show();
            cmbPoid.Hide();
            txtPoID.Text = DAL.ID("select max(Poid) from PurchaseMaster", "PUD0000");
            cbCutomerId.DataSource = DAL.show("select distinct(Customer_Id) from Customer");
            cbCutomerId.DisplayMember = "Customer_Id";
            cbCutomerId.ValueMember = "Customer_Id";

            cbProductId.DataSource = DAL.show("select distinct(Product_Id) from Product");
            cbProductId.DisplayMember = "Product_Id";
            cbProductId.ValueMember = "Product_Id";
        }

        private void cbCutomerId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvPurchase_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbProductId_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProName.Text = DAL.select("select Product_Name from Product where Product_Id = '"+cbProductId.Text+"'");
            lblAqty.Text = DAL.select("select QTY from Product where Product_Id = '" + cbProductId.Text + "'");
            txtHsnC.Text = DAL.select("select HSN_Code from Product where Product_Id = '" + cbProductId.Text + "'");
            txtCGST.Text = DAL.select("select C_GST from Product where Product_Id = '" + cbProductId.Text + "'");
            txtSGST.Text = DAL.select("select S_GST from Product where Product_Id = '" + cbProductId.Text + "'");
            txtIGST.Text = DAL.select("select I_GST from Product where Product_Id = '" + cbProductId.Text + "'");
            txtRate.Text = DAL.select("select Rate from Product where Product_Id = '" + cbProductId.Text + "'");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int qty = Convert.ToInt16(txtProQty.Text);
            double CGST, SGST, IGST, rate, Cost;
            CGST = Convert.ToDouble(txtCGST.Text);
            SGST = Convert.ToDouble(txtSGST.Text);
            IGST = Convert.ToDouble(txtIGST.Text);
            rate = Convert.ToDouble(txtRate.Text);
            //Disrate = Convert.ToDouble(lblDrate.Text);
            //ReRate = Convert.ToDouble(lblReRate.Text);
            Cost = Convert.ToDouble(lblProCost.Text);

            string query = "insert into PurchaseDetail values('" + txtPoID.Text + "','" + this.dtpSOD.Value.ToShortDateString() + "','" + cbCutomerId.Text + "','" + cbProductId.Text + "','" + txtProName.Text + "','" + txtHsnC.Text + "'," + qty + "," + rate + "," + CGST + "," + SGST + "," + IGST + "," + Cost + ")";
            if (txtProName.Text == "" || txtProQty.Text == "" || txtRate.Text == "" || txtHsnC.Text == "" || txtCGST.Text == "" || txtIGST.Text == "" || txtSGST.Text == "" || txtPoID.Text == "")
            {
                MessageBox.Show("Please Enter All Mandatory Fields ", "Error Message");
            }
            else
            {
                if (txtProQty.Text == "0" || txtProQty.Text == "")
                {
                    MessageBox.Show("Enter Valid Quantity");
                    txtProQty.Text = "";
                    txtProQty.Focus();
                }
                if (txtRate.Text == "0" || txtRate.Text == "")
                {
                    MessageBox.Show("Enter Valid Rate");
                    txtRate.Text = "";
                    txtRate.Focus();
                }
                else
                {

                    int i = dgvPurchase.Rows.Add();
                    dgvPurchase.Rows[i].Cells[0].Value = srno().ToString();
                    dgvPurchase.Rows[i].Cells[1].Value = txtPoID.Text;
                    dgvPurchase.Rows[i].Cells[2].Value = dtpSOD.Text;
                    dgvPurchase.Rows[i].Cells[3].Value = cbCutomerId.Text;
                    dgvPurchase.Rows[i].Cells[4].Value = cbProductId.Text;
                    dgvPurchase.Rows[i].Cells[5].Value = txtProName.Text;
                    dgvPurchase.Rows[i].Cells[6].Value = txtHsnC.Text;
                    dgvPurchase.Rows[i].Cells[7].Value = txtProQty.Text;
                    dgvPurchase.Rows[i].Cells[8].Value = txtRate.Text;
                    dgvPurchase.Rows[i].Cells[9].Value = lblProCost.Text;
                    dgvPurchase.Rows[i].Cells[10].Value = txtCGST.Text;
                    dgvPurchase.Rows[i].Cells[11].Value = txtSGST.Text;
                    dgvPurchase.Rows[i].Cells[12].Value = txtIGST.Text;
                    bool b = DAL.insert(query);
                    if (b == true)
                    {
                        bool up = DAL.update("Update Product Set QTY=" + lblAqty.Text + " where Product_Id='" + cbProductId.Text + "'");
                    }
                    Clear();
                }
            }
            Grand_Total();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string a, d, c, f, g,q;

            if (dgvPurchase.Rows.Count > 0)
            {
                foreach (DataGridViewRow r1 in dgvPurchase.Rows)
                {
                    if (r1.Selected == true)
                    {
                        int selectedIndex = dgvPurchase.CurrentRow.Index;
                        a = dgvPurchase.Rows[selectedIndex].Cells[1].Value.ToString();
                        d = dgvPurchase.Rows[selectedIndex].Cells[2].Value.ToString();
                        c = dgvPurchase.Rows[selectedIndex].Cells[3].Value.ToString();
                        f = dgvPurchase.Rows[selectedIndex].Cells[4].Value.ToString();
                        g = dgvPurchase.Rows[selectedIndex].Cells[6].Value.ToString();
                        q = dgvPurchase.Rows[selectedIndex].Cells[7].Value.ToString();
                        
                        int aq,gq;
                        string s = DAL.select("Select QTY from Product where Product_Id='" + f + "'");
                        int.TryParse(s,out aq);
                        int.TryParse(q, out gq);

                        dgvPurchase.Rows.Remove(r1);
                        bool b = DAL.delete("DELETE FROM PurchaseDetail WHERE Poid='" + a + "' and date='" + d + "' and Customer_Id='" + c + "' and Product_Id='" + f + "' and HSN_Code='" + g + "'");
                        lblAqty.Text = (aq - gq).ToString();
                        Thread.Sleep(1000);
                        if (b == true)
                        {
                            MessageBox.Show("Record Removed Successfully", "Success Message");
                            bool up = DAL.update("Update Product Set QTY=" + lblAqty.Text + " where Product_Id='" + cbProductId.Text + "'");
                        }
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Please Select row to Remove....");
                    }
                    Grand_Total();
                }
            }
            else
            {
                MessageBox.Show("Record Is Not Available For Deletion");
            }
        }

        private void txtProQty_TextChanged(object sender, EventArgs e)
        {
            ProCal();
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            ProCal();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPoID.Text==""||dtpSOD.Text==""||cbCutomerId.Text==""||lblFcost.Text==""||lblPenA.Text==""||lblFcost.Text=="0"||txtAmountPaid.Text=="")
            {
                MessageBox.Show("Enter All Mandatory Fields????");
            }
            else
            {
                double FC, PA, PeA;
                double.TryParse(lblFcost.Text, out FC);
                double.TryParse(txtAmountPaid.Text, out PA);
                double.TryParse(lblPenA.Text, out PeA);
                //double.TryParse(lblDisRateF.Text, out DisA);
                //double.TryParse(lblRetailorF.Text, out RetA);

                DialogResult r = MessageBox.Show("Are You sure you want to Insert the Record", "Warnig", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    bool b = DAL.insert("insert into PurchaseMaster values('" + txtPoID.Text + "','" + this.dtpSOD.Value.ToShortDateString() + "','" + cbCutomerId.Text + "',"+FC+","+PA+","+PeA+")");
                    if (b == true)
                    {
                        MessageBox.Show("Record Inserted Successfully!!!!!!!");
                        ClearAll();
                        txtPoID.Text = DAL.ID("select max(Poid) from PurchaseMaster", "PUD0000");
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Are You sure you want to Delete the Record", "Warnig", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r == DialogResult.Yes)
            {
                bool b = DAL.delete("delete from PurchaseMaster where Poid ='" + txtPoID.Text + "'");
                if (b == true)
                {
                    MessageBox.Show("Record Deleted Successfully!!!!!!!");
                    ClearAll();
                    txtPoID.Text = DAL.ID("select max(Poid) from PurchaseMaster", "PUD0000");
                }
            }
        }

        private void chkUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUpdate.Checked == true)
            {
                txtPoID.Hide();
                cmbPoid.Show();
                btnUpdate.Enabled = true;
                cmbPoid.DataSource = DAL.show("select distinct(Poid) from PurchaseMaster");
                cmbPoid.DisplayMember = "Poid";
                cmbPoid.ValueMember = "Poid";
            }
            else
            {
                txtPoID.Show();
                cmbPoid.Hide();
                btnUpdate.Enabled = false;
                dgvPurchase.Refresh();
                txtPoID.Text = DAL.ID("select max(Poid) from PurchaseMaster", "PUD0000");
            }
        }

        private void cmbPoid_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvPurchase.Refresh();
            dgvPurchase.DataSource = DAL.select("Select * from PurchaseDetail where Soid='" + cmbPoid.Text + "'");
            txtPoID.Text = cmbPoid.Text;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtPoID.Text == "" || dtpSOD.Text == "" || cbCutomerId.Text == "" || lblFcost.Text == "" || lblPenA.Text == "" || lblPenA.Text == "0" || lblFcost.Text == "0" || txtAmountPaid.Text == "")
            {
                MessageBox.Show("Enter All Mandatory Fields????");
            }
            else
            {
                double FC, PA, PeA;
                double.TryParse(lblFcost.Text, out FC);
                double.TryParse(txtAmountPaid.Text, out PA);
                double.TryParse(lblPenA.Text, out PeA);
                //double.TryParse(lblDisRateF.Text, out DisA);
                //double.TryParse(lblRetailorF.Text, out RetA);

                DialogResult r = MessageBox.Show("Are You sure you want to Update the Record", "Warnig", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    bool b = DAL.insert("Update PurchaseMaster set date='" + this.dtpSOD.Value.ToShortDateString() + "',Customer_Id='" + cbCutomerId.Text + "',Final_Cost=" + FC + ",Paid_Amt=" + PA + ",Pending_Amt=" + PeA + " where Poid='" + txtPoID.Text + "'");
                    if (b == true)
                    {
                        MessageBox.Show("Record Updated Successfully!!!!!!!");
                        ClearAll();
                        txtPoID.Text = DAL.ID("select max(Poid) from PurchaseMaster", "PUD0000");
                        cmbPoid.Hide();
                        txtPoID.Show();
                        chkUpdate.Checked = false;
                    }
                }
            }
        }
    }
}
