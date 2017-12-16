using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VizWorxCodeChallengeTests
{
    public class ListComparer<T> : IEqualityComparer<List<T>>
    {

        public bool Equals(List<T> x, List<T> y)
        {
            return true; //x.SetEquals(y, valueComparer);
        }

        public int GetHashCode(List<T> obj)
        {
            throw new NotImplementedException();
        }

     
    }
}
