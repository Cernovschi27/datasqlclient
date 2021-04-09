using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
namespace Lab1t
{
    public partial class Form1 : Form
    {
        string con = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet dataSet = new DataSet();
        DataSet dataSet2 = new DataSet();
        List<TextBox> textBoxList = new List<TextBox>();

        public Form1()
        {
            SqlConnection conn = new SqlConnection(con);
            InitializeComponent();
            conn.Open();

            String cmd = ConfigurationManager.AppSettings["SelectParent"];
            dataAdapter.SelectCommand = new SqlCommand(cmd, conn);
            dataSet.Clear();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            conn.Close();

            List<String> columnNames = new List<String>(ConfigurationManager.AppSettings["ChildColumnNames"].Split(','));
            for (int i = 0; i < Int32.Parse(ConfigurationManager.AppSettings["ChildNoColumns"]); i++)
            {
                Label label = new Label();
                label.Text = columnNames[i];
                label.Location = new Point(25, i * 70 + 50);

                panel1.Controls.Add(label);


                TextBox textBox = new TextBox();
                textBox.Location = new Point(label.Location.X + 100 + i, label.Location.Y + i);


                panel1.Controls.Add(textBox);

                textBoxList.Add(textBox);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();

            String cmd = ConfigurationManager.AppSettings["SelectParent"];
            dataAdapter.SelectCommand = new SqlCommand(cmd, conn);
            dataSet.Clear();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            int row = dataGridView1.CurrentCell.RowIndex;
            var value = dataGridView1.Rows[row].Cells[0].Value;


            String cmd = ConfigurationManager.AppSettings["SelectChild"];
            dataAdapter.SelectCommand = new SqlCommand(cmd, conn);

            if (Int32.Parse(ConfigurationManager.AppSettings["IsInt"]) == 0)
            {
                dataAdapter.SelectCommand.Parameters.AddWithValue("@value", SqlDbType.VarChar).Value = value.ToString();
            }
            else
            {
                dataAdapter.SelectCommand.Parameters.AddWithValue("@value", SqlDbType.Int).Value = Int32.Parse(value.ToString());
            }


            dataSet2.Clear();
            dataAdapter.Fill(dataSet2);
            dataGridView2.DataSource = dataSet2.Tables[0];
            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            try
            {
                int row = dataGridView2.CurrentCell.RowIndex;
                var id = dataGridView2.Rows[row].Cells[0].Value;

                String cmd = ConfigurationManager.AppSettings["DeleteChild"];
                dataAdapter.InsertCommand = new SqlCommand(cmd, conn);
                dataAdapter.InsertCommand.Parameters.Add("@id", SqlDbType.Int).Value = int.Parse(id.ToString());

                conn.Open();
                dataAdapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Ok");
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                conn.Close();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            try
            {
                int row = dataGridView2.CurrentCell.RowIndex;
                var id = dataGridView2.Rows[row].Cells[0].Value;

                String cmd = ConfigurationManager.AppSettings["UpdateChild"];
                dataAdapter.InsertCommand = new SqlCommand(cmd, conn);


                List<String> parametersNames = new List<String>(ConfigurationManager.AppSettings["ChildColumnParameters"].Split(','));

                for (int i = 0; i < Int32.Parse(ConfigurationManager.AppSettings["ChildNoColumns"]); i++)
                {
                    dataAdapter.InsertCommand.Parameters.Add(parametersNames[i], SqlDbType.VarChar).Value = textBoxList[i].Text;
                }

                if (Int32.Parse(ConfigurationManager.AppSettings["IsInt"]) == 0)
                {
                    dataAdapter.InsertCommand.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = id.ToString();
                }
                else
                {
                    dataAdapter.InsertCommand.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Int32.Parse(id.ToString());
                }

                conn.Open();
                dataAdapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Ok");
                conn.Close();

                foreach (TextBox tb in textBoxList)
                {
                    tb.Clear();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                conn.Close();

                foreach (TextBox tb in textBoxList)
                {
                    tb.Clear();
                }
            }

        }





        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            try
            {
                int row = dataGridView2.CurrentCell.RowIndex;
                var id = dataGridView2.Rows[row].Cells[0].Value;

                String cmd = ConfigurationManager.AppSettings["InsertChild"];
                dataAdapter.InsertCommand = new SqlCommand(cmd, conn);


                List<String> parametersNames = new List<String>(ConfigurationManager.AppSettings["ChildColumnParameters"].Split(','));

                for (int i = 0; i < Int32.Parse(ConfigurationManager.AppSettings["ChildNoColumns"]); i++)
                {

                    dataAdapter.InsertCommand.Parameters.Add(parametersNames[i], SqlDbType.VarChar).Value = textBoxList[i].Text;
                }

                if (Int32.Parse(ConfigurationManager.AppSettings["IsInt"]) == 0)
                {
                    dataAdapter.InsertCommand.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = id.ToString();
                }
                else
                {
                    dataAdapter.InsertCommand.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Int32.Parse(id.ToString());
                }


                conn.Open();
                dataAdapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Ok");
                conn.Close();

                foreach (TextBox tb in textBoxList)
                {
                    tb.Clear();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                conn.Close();

                foreach (TextBox tb in textBoxList)
                {
                    tb.Clear();
                }
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void button2_Click_2(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            try
            {
                int row = dataGridView2.CurrentCell.RowIndex;
                var id = dataGridView2.Rows[row].Cells[0].Value;

                String cmd = ConfigurationManager.AppSettings["DeleteChild"];
                dataAdapter.InsertCommand = new SqlCommand(cmd, conn);
                dataAdapter.InsertCommand.Parameters.Add("@id", SqlDbType.Int).Value = int.Parse(id.ToString());

                conn.Open();
                dataAdapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Ok");
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                conn.Close();
            }
        }
    }
}

