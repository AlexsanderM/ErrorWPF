using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorWpf.Data
{
    public class DataServiceDay
    {
        private int[] services = new int[] { 1001, 1301};

        public int[] getServiceArr()
        {
            return services;
        }
    }
}
