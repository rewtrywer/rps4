using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace rps4
{
    public partial class Form3 : Form
    {
        int data1 = 0;
        public Form3(int data)
        {
            InitializeComponent();
            data1 = data + 1;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = data1;
            string? date = dateTimePicker1.Text;
            string? time = dateTimePicker2.Text;
            string? travel_time = dateTimePicker2.Text;
            string? departure = textBox5.Text;
            string? destination = textBox6.Text;

            if (departure == "" || destination == "")
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                add(id, date, time, travel_time, departure, destination);
                MessageBox.Show("Запись добавлена");
                this.Close();
            }
        }

        private void add(int id, string? date, string? time, string? travel_time, string? departure, string? destination)
        {
            BD db = new BD();

            string queryCheckExisting = "SELECT COUNT(*) FROM q WHERE id = @id";
            string queryInsert = "INSERT INTO q (id, date, time, travel_time, departure, destination) VALUES (@id, @date, @time, @travel_time, @departure, @destination)";

            DataTable table = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = C:\Users\Арина\Desktop\сем 4\rps4\bin\Debug\bd.db"))
            {
                connection.Open();

                using (SQLiteCommand checkCommand = new SQLiteCommand(queryCheckExisting, connection))
                {
                    
                        checkCommand.Parameters.AddWithValue("@id", id.ToString());
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count == 0)
                        {
                            using (SQLiteCommand insertCommand = new SQLiteCommand(queryInsert, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@id", id.ToString());
                                insertCommand.Parameters.AddWithValue("@date", date);
                                insertCommand.Parameters.AddWithValue("@time", time);
                                insertCommand.Parameters.AddWithValue("@travel_time", travel_time);
                                insertCommand.Parameters.AddWithValue("@departure", departure);
                                insertCommand.Parameters.AddWithValue("@destination", destination);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                }
            }
        }
    }   
}
