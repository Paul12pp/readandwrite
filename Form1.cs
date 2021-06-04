using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeeryEscribir
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pe=dtPeriodo.Value.ToString("ddMMyyyy");
            string tr=dtTransm.Value.ToString("ddMMyyyy");
            ApplicationDbContext db = new ApplicationDbContext();
            var empleados = db.Empleados.ToList();

            dataGridView1.DataSource = empleados;
            using (StreamWriter outputFile = new StreamWriter(@"C:\Users\USER\Desktop\Clases APEC\Int propietaria\write.txt"))
            {
                string firts = $"02{txtRnc.Text}{pe}{tr}";
                outputFile.WriteLine(firts);
                foreach (var item in empleados)
                {
                    string sueldo = item.Sueldo.ToString();
                    sueldo = sueldo.Replace(",00", "");
                    sueldo = sueldo.Length < 7 ? sueldo += new String(' ', 7 - sueldo.Length) : sueldo;
                    string line = $"02{item.Cedula}{sueldo}{item.Moneda}";
                    outputFile.WriteLine(line);
                }
                outputFile.WriteLine($"02{empleados.Count}");
            }
        }
    }
}
