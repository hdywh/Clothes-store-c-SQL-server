using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace rabota1
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        bdd bd = new bdd();

        // 🔹 При загрузке формы — загружаем данные
             private void Admin_Load(object sender, EventArgs e)
             {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "companyDBDataSet5.Заказы". При необходимости она может быть перемещена или удалена.
            this.заказыTableAdapter2.Fill(this.companyDBDataSet5.Заказы);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "companyDBDataSet3.Промокоды". При необходимости она может быть перемещена или удалена.
            this.промокодыTableAdapter2.Fill(this.companyDBDataSet3.Промокоды);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "companyDBDataSet3.Каталог". При необходимости она может быть перемещена или удалена.
            this.каталогTableAdapter1.Fill(this.companyDBDataSet3.Каталог);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "companyDBDataSet3.Заказы". При необходимости она может быть перемещена или удалена.
            
            // TODO: данная строка кода позволяет загрузить данные в таблицу "companyDBDataSet1.Промокоды". При необходимости она может быть перемещена или удалена.
            this.промокодыTableAdapter1.Fill(this.companyDBDataSet1.Промокоды);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "companyDBDataSet.Промокоды". При необходимости она может быть перемещена или удалена.
            this.промокодыTableAdapter.Fill(this.companyDBDataSet.Промокоды);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "companyDBDataSet.Заказы". При необходимости она может быть перемещена или удалена.
           
            // TODO: данная строка кода позволяет загрузить данные в таблицу "companyDBDataSet.Поставка". При необходимости она может быть перемещена или удалена.
            
            
        }

      
        private void guna2ButtonEdit_Click(object sender, EventArgs e)
        {
            
        }

        // 🔹 Удаление записи
        private void guna2ButtonDelete_Click(object sender, EventArgs e)
        {
   
        }

        // 🔹 Добавление новой записи
        private void guna2ButtonAdd_Click(object sender, EventArgs e)
        {
         
        }

        private void UpdatePriceChart()
        {
                try
                {
                    chartPrices.Series.Clear();
                    chartPrices.ChartAreas.Clear();

                    ChartArea area = new ChartArea("PriceArea");
                    chartPrices.ChartAreas.Add(area);

                    Series series = new Series("Цены");
                    series.ChartType = SeriesChartType.Column;

                    // Поиск колонок по HeaderText
                    DataGridViewColumn colName = guna2DataGridView2.Columns
                        .Cast<DataGridViewColumn>()
                        .FirstOrDefault(c => c.HeaderText == "Название");

                    DataGridViewColumn colPrice = guna2DataGridView2.Columns
                        .Cast<DataGridViewColumn>()
                        .FirstOrDefault(c => c.HeaderText == "Цена");

                    if (colName == null || colPrice == null)
                    {
                        MessageBox.Show("Не найдено колонок 'Название' или 'Цена'. Проверь HeaderText.");
                        return;
                    }

                    var priceData = new List<(string Name, decimal Price)>();

                    foreach (DataGridViewRow row in guna2DataGridView2.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string name = Convert.ToString(row.Cells[colName.Index].Value);

                        decimal price = 0;
                        decimal.TryParse(Convert.ToString(row.Cells[colPrice.Index].Value), out price);

                        priceData.Add((name, price));
                    }

                    // сортировка от большего к меньшему
                    foreach (var item in priceData.OrderByDescending(x => x.Price))
                    {
                        series.Points.AddXY(item.Name, item.Price);
                    }

                    chartPrices.Series.Add(series);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при построении диаграммы: " + ex.Message);
                }
            }

        private void UpdateChart1FromTable2()
        {
            try
            {
                chart1.Series.Clear();
                chart1.ChartAreas.Clear();
                chart1.Legends.Clear();

                // ---- Создаём область диаграммы ----
                ChartArea area = new ChartArea("PriceArea");
                chart1.ChartAreas.Add(area);

                // Настройки осей
                area.AxisX.Title = "Товар";
                area.AxisY.Title = "Цена";

                area.AxisX.Interval = 2;
                area.AxisX.LabelStyle.Angle = -45;
                area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9);

                area.AxisY.LabelStyle.Font = new Font("Segoe UI", 9);

                area.AxisY.MajorGrid.LineWidth = 1;
                area.AxisY.MajorGrid.LineColor = Color.LightGray;
                area.AxisX.MajorGrid.Enabled = false;

                // ---- Легенда ----
                Legend legend = new Legend();
                
                chart1.Legends.Add(legend);

                // ---- Серия ----
                Series series = new Series("Цены");
                series.ChartType = SeriesChartType.Column;
                series.BorderWidth = 1;
                series.BorderColor = Color.Black;
                series["PointWidth"] = "0.6";

                // ---- Чтение таблицы ----
                DataGridViewColumn colName = guna2DataGridView1.Columns
                    .Cast<DataGridViewColumn>()
                    .FirstOrDefault(c => c.HeaderText == "Название товара");

                DataGridViewColumn colPrice = guna2DataGridView1.Columns
                    .Cast<DataGridViewColumn>()
                    .FirstOrDefault(c => c.HeaderText == "Цена");

                if (colName == null || colPrice == null)
                {
                    MessageBox.Show("Не найдено колонок 'Название товара' или 'Цена'. Проверь HeaderText.");
                    return;
                }

                var priceData = new List<(string Name, decimal Price)>();

                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    string name = Convert.ToString(row.Cells[colName.Index].Value);

                    decimal price = 0;
                    decimal.TryParse(Convert.ToString(row.Cells[colPrice.Index].Value), out price);

                    priceData.Add((name, price));
                }

                // ---- Добавление значений ----
                foreach (var item in priceData.OrderByDescending(x => x.Price))
                {
                    series.Points.AddXY(item.Name, item.Price);
                }

                chart1.Series.Add(series);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при построении диаграммы Chart1: " + ex.Message);
            }
        }



        private void guna2ButtonAdd_Click_1(object sender, EventArgs e)
        {
            // Проверка, чтобы все поля были заполнены
            if (guna2TextBox8.Text != "" &&
                guna2TextBox9.Text != "" &&
                guna2TextBox10.Text != "" &&
                guna2TextBox11.Text != "" &&
                guna2TextBox12.Text != "")
            {
                // Формируем SQL-запрос на добавление новой записи
                string query = $"INSERT INTO Каталог (item_name, Тип, [Количество товара], Размеры, Цена) " +
                               $"VALUES ('{guna2TextBox8.Text}', '{guna2TextBox9.Text}', '{guna2TextBox10.Text}', '{guna2TextBox11.Text}', '{guna2TextBox12.Text}')";

                SqlCommand cmd = new SqlCommand(query, bd.GetCon());
                bd.Open();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Товар успешно добавлен!");
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении товара!");
                }

                bd.Close();

                // (необязательно) обновить таблицу
                // LoadData();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля перед добавлением!");
            }
        }

        private void guna2ButtonEdit_Click_1(object sender, EventArgs e)
        {
            // Проверяем, что есть выделенная строка
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для редактирования");
                return;
            }

            // Получаем ID из выделенной строки
            int id = Convert.ToInt32(guna2DataGridView1.SelectedRows[0].Cells[0].Value);

            // Проверяем, что все поля заполнены
            if (guna2TextBox8.Text != "" &&
                guna2TextBox9.Text != "" &&
                guna2TextBox10.Text != "" &&
                guna2TextBox11.Text != "" &&
                guna2TextBox12.Text != "")
            {
                // Формируем запрос
                string query = $"UPDATE Каталог SET " +
                               $"item_name = '{guna2TextBox8.Text}', " +
                               $"Тип = '{guna2TextBox9.Text}', " +
                               $"[Количество товара] = '{guna2TextBox10.Text}', " +
                               $"Размеры = '{guna2TextBox11.Text}', " +
                               $"Цена = '{guna2TextBox12.Text}' " +
                               $"WHERE item_id = {id}";

                SqlCommand cmd = new SqlCommand(query, bd.GetCon());
                bd.Open();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Товар успешно изменен");
                }
                else
                {
                    MessageBox.Show("Ошибка при изменении товара");
                }

                bd.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля для редактирования");
            }
        }

        private void guna2ButtonDelete_Click_1(object sender, EventArgs e)
        {
            int index = int.Parse(guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString());

            string query = $"DELETE FROM Каталог WHERE item_id={index}";
            SqlCommand cmd = new SqlCommand(query, bd.GetCon());
            bd.Open();
            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Успешное удаление");
            }
            bd.Close();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления!");
                return;
            }

            int id = Convert.ToInt32(guna2DataGridView2.SelectedRows[0].Cells[0].Value);

            string query = $"DELETE FROM Заказы WHERE id_zakaza = {id}";

            SqlCommand cmd = new SqlCommand(query, bd.GetCon());
            bd.Open();

            if (cmd.ExecuteNonQuery() == 1)
                MessageBox.Show("Заказ успешно удалён!");
            else
                MessageBox.Show("Ошибка при удалении заказа!");

            bd.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для редактирования!");
                return;
            }

            int id = Convert.ToInt32(guna2DataGridView2.SelectedRows[0].Cells[0].Value);

            if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "" && guna2TextBox3.Text != "" &&
                guna2TextBox4.Text != "" && guna2TextBox5.Text != "" &&
                guna2TextBox6.Text != "" )
            {
                string query = $"UPDATE Заказы SET " +
                               $"Клиент = '{guna2TextBox1.Text}', " +
                               $"[Кол-во] = '{guna2TextBox2.Text}', " +
                               $"Цена = '{guna2TextBox3.Text}', " +
                               $"Промокод = '{guna2TextBox4.Text}', " +
                               $"Название = '{guna2TextBox5.Text}', " +
                               $"Размер = '{guna2TextBox6.Text}' " +
                               $"WHERE id_zakaza = {id}";

                SqlCommand cmd = new SqlCommand(query, bd.GetCon());
                bd.Open();

                if (cmd.ExecuteNonQuery() == 1)
                    MessageBox.Show("Заказ успешно изменён!");
                else
                    MessageBox.Show("Ошибка при изменении заказа!");

                bd.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля для редактирования!");
            }

        }
        private void LoadData()
        {
           
        }


        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Проверка заполнения всех полей
            if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "" && guna2TextBox3.Text != "" &&
                guna2TextBox3.Text != "" && guna2TextBox4.Text != "" &&
                guna2TextBox5.Text != "" && guna2TextBox6.Text != "")
            {
                string query = $"INSERT INTO Заказы ( Клиент, [Кол-во], Цена, Промокод, Название, Размер) " +
                               $"VALUES ('{guna2TextBox1.Text}', '{guna2TextBox2.Text}', '{guna2TextBox3.Text}', " +
                               $"'{guna2TextBox4.Text}', '{guna2TextBox5.Text}', '{guna2TextBox6.Text}')";

                SqlCommand cmd = new SqlCommand(query, bd.GetCon());
                bd.Open();

                if (cmd.ExecuteNonQuery() == 1)
                    MessageBox.Show("Заказ успешно добавлен!");
                else
                    MessageBox.Show("Ошибка при добавлении заказа!");

                bd.Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля перед добавлением!");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView3.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления!");
                return;
            }

            int id = Convert.ToInt32(guna2DataGridView3.SelectedRows[0].Cells[0].Value);

            string query = $"DELETE FROM Промокоды WHERE id_promo = {id}";

            SqlCommand cmd = new SqlCommand(query, bd.GetCon());
            bd.Open();

            if (cmd.ExecuteNonQuery() == 1)
                MessageBox.Show("Промокод успешно удалён!");
            else
                MessageBox.Show("Ошибка при удалении промокода!");

            bd.Close();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView3.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для редактирования!");
                return;
            }

            int id = Convert.ToInt32(guna2DataGridView3.SelectedRows[0].Cells[0].Value);

            if (guna2TextBox13.Text != "" && guna2TextBox14.Text != "")
            {
                string query = $"UPDATE Промокоды SET " +
                               $"[активные promo] = '{guna2TextBox13.Text}', " +
                               $"[сезонные promo] = '{guna2TextBox14.Text}' " +
                               $"WHERE id_promo = {id}";

                SqlCommand cmd = new SqlCommand(query, bd.GetCon());
                bd.Open();

                if (cmd.ExecuteNonQuery() == 1)
                    MessageBox.Show("Промокод успешно изменён!");
                else
                    MessageBox.Show("Ошибка при изменении промокода!");

                bd.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля для редактирования!");
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (
            guna2TextBox13.Text != "" &&
            guna2TextBox14.Text != "")
            {
                // Формируем SQL-запрос на добавление новой записи
                string query = $"INSERT INTO Промокоды ([активные promo], [сезонные promo]) " +
                               $"VALUES ('{guna2TextBox13.Text}', '{guna2TextBox14.Text}')";

                SqlCommand cmd = new SqlCommand(query, bd.GetCon());
                bd.Open();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Промокоды успешно добавлен!");
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении промокодов!");
                }

                bd.Close();

                // (необязательно) обновить таблицу
                // LoadData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void chartPrices_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
           
            chartPrices.Visible = true;
            UpdatePriceChart();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            
            chart1.Visible = true;
            UpdateChart1FromTable2();
        }

        private void chart1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button7_Click_1(object sender, EventArgs e)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rnd = new Random();
            string promo = "";

            for (int i = 0; i < 10; i++)  // длина промокода = 10 символов
            {
                promo += chars[rnd.Next(chars.Length)];
            }

            guna2TextBox15.Text = promo;
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
