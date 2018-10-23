using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataParser
{
    public interface IParser
    {
        DataTable ParseToDataTable(string fileFullPath); 
    }
}
