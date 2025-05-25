using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Puertos.Entrada
{
    public interface ICommonServices<Tentity>
    {
        Task Add(Tentity entity);
        Task<Tentity> Get(int idEntity);
        Task <List<Tentity>> GetAll();
        Task Update(Tentity entity);
        Task Delete(int idEntity);

    }
}
