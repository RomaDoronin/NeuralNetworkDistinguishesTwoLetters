using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perzeptron
{
    public class MathFunc
    {
        public static int Sgnm(float num)
        {
            if (num > 0)
            {
                return 1;
            }
            else if (num < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
