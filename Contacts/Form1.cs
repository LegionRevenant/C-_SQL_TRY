using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Contacts
{
    public partial class Form1 : Form
    {
        Connections con = new Connections();
        SqlConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        // Load Contact by ID
        private void button6_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox4.Text, out int contactId))
            {
                using (conn = con.getConnect())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Contacts WHERE ContactID = @ContactID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ContactID", contactId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox1.Text = reader["Name"].ToString();
                                textBox2.Text = reader["PhoneNumber"].ToString();
                                textBox3.Text = reader["Email"].ToString();
                                textBox4.Text = reader["ContactID"].ToString();
                                MessageBox.Show("Contact loaded successfully.");
                            }
                            else
                            {
                                MessageBox.Show("Contact not found.");
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid ContactID.");
            }
        }

        // Edit Contact
        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox4.Text, out int contactId))
            {
                using (conn = con.getConnect())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Contacts SET Name = @Name, PhoneNumber = @PhoneNumber, Email = @Email WHERE ContactID = @ContactID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                        cmd.Parameters.AddWithValue("@PhoneNumber", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Email", textBox3.Text);
                        cmd.Parameters.AddWithValue("@ContactID", contactId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Contact updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Contact not found.");
                        }
                    }
                }
                ClearTextBoxes();
            }
            else
            {
                MessageBox.Show("Invalid ContactID.");
            }
        }

        // Add Contact
        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox4.Text, out int contactId))
            {
                using (conn = con.getConnect())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Contacts (Name, PhoneNumber, Email, ContactID) VALUES (@Name, @PhoneNumber, @Email, @ContactID)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                        cmd.Parameters.AddWithValue("@PhoneNumber", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Email", textBox3.Text);
                        cmd.Parameters.AddWithValue("@ContactID", contactId);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Contact added successfully.");
                    }
                }
                ClearTextBoxes();
            }
            else
            {
                MessageBox.Show("Invalid ContactID.");
            }
        }

        // Delete Contact
        private void button3_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox4.Text, out int contactId))
            {
                using (conn = con.getConnect())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Contacts WHERE ContactID = @ContactID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ContactID", contactId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Contact deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Contact not found.");
                        }
                    }
                }
                ClearTextBoxes();
            }
            else
            {
                MessageBox.Show("Invalid ContactID.");
            }
        }

        // View All Contacts
        private void button5_Click(object sender, EventArgs e)
        {
            using (conn = con.getConnect())
            {
                conn.Open();
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Contacts", conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        // Search Contact
        private void button4_Click(object sender, EventArgs e)
        {
            using (conn = con.getConnect())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Contacts WHERE PhoneNumber = @PhoneNumber", conn))
                {
                    cmd.Parameters.AddWithValue("@PhoneNumber", textBox2.Text);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox1.Text = reader["Name"].ToString();
                            textBox3.Text = reader["Email"].ToString();
                            textBox4.Text = reader["ContactID"].ToString();
                            MessageBox.Show("Contact found.");
                        }
                        else
                        {
                            MessageBox.Show("Contact not found.");
                        }
                    }
                }
            }
        }
    }
}