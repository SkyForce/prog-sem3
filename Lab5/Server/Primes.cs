using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Primes : IPrimes
    {
        bool IsPrime(int n)
        {
            for (int i = 2; i <= Math.Sqrt(n); i++)
                if (n % i == 0) return false;
            return true;
        }
        public ArrayList GetPrimes(int n)
        {
            ArrayList l = new ArrayList();
            for (int i = 2; i <= n; i++)
                if (IsPrime(i)) l.Add(i);
            return l;
        }
    }
}
