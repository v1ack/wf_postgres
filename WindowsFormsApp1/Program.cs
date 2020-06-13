using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public class NewsCategory
    {
        public int NewId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }
    public class New
    {
        public int NewId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<New> News { get; set; }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<New> News { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost; Port=5432; User Id=me;Password=hackme; Database=windows_app;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.UseSerialColumns();

    }
    static class Program
    {

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
