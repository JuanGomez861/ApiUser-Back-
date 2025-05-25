using Dominio.Entidades;
using Dominio.Servicios;
using Microsoft.EntityFrameworkCore;
using Repositorio.Contexto;
using Repositorio.Repositorios;


namespace Testing
{
    [TestClass]
    public class UsuarioServiciosTests
    {
        private UserContext _context;
        private UsuarioServicios _servicio;

        [TestInitialize]
        public void Setup()
        {
            // Configurar la base de datos en memoria
            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // nombre único por prueba
                .Options;

            _context = new UserContext(options);
            var repositorio = new RepositorioUsuario(_context);
            _servicio = new UsuarioServicios(repositorio);
        }

        [TestMethod]
        public async Task Add_Deberia_Guardar_Un_Usuario()
        {
            // Arrange
            var usuario = new Usuario
            {
                IdUsuario = 1,
                Nombre = "Juan",
                Apellido = "Pérez",
                Cedula = "123456",
                Direccion = "Calle Falsa 123",
                Telefono = "5551234",
                FechaNacimiento = DateTime.Parse("1990-01-01")
            };

            // Act
            await _servicio.Add(usuario);

            // Assert
            var usuarioGuardado = await _servicio.Get(1);
            Assert.IsNotNull(usuarioGuardado);
            Assert.AreEqual("Juan", usuarioGuardado.Nombre);
        }

        [TestMethod]
        public async Task Get_Deberia_Devolver_Usuario_Por_Id()
        {
            // Arrange
            var usuario = new Usuario
            {
                IdUsuario = 1,
                Nombre = "Laura",
                Apellido = "Salazar",
                Cedula = "4444",
                Telefono = "3500000000",
                Direccion = "Diagonal 33",
                FechaNacimiento = DateTime.Parse("1995-06-25")
            };
            await _servicio.Add(usuario);

            // Act
            var usuarioObtenido = await _servicio.Get(1);

            // Assert
            Assert.IsNotNull(usuarioObtenido);
            Assert.AreEqual("Laura", usuarioObtenido.Nombre);
            Assert.AreEqual("Salazar", usuarioObtenido.Apellido);
        }


        [TestMethod]
        public async Task GetAll_Deberia_Retornar_Usuarios()
        {
            // Arrange
            await _servicio.Add(new Usuario
            {
                IdUsuario = 1,
                Nombre = "Ana",
                Apellido = "Gómez",
                Cedula = "1111",
                Telefono = "3000000000",
                Direccion = "Calle 1",
                FechaNacimiento = DateTime.Parse("1985-05-10")
            });

            await _servicio.Add(new Usuario
            {
                IdUsuario = 2,
                Nombre = "Luis",
                Apellido = "Martínez",
                Cedula = "2222",
                Telefono = "3100000000",
                Direccion = "Carrera 2",
                FechaNacimiento = DateTime.Parse("1992-03-15")
            });

            // Act
            var usuarios = await _servicio.GetAll();

            // Assert
            Assert.AreEqual(2, usuarios.Count);
            Assert.IsTrue(usuarios.Any(u => u.Cedula == "1111"));
            Assert.IsTrue(usuarios.Any(u => u.Cedula == "2222"));
        }

        [TestMethod]
        public async Task Update_Deberia_Modificar_Los_Datos_Del_Usuario()
        {
            // Arrange
            var usuarioOriginal = new Usuario
            {
                IdUsuario = 1,
                Nombre = "Mario",
                Apellido = "López",
                Cedula = "5555",
                Telefono = "3010000000",
                Direccion = "Carrera 45",
                FechaNacimiento = DateTime.Parse("1988-09-12")
            };
            await _servicio.Add(usuarioOriginal);

            // Act
            var usuarioActualizado = new Usuario
            {
                IdUsuario = 1, // debe coincidir con el existente
                Nombre = "Mario Andrés",
                Apellido = "López Ríos",
                Cedula = "5555",
                Telefono = "3011111111",
                Direccion = "Carrera 45 #22",
                FechaNacimiento = DateTime.Parse("1988-09-12")
            };
            await _servicio.Update(usuarioActualizado);

            var usuarioDesdeBD = await _servicio.Get(1);

            // Assert
            Assert.IsNotNull(usuarioDesdeBD);
            Assert.AreEqual("Mario Andrés", usuarioDesdeBD.Nombre);
            Assert.AreEqual("López Ríos", usuarioDesdeBD.Apellido);
            Assert.AreEqual("3011111111", usuarioDesdeBD.Telefono);
            Assert.AreEqual("Carrera 45 #22", usuarioDesdeBD.Direccion);
        }



        [TestMethod]
        public async Task Delete_Deberia_Eliminar_Usuario()
        {
            // Arrange
            var usuario = new Usuario
            {
                IdUsuario = 1,
                Nombre = "Carlos",
                Apellido = "Ramírez",
                Cedula = "3333",
                Telefono = "3200000000",
                Direccion = "Avenida Siempre Viva",
                FechaNacimiento = DateTime.Parse("1990-11-20")
            };
            await _servicio.Add(usuario);

            // Act
            await _servicio.Delete(1);
            var usuarioEliminado = await _servicio.Get(1);

            // Assert
            Assert.IsNull(usuarioEliminado);
        }

    }
}
