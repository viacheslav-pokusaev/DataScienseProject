using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Interfaces
{
    public interface IEncriptionService
    {
        string EncriptPassword(string pass);
        string DescriptPassword(string pass);
    }
}
