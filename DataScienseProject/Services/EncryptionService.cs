using DataScienseProject.Interfaces;
using System;
using System.Text;

namespace DataScienseProject.Services
{
    public class EncryptionService : IEncryptionService
    {
        private static readonly string SALT1 = "Data";
        private static readonly string SALT2 = "Science";
        public EncryptionService() { }

        public string EncryptPassword(string pass)
        {
            var encryptArr = ASCIIEncoding.ASCII.GetBytes(SALT1 + pass + SALT2);
            var res = Convert.ToBase64String(encryptArr);

            return res;
        }

        public string DescryptPassword(string pass)
        {
            var decryptedPass = string.Empty;
            try
            {
                var descryptArr = Convert.FromBase64String(pass);
                var decryptedSalt = ASCIIEncoding.ASCII.GetString(descryptArr);

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
