using System.Collections.Generic;
using System.Linq;

namespace Fuzzy_logic
{
    public class Scalies
    {
        public List<Scale> ScaleList { get; set; }

        public Scalies()
        {
            ScaleList = new List<Scale>();
        }

        public void Add(int x)
        {
            foreach (var scale in ScaleList)
            {
                if(scale.A.X <= x && x <= scale.C.X)
                    scale.AddPoint(x);
            }
        }

        public int GetPointCount()
        {
            return ScaleList.Sum(scale => scale.GetPoint().Count);
        }
    }
}
