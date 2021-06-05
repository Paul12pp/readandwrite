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
            ApplicationDbContext dbContext = new ApplicationDbContext();
            
            var empleados = dbContext.Empleados.ToList();

            dataGridView1.DataSource = empleados;
            using (StreamWriter outputFile = new StreamWriter(@"C:\Users\ec319981\Documents\Unapec\Propietaria\write.txt"))
            {
                string fechaDePago = DateTime.Now.ToString("dd/MM/yyyy");
                string cabecera = $"0210010001{fechaDePago}{fechaDePago}";
                outputFile.WriteLine(cabecera);
               
                foreach (var item in empleados)
                {
                    string sueldo = item.Sueldo.ToString();
                    sueldo = sueldo.Replace(",00", "");
                    sueldo = sueldo.Length < 7 ? sueldo += new string('0', 7 - sueldo.Length) : sueldo;
                    string line = $"02{item.Cedula}{sueldo}{item.Moneda}";
                    outputFile.WriteLine(line);
                }
                outputFile.WriteLine($"02{empleados.Count}");
            }
        }

        private void agregar_Click(object sender, EventArgs e)
        {
            Empleados empleado = new Empleados()
            {
                Cedula = documentoIdentidad.Text,
                Moneda = RD.Checked ? "RD" : "USD",
                Nombre = nombreEmpleado.Text,
                Sueldo = sueldoBruto.Value
            };
            try
            {
                ApplicationDbContext dbContext = new ApplicationDbContext();
                dbContext.Empleados.Add(empleado);
                dbContext.SaveChanges();


                dataGridView1.Update();
                dataGridView1.Refresh();
            }
            catch (Exception)
            {
                throw;
            }
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'nominaDataSet.Empleados' table. You can move, or remove it, as needed.
            this.empleadosTableAdapter.Fill(this.nominaDataSet.Empleados);

        }
    }
}
