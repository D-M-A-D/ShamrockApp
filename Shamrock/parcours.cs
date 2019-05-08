using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Shamrock
{
    public class course //course definition (serialized)
    {
        public string name { get; set; }
        public string TeeColor { get; set; }
        public string Yards { get; set; }
        public double sr { get; set; }
        public double cr { get; set; }
        public string comment { get; set; }
        public string url { get; set; }
        public List<hole> holes = new List<hole>();
        public List<int> pointStbl = new List<int>();
        public void addHole(int Par, int Hcp)
        {
            hole ctHole = new hole();
            ctHole.par = Par;
            ctHole.hcp = Hcp;
            ctHole.nr = holes.Count() + 1;
            holes.Add(ctHole);
        }
        public void validateDefinition()
        {
            int sumHcp = 0;
            foreach (hole ctHole in holes)
                sumHcp += ctHole.hcp;
            if(sumHcp != 171)
                throw new Exception("sum Hcps is not 171!");

        }
        public int getSumPar()
        {
            return holes.Sum(x => x.par);
        }
        public int getSumParIn()
        {
            return holes.FindAll(x => x.nr > 9).Sum(x => x.par);
        }
        public int getSumParOut()
        {
            return holes.FindAll(x => x.nr <= 9).Sum(x => x.par);
        }
        public int getSumHcp()
        {
            return holes.Sum(x => x.hcp);
        }
        public hole getHolebyNr(int nr)
        {
            hole ret = holes[nr - 1];
            if (ret.nr != nr)
                throw new Exception(String.Format("could not find hole with nr {0}, holes probably wrong sorted", nr));
            return ret;
        }
        public Double getPlayingHcp(Double hcp)
        {
            return Math.Min(36, ((sr * hcp) / 113) + cr - getSumPar());
        }
    }
    public class hole
    {
        public int nr { get; set; }
        public int par { get; set; }
        public int hcp { get; set; }
    }
}
