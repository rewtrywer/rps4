using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace rps4
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();

            dataGridView1.RowsAdded += dataGridView1_RowsAdded;

            button5.Visible = false;
            button6.Visible = false;
            if (CheckShowGreeting())
            {
                checkBox1.Checked = true;
                Form2 form2 = new Form2();
                form2.ShowDialog();
            }
            else
            {
                checkBox1.Checked = false;
            }

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            ChangeShowGreeting(checkBox1.Checked);
        }

        private static void ChangeShowGreeting(bool isChecked)
        {
            const string filePath = "C:\\Users\\Арина\\Desktop\\сем 4\\rps4\\bin\\Debug\\ShowGreetingCheck.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                if (isChecked)
                {
                    writer.WriteLine("1");
                }
                else
                {
                    writer.WriteLine("0");
                }
            }

        }

        private bool CheckShowGreeting()
        {
            const string filePath = "C:\\Users\\Арина\\Desktop\\сем 4\\rps4\\bin\\Debug\\ShowGreetingCheck.txt";
            using (StreamReader reader = new StreamReader(filePath))
            {
                if (reader.ReadLine() == "1")
                {
                    reader.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    return false;
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            see();
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) // добавление номера строки(id)
        {
            for (int i = 1; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString(); // Add row number
                dataGridView1.Rows[i].Cells[0].ReadOnly = true; // Make the first column read-only
            }
        }

        private void see()
        {
            BD db = new BD();

            string query = "select * from q";

            DataTable table = new DataTable();
            SQLiteConnection connection = new SQLiteConnection(@"Data Source = C:\Users\Арина\Desktop\сем 4\rps4\bin\Debug\bd.db");

            SQLiteCommand command = new SQLiteCommand(query, connection);

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            see();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Проверяем, если строка не новая
                if (!row.IsNewRow)
                {
                    int id = Convert.ToInt32(row.Cells[0].Value);
                    string? date = row.Cells[1].Value.ToString();
                    string? time = row.Cells[2].Value.ToString();
                    string? travel_time = row.Cells[3].Value.ToString();
                    string? departure = row.Cells[4].Value.ToString();
                    string? destination = row.Cells[5].Value.ToString();

                    add(id, date, time, travel_time, departure, destination);

                }
            }

            button5.Visible = false;
            button2.Visible = true;

            button1.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Разрешаем пользователю добавлять новые строки
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = true;

            // Запрещаем редактирование существующих строк
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.ReadOnly = true;
                    }
                }
            }

            // Разрешаем редактирование ячеек в новых строках
            dataGridView1.Rows[dataGridView1.NewRowIndex].ReadOnly = false;
            button2.Visible = false;
            button5.Visible = true;

            button1.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;


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
                    if (DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                        && DateTime.TryParseExact(time, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                        && DateTime.TryParseExact(travel_time, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
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
                    else
                    {
                        MessageBox.Show("Error: Date, time, or travel time format is incorrect");
                    }
                }

                
            }

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveDataToFile();
        }

        private void SaveDataToFile()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
            saveFileDialog1.Title = "Save Data File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                string filePath = saveFileDialog1.FileName;
                save(filePath); // Вызов функции сохранения данных в указанный файл
            }
        }

        private void save(string filePath)
        {
            using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = C:\Users\Арина\Desktop\сем 4\rps4\bin\Debug\bd.db"))
            {
                connection.Open();

                string query = "SELECT * FROM q";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            while (reader.Read())
                            {
                                StringBuilder sb = new StringBuilder();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    sb.Append(reader[i].ToString());
                                    sb.Append(",");
                                }

                                writer.WriteLine(sb.ToString().TrimEnd(','));
                            }
                        }
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            update();

            button6.Visible = false;
            button3.Visible = true;

            button1.Enabled = true;
            button2.Enabled = true;
            button4.Enabled = true;
            button7.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // update();
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.ReadOnly = false;
                    }
                }
            }
            button3.Visible = false;
            button6.Visible = true;

            button1.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            button7.Enabled = false;
        }

        private void update()
        {
            using (SQLiteConnection connection = new SQLiteConnection(@"Data Source = C:\Users\Арина\Desktop\сем 4\rps4\bin\Debug\bd.db"))
            {
                connection.Open();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow && AllCellsFilled(row))  // Проверяем, что строка не новая и есть значение в первой ячейке (id)
                    {
                        int id = Convert.ToInt32(row.Cells[0].Value);
                        string date = row.Cells[1].Value.ToString();
                        string time = row.Cells[2].Value.ToString();
                        string travel_time = row.Cells[3].Value.ToString();
                        string departure = row.Cells[4].Value.ToString();
                        string destination = row.Cells[5].Value.ToString();

                        string queryUpdate = "UPDATE q SET date = @date, time = @time, travel_time = @travel_time, departure = @departure, destination = @destination WHERE id = @id";

                        using (SQLiteCommand updateCommand = new SQLiteCommand(queryUpdate, connection))
                        {
                            if (DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                                && DateTime.TryParseExact(time, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                                && DateTime.TryParseExact(travel_time, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                            {
                                updateCommand.Parameters.AddWithValue("@id", id);
                                updateCommand.Parameters.AddWithValue("@date", date);
                                updateCommand.Parameters.AddWithValue("@time", time);
                                updateCommand.Parameters.AddWithValue("@travel_time", travel_time);
                                updateCommand.Parameters.AddWithValue("@departure", departure);
                                updateCommand.Parameters.AddWithValue("@destination", destination);

                                updateCommand.ExecuteNonQuery();
                            }
                            else
                            {
                                MessageBox.Show("Error: Date, time, or travel time format is incorrect");
                            }
                        }
                    }
                }
            }
        }

        private bool AllCellsFilled(DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    return false; // Возвращаем false, если хотя бы одна ячейка пуста или содержит только пробелы
                }
            }
            return true; // Возвращаем true, если все ячейки заполнены
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

      
    }
}
