using Dominio.Entidades;
using Dominio.Puertos.Entrada;
using Dominio.Puertos.Salida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicios
{
    public class UsuarioServicios : ICommonServices<Usuario>
    {
        private readonly ICommonRepository<Usuario> _commonRepository;
        public UsuarioServicios(ICommonRepository<Usuario> commonRepository) 
        { 
            _commonRepository = commonRepository;
        }

        public async Task Add(Usuario usuario) 
        { 
            await _commonRepository.Add(usuario);
        }
        public async Task<Usuario> Get(int idUsuario)
        {
            return await _commonRepository.Get(idUsuario);
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await _commonRepository.GetAll();
        }

        public async Task Update(Usuario usuario)
        {
            await _commonRepository.Update(usuario);
        }

        public async Task Delete(int idUsuario) 
        { 
            await _commonRepository.Delete(idUsuario);
        }

    }
}
