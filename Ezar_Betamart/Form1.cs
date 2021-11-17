using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ezar_Betamart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    daftarbarangBindingSource.RemoveCurrent();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.daftarbarang' table. You can move, or remove it, as needed.
            this.daftarbarangTableAdapter.Fill(this.appData.daftarbarang);
            daftarbarangBindingSource.DataSource = this.appData.daftarbarang;

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                txtnama.Focus();
                //Add new row
                this.appData.daftarbarang.AdddaftarbarangRow(this.appData.daftarbarang.NewdaftarbarangRow());
                daftarbarangBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                daftarbarangBindingSource.ResetBindings(false);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            daftarbarangBindingSource.ResetBindings(false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtnama.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    //Fill data to datatable
                    this.daftarbarangTableAdapter.Fill(this.appData.daftarbarang
                        );
                    daftarbarangBindingSource.DataSource = this.appData.daftarbarang;
                    //dataGridView.DataSource = employeesBindingSource;
                }
                else
                {
                    //using linq to query data
                    var query = from o in this.appData.daftarbarang
                                where o.namabarang.Contains(txtSearch.Text) || o.kodebarang == txtSearch.Text
                                select o;
                    daftarbarangBindingSource.DataSource = query.ToList();
                    //dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                daftarbarangBindingSource.EndEdit();
                daftarbarangTableAdapter.Update(this.appData.daftarbarang);
                panel.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                daftarbarangBindingSource.ResetBindings(false);
            }
        }
    }
}
