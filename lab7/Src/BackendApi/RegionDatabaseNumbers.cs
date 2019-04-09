using System.Collections.Generic;

namespace BackendApi
{
    public class RegionDatabaseNumbers
    {
        public int Ru {get; set;}
        public int Eu {get; set;}
        public int Usa {get; set;}

        public IEnumerable<int> GetNumbers()
        {
            yield return Ru;
            yield return Eu;
            yield return Usa;
            yield break;
        }
    }
}