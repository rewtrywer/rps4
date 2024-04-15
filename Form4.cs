using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rps4
{
    public partial class Form4 : Form
    {
        int data1;
        public Form4(int data)
        {
            InitializeComponent();
            data1 = data;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            update_info();
        }

        private void update_info()
        {
            using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = C:\Users\Арина\Desktop\сем 4\rps4\bin\Debug\bd.db"))
            {
                connection.Open();

                string queryUpdate = "select id as id, date as date, time as time, travel_time as travel_time, departure as departure, destination as destination from q WHERE id =" + data1;

                using (SQLiteCommand command = new SQLiteCommand(queryUpdate, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string? date = reader["date"].ToString();
                            string? time = reader["time"].ToString();
                            string? travelTime = reader["travel_time"].ToString();
                            string? departure = reader["departure"].ToString();
                            string? destination = reader["destination"].ToString();

                            dateTimePicker1.Value = DateTime.Parse(date);
                            dateTimePicker2.Value = DateTime.Parse(time);
                            dateTimePicker3.Value = DateTime.Parse(travelTime);
                            textBox5.Text = departure;
                            textBox6.Text = destination;

                        }
                    }
                }
            }
        }

        private void update()
        {
            using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = C:\Users\Арина\Desktop\сем 4\rps4\bin\Debug\bd.db"))
            {
                connection.Open();

                string? date = dateTimePicker1.Text;
                string? time = dateTimePicker2.Text;
                string? travel_time = dateTimePicker3.Text;
                string? departure = textBox5.Text;
                string? destination = textBox6.Text;

                if (departure == "" || destination == "")
                {
                    MessageBox.Show("Заполните все поля");
                }
                else
                {
                    string queryUpdate = "UPDATE q SET date = @date, time = @time, travel_time = @travel_time, departure = @departure, destination = @destination WHERE id =" + data1;

                    using (SQLiteCommand updateCommand = new SQLiteCommand(queryUpdate, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@date", date);
                        updateCommand.Parameters.AddWithValue("@time", time);
                        updateCommand.Parameters.AddWithValue("@travel_time", travel_time);
                        updateCommand.Parameters.AddWithValue("@departure", departure);
                        updateCommand.Parameters.AddWithValue("@destination", destination);

                        updateCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Запись отредактированна");
                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            update();
        }
    }
}
