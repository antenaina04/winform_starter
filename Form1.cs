using CitizenConnect.Model;
using Dapper;
using System.Data;
using System.Data.SQLite;

namespace CitizenConnect
{
    public partial class Form1 : Form
    {
        //public List<UserModel> userModels = new List<UserModel>();
        public IDbConnection GetConnection()
        {
            return new SQLiteConnection(@"Data Source=D:\UsersDB.db; Version=3;New=true");
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(@"D:\UsersDB.db"))
            {
                LoadData();
            }
        }
        private void LoadData()
        {
            using (IDbConnection _db = GetConnection())
            {
                var userList = _db.Query<UserModel>("select * from Users").ToList();
                if (userList.Count() > 0)
                {
                    dataGridView1.DataSource = userList;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (IDbConnection _db = GetConnection())
            {
                _db.Execute("Create Table Users(ID int, Name varchar(50), FirstName varchar(50), Username varchar(50), Password varchar(50))");
                MessageBox.Show("La table Users a été créee");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (IDbConnection _db = GetConnection())
            {
                int ID = Convert.ToInt32(textBox1.Text);
                string Name = textBox2.Text;
                string FirstName = textBox3.Text;
                string Username = textBox4.Text;
                string Password = textBox5.Text;
                int query = _db.Execute("insert into Users(ID,Name,FirstName,Username,Password) values (@ID,@Name,@FirstName,@Username,@Password)", new { ID = ID, Name = Name, FirstName = FirstName, Username = Username, Password = Password });

                if (query > 0)
                {
                    MessageBox.Show("User inseré");
                    LoadData();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int idSelectedItem = (int)dataGridView1.CurrentRow.Cells[0].Value;
            using (IDbConnection _db = GetConnection())
            {
                int query = _db.Execute("delete from Users where ID=@ID", new { ID = idSelectedItem });
                if (query > 0)
                {
                    MessageBox.Show("User supprimé");
                    LoadData();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int idSelectedItem = (int)dataGridView1.CurrentRow.Cells[0].Value;
            string Name = (string)dataGridView1.CurrentRow.Cells[1].Value;
            string FirstName = (string)dataGridView1.CurrentRow.Cells[2].Value;
            string Username = (string)dataGridView1.CurrentRow.Cells[3].Value;
            string Password = (string)dataGridView1.CurrentRow.Cells[4].Value;

            using (IDbConnection _db = GetConnection())
            {
                int query = _db.Execute("delete from Users where ID=@ID", new { ID = idSelectedItem, Name = Name, FirstName = FirstName, Username = Username, Password = Password });
                if (query > 0)
                {
                    MessageBox.Show("User Modifié");
                    LoadData();
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}