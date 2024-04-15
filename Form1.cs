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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Collections.Generic;
using System.IO;

namespace rps4
{
    public partial class Form1 : Form
    {
        int value = 0;

        public Form1()
        {
            InitializeComponent();

            dataGridView1.ReadOnly = true;

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

        private static bool CheckShowGreeting()
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
                // dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString(); // Add row number
                //lastRowIndex1 = i + 1;
                dataGridView1.Rows[i].Cells[0].ReadOnly = true; // Make the first column read-only
            }
        }

        private void last()
        {
            int lastIndex = dataGridView1.Rows.Count - 2;

            // Получаем значение в первой колонке последней строки
            value = Convert.ToInt32(dataGridView1.Rows[lastIndex].Cells[0].Value);
        }

        private void see()
        {
            BD db = new BD();

            string query = "select id as id, date as 'Дата', time as 'Время', travel_time as  'Время в пути', departure as 'Откуда', destination as 'Куда' from q";

            DataTable table = new DataTable();
            SQLiteConnection connection = new SQLiteConnection(@"Data Source = C:\Users\Арина\Desktop\сем 4\rps4\bin\Debug\bd.db");

            SQLiteCommand command = new SQLiteCommand(query, connection);

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            // Скрыть первый столбец
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            see();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            last();

            Form3 form3 = new Form3(value);
            form3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveDataToFile();
        }

        private static void SaveDataToFile()
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

        private static void save(string filePath)
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int gradeBookNumber = 0;
                int index = dataGridView1.CurrentCell.RowIndex;

                if (dataGridView1.RowCount >= index)
                {
                    gradeBookNumber = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                }

                Form4 form4 = new Form4(gradeBookNumber);
                form4.ShowDialog();
            }
            catch
            {
                MessageBox.Show($"Ошибка при редактировании записи");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dataGridView1.CurrentCell.RowIndex;
         
                if (dataGridView1.RowCount >= index)
                {
                   
                    int gradeBookNumber = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    string message = "Вы уверены, что хотите удалить запись?";
                    string caption = "Сообщение";

                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {

                        button1.PerformClick();

                        using (SqliteConnection conn = new SqliteConnection(@"Data Source = C:\Users\Арина\Desktop\сем 4\rps4\bin\Debug\bd.db"))
                        {
                            conn.Open();
                            string sql = $"DELETE FROM q WHERE  id =" + gradeBookNumber;
                            SqliteCommand cmd = new SqliteCommand(sql, conn);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show($"Выбранная запись была удалена");
                    }
                }
            }
            catch
            {
                MessageBox.Show($"Ошибка при удалении записи");
            }
        }
    }
}
