using DataScienseProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Services
{
    public class EncriptionService : IEncriptionService
    {
        public EncriptionService(){ }

        public string EncriptPassword(string pass)
        {
            var encriptedPass = string.Empty;

            var index = pass.ToCharArray().Length;

            foreach (var charElem in pass.ToCharArray())
            {
                encriptedPass += (Convert.ToInt32(charElem) + index);
                index--;
            }

            return encriptedPass;
        }

        public string DescriptionPassword(string pass)
        {
            var decriptedPass = string.Empty;

            var index = pass.ToCharArray().Length;

            foreach (var charElem in pass.ToCharArray())
            {
                decriptedPass += (Convert.ToInt32(charElem) - index);
                index--;
            }

            return decriptedPass;
        }
    }
}
