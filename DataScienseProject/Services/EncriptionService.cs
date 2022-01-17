using DataScienseProject.Interfaces;
using System;

namespace DataScienseProject.Services
{
    public class EncriptionService : IEncriptionService
    {
        public EncriptionService() { }

        public string EncriptPassword(string pass)
        {
            if (!string.IsNullOrEmpty(pass))
            {
                var encriptedPass = string.Empty;
                var index = pass.ToCharArray().Length;

                foreach (var charElem in pass.ToCharArray())
                {
                    encriptedPass += (char)(Convert.ToInt32(charElem) + index);
                    index--;
                }

                return encriptedPass;
            }
            return null;
        }

        public string DescriptPassword(string pass)
        {
            var decriptedPass = string.Empty;

            var index = pass.ToCharArray().Length;

            foreach (var charElem in pass.ToCharArray())
            {
                decriptedPass += (char)(Convert.ToInt32(charElem) - index);
                index--;
            }

            return decriptedPass;
        }
    }
}
