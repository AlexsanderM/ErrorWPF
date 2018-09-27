using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorWpf.Data
{
    public class DataServiceAll
    {
        private readonly int[] services = new int[] {
            1001, 1002, 1011, 1012, 1051, 1052, 1141, 1142, 1161, 1162, 1261, 1262, 1301,
            1302, 1371, 1372, 1801, 1802, 22106};

        public int[] getServiceArr()
        {
            return services;
        }
    }
}
