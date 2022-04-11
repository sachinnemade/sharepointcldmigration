using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudinaryMigration
{
    public partial class Threadtest : Form
    {
        public Threadtest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Task task1 = Task.Factory.StartNew(() => doStuff(1));
            Task task2 = Task.Factory.StartNew(() => doStuff(2));
            Task task3 = Task.Factory.StartNew(() => doStuff(3));

            Task.WaitAll(task1, task2, task3);
            Console.WriteLine("All threads complete");
        }


        static void doStuff(int i)
        {
            Console.WriteLine("Thread no " + i + " Started.");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Thread no " + i + " finished");
        }
    }
}
