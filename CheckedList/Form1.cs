using FirebirdSql.Data.FirebirdClient;//Firebird Liblary
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;//Configuration Liblary
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckedList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        FbConnection fbconn = new FbConnection(ConfigurationManager.ConnectionStrings["ConnectionName"].ConnectionString);// Get Connection From AppConfig
        public static string value = "";
        public static string display = "";
        void getCheckListItem()
        {
            //YOU MUST CONFIGURE YORU TABLE NAME 
            fbconn.Close();
            string sql = "SELECT ID,TEXT FROM TABLE";
            DataTable dt = new DataTable();
            FbDataAdapter adp = new FbDataAdapter(sql, fbconn);
            
            fbconn.Open();
            adp.Fill(dt);
            fbconn.Close();
            checkedListBox1.DataSource = dt;
            checkedListBox1.DisplayMember = "TEXT";
            checkedListBox1.ValueMember = "ID";

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            getCheckListItem();
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            value = " ";
            display = " ";
            int i =0;
            DataRow r;
            for ( i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                

                r = ((DataRowView)this.checkedListBox1.CheckedItems[i]).Row;
                display += (r[this.checkedListBox1.DisplayMember]).ToString();
                value += (r[this.checkedListBox1.ValueMember]).ToString();
                r = null;
               
                
                //This if block is add ',' all item and last item don't add
                if (i != checkedListBox1.CheckedItems.Count - 1)
                {
                    value += ",";
                    display += "," + "\n";
                }
                else
                    value += "";
                    display += "";

            }
            label2.Text = value.ToString();
            label3.Text = display.ToString();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            while (checkedListBox1.CheckedIndices.Count > 0)
                checkedListBox1.SetItemChecked(checkedListBox1.CheckedIndices[0], false);
        }
    }
}
