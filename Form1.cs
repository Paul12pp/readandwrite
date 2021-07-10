using LeeryEscribir.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
                string cabecera = $"02100100001{fechaDePago}{fechaDePago}";
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
            //Empleados empleado = new Empleados()
            //{
            //    Cedula = documentoIdentidad.Text,
            //    Moneda = RD.Checked ? "RD" : "USD",
            //    Nombre = nombreEmpleado.Text,
            //    Sueldo = sueldoBruto.Value
            //};
            try
            {
                PostHttpRequest();
                //postToDatabase(empleado);
                getHttpRequest();
            }
            catch (Exception)
            {
                throw;
            }


        }

        private static void postToDatabase(Empleados empleado)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                dbContext.Empleados.Add(empleado);
                dbContext.SaveChanges();

            }
        }

        private void PostHttpRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://localhost:38478/api/integration");

            request.Method = "POST";
            request.ContentType = "application/json";
            IntegrationInputDto integrationInputDto = new IntegrationInputDto()
            {
                Periodo = DateTime.Now,
                RNC = "100-000000-1",
                Transimision = DateTime.Now.AddDays(1),
                Empleados = new List<Empleados>()
                {
                    new Empleados()
                    {
                         Cedula = documentoIdentidad.Text,
                Moneda = RD.Checked ? "RD" : "USD",
                Nombre = nombreEmpleado.Text,
                Sueldo = sueldoBruto.Value
                    }
                }
            };

            
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

            var empleado = JsonConvert.SerializeObject(integrationInputDto);

            byte[] byteArray = Encoding.UTF8.GetBytes(empleado);
            request.ContentLength = byteArray.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }

            //getHttpRequest();

            //dataGridView1.DataSource = null;
            //dataGridView1.Refresh();
            //dataGridView1.DataSource = empleado;
        }

        [Obsolete]
        private void getDatabaseRequest()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = dbContext.Empleados.ToList();
        }

        private void getHttpRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://localhost:38478/api/integration/getempleados");

            request.Method = "GET";
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

            List<Empleados> empleado = JsonConvert.DeserializeObject<List<Empleados>>(content);

            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = empleado;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'nominaDataSet.Empleados' table. You can move, or remove it, as needed.
            //this.empleadosTableAdapter.Fill(this.nominaDataSet.Empleados);
            getHttpRequest();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            getHttpRequest();
        }
    }
}
