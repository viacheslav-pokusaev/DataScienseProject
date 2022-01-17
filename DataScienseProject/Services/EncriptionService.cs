using DataScienseProject.Interfaces;
using System;
using System.Text;

namespace DataScienseProject.Services
{
    public class EncriptionService : IEncriptionService
    {
        private static readonly string SALT1 = "Data";
        private static readonly string SALT2 = "Science";
        public EncriptionService() { }

        public string EncriptPassword(string pass)
        {
            var encriptArr = ASCIIEncoding.ASCII.GetBytes(SALT1 + pass + SALT2);
            var res = Convert.ToBase64String(encriptArr);

            return res;
        }

        public string DescriptPassword(string pass)
        {
            var decryptedPass = string.Empty;
            try
            {
                var descriptArr = Convert.FromBase64String(pass);
                var decryptedSalt = ASCIIEncoding.ASCII.GetString(descriptArr);

                for(var i = SALT1.Length; i < decryptedSalt.Length - SALT2.Length; i++)
                {
                    decryptedPass += decryptedSalt[i];
                }
            }
            catch
            {
            }
            return decryptedPass;
        }
    }
}
