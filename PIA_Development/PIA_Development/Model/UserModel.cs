using System;
using System.Collections.Generic;
using System.Text;

namespace PIA_Development.Model
{
    public class UserModel
    {
        public string EmailField { get; set; }
        public string PasswordField { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        private static UserModel instancia;
        private UserModel() { }

        // Propiedad estática para acceder a la instancia única de Usuario
        public static UserModel Instancia
        {
            get
            {
                // Si la instancia aún no se ha creado, la creamos
                if (instancia == null)
                {
                    instancia = new UserModel();
                }
                return instancia;
            }
        }


    }
}
