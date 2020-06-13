using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        private readonly ApplicationContext _context = new ApplicationContext();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = _context.Categories.Local.ToBindingList();
            dataGridView2.DataSource = _context.News.Local.ToBindingList();
            dataGridView2.Columns["Category"].Visible = false;
            _context.Categories.ToList();
            _context.News.ToList();
            var query = _context.News.Join(
                    _context.Categories,
                    n => n.CategoryId,
                    ca => ca.CategoryId,
                    (n, ca) => new NewsCategory
                    {
                        NewId = n.NewId,
                        Name = n.Name,
                        Text = n.Text,
                        CategoryId = n.CategoryId,
                        CategoryName = ca.Name,
                        CategoryDescription = ca.Description
                    });
            query = query.Where(s => s.Text.Length <= numericUpDown1.Value);
            dataGridView3.DataSource = query.ToList();
            dataGridView3.Columns["NewId"].Visible = false;
            dataGridView3.Columns["CategoryId"].Visible = false;
        }

        private void save_changes_button(object sender, EventArgs e)
        {
            _context.SaveChanges();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Category category_transport = new Category { Name = "Transport", Description="News about trasport" };
            Category category_politics = new Category { Name = "Politics", Description = "Important politics news" };
            Category category_hitech = new Category { Name = "HiTech", Description = "All about hight techonogies" };

            _context.Categories.Add(category_transport);
            _context.Categories.Add(category_politics);
            _context.Categories.Add(category_hitech);

            _context.News.Add(new New { Category = category_hitech, Name = "Android 11", Text = "Google released new beta version of their mobile operating system" });
            _context.News.Add(new New { Category = category_hitech, Name = "Jailbreak!", Text = "Hackers found new way to get Jailbreak on last iOS version" });
            _context.News.Add(new New { Category = category_politics, Name = "#BLM", Text = "Stupid people still stealing and breaking shops in usa" });
            _context.News.Add(new New { Category = category_politics, Name = "Make America great again", Text = "Trump, we believe in you" });
            _context.News.Add(new New { Category = category_transport, Name = "Nothing new", Text = "No news for now... Waiting for coronovirus end" });

            _context.SaveChanges();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var query = _context.News.Join(
                    _context.Categories,
                    n => n.CategoryId,
                    ca => ca.CategoryId,
                    (n, ca) => new NewsCategory
                    {
                        NewId = n.NewId,
                        Name = n.Name,
                        Text = n.Text,
                        CategoryId = n.CategoryId,
                        CategoryName = ca.Name,
                        CategoryDescription = ca.Description
                    });
            query = query.Where(s => s.Text.Length <= numericUpDown1.Value);
            dataGridView3.DataSource = query.ToList();
            dataGridView3.Columns["NewId"].Visible = false;
            dataGridView3.Columns["CategoryId"].Visible = false;
        }
    }
}
