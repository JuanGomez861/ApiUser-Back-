using Dominio.Entidades;
using Dominio.Puertos.Salida;
using Microsoft.EntityFrameworkCore;
using Repositorio.Contexto;


namespace Repositorio.Repositorios
{
    public class RepositorioUsuario : ICommonRepository<Usuario>
    {
        private readonly UserContext _context ;
        public RepositorioUsuario(UserContext userContext)
        {
            _context = userContext;
        }

        public async Task Add(Usuario usuario)
        {

            try
            {
                bool existeCedula = await _context.Set<Usuario>()
                .AnyAsync(u => u.Cedula == usuario.Cedula);

                if (existeCedula)
                {
                    throw new Exception();
                }

                _context.Set<Usuario>().Add(usuario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            { 
            
            }
        }

        public async Task<Usuario> Get(int id)
        {
            try
            {
                return await _context.Set<Usuario>().FirstOrDefaultAsync(u=>u.IdUsuario==id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar usuario");
            }
        }

        public async Task<List<Usuario>> GetAll()
        {
            try
            {
                return await _context.Set<Usuario>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar todos los usuario");
            }
        }

        public async Task Update(Usuario usuarioEditar)
        {
            try
            {

                var usuario = await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.IdUsuario == usuarioEditar.IdUsuario);

                if (usuario == null)
                    throw new Exception("Usuario no encontrado");

                bool cedulaDuplicada = await _context.Set<Usuario>()
                        .AnyAsync(u => u.Cedula == usuario.Cedula && u.IdUsuario != usuario.IdUsuario);

                if (cedulaDuplicada)
                    throw new Exception();

                usuario.Nombre = usuarioEditar.Nombre;
                usuario.Apellido = usuarioEditar.Apellido;
                usuario.Direccion = usuarioEditar.Direccion;
                usuario.Cedula = usuarioEditar.Cedula;
                usuario.Telefono = usuarioEditar.Telefono;
                usuario.FechaNacimiento = usuarioEditar.FechaNacimiento;

                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var usuario= await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.IdUsuario == id);

                if(usuario==null)
                    throw new Exception("Usuario no encontrado");

                _context.Set<Usuario>().Remove(usuario);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar todos los usuario");
            }
        }
    }
}
