# Vuelos-Newshore

1. Importar el proyecto Vuelos-Newshore, esta hecho en NetCore 6
2. Seleccionar la capa "Vuelos.DAL" como proyecto de inicio
3. Abrir la consola de administrador de paquetes y ejecutar este codigo EntityFrameworkCore\Update-Database, la base de datos esta configurada a localhost con la autentificacion del windows, la linea de conexion se encuentra en DAL se llama "appsettings.json" 
4. Al terminar de ejecutar la migracion, es volver a poner el proyecto de inicio a "Vuelos.WebApi"
Notas: el limite del numero de rutas lo estableci en el cod mas no lo puse en el request, por tiempo no alcance las pruebas unitarias pero si me dan un poco mas no tengo problema en hacerlas.
