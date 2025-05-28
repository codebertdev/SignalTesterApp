using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SignalTesterApp.GUI
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "server=localhost;user=root;password=YOURPASS;database=signaltester";

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData(); // vagy amit szeretnél indításkor
        }

        private void LoadData()
        {
            using var conn = new MySqlConnection(ConnectionString);
            var adapter = new MySqlDataAdapter("SELECT * FROM decoded_data", conn);
            var table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
