using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojUpravljanjaSBazomPodataka.Sucelja
{
    public interface IRepozitori<T> : IDisposable where T : class
    {
        IQueryable<T> DajSve();
        int Dodaj(T entitet);
        int Azuriraj(T entitet);
        int Izbrisi(T entitet);
    }
}
