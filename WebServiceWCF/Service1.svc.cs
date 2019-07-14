using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace WebServiceWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        SqlConnection con = new SqlConnection(@"Data Source=localhost; Initial Catalog= Northwind; User Id= sa; Password=1234;");
      //  private string conect = "Data Source=(local);Initial Catalog=Northwind;User ID=user;Password=1234;";

    public string ValidateLogin(string user, string pass)
        {
            string mensaje = "";
            List<string> UserPassword = new List<string>();
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Usuarios WHERE Username =@user and PasswordUser = @pass", con);

               /* if (string.IsNullOrEmpty(this.txtFirstName.Text))
                {
                    cmd.Parameters.AddWithValue("@Name", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Name", this.txtFirstName.Text);
                }*/
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@pass", pass);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    mensaje = "El usuario ha sido encontrado";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string name = dt.Rows[i]["UserName"].ToString();
                        string password = dt.Rows[i]["PasswordUser"].ToString();
                        UserPassword.Add(name);
                        UserPassword.Add(password);

                    }
                }
                else
                {
                    mensaje = "No se encontraron coincidencias";
                }
                con.Close();
            }
            return mensaje;
        }
          

    }

}
