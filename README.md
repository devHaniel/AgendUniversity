# AgendUniversity

## 1. ¿Qué es AgendUniversity?

AgendUniversity es una aplicación simple diseñada para estudiantes universitarios que necesitan registrar y organizar su vida académica. Su propósito es resolver el problema de la falta de control sobre los periodos cursados, las asignaturas tomadas y las tareas realizadas durante la carrera.

En muchas ocasiones, los estudiantes acumulan información en notas dispersas o en múltiples herramientas sin una estructura clara. AgendUniversity ofrece una forma centralizada de:
- guardar periodos académicos (semestres o ciclos),
- registrar asignaturas cursadas,
- asociar tareas y entregas a cada asignatura,
- mantener un historial de la experiencia universitaria.

Este enfoque facilita la revisión de qué se cursó en cada etapa, ayuda a planificar futuras materias y mejora el seguimiento del desempeño académico.

## 2. Detalles del proyecto

La aplicación está compuesta por dos partes:
- `BackEnd/`: API REST construida con ASP.NET Core.
- `FrontEnd/`: cliente que consume la API.

### Funcionalidades principales

- CRUD para `Periodo`, `Asignatura`, `Tarea` y `Usuario`.
- Autenticación y autorización básica.
- Registro y consulta de tareas por periodo y asignatura.
- Manejo de usuarios para que cada estudiante pueda gestionar su propio historial.

### Estructura del repositorio

- `BackEnd/`: proyecto backend en ASP.NET Core.
- `FrontEnd/`: proyecto frontend para consumir la API.
- `Dtos/`: objetos de transferencia de datos.
- `Models/`: entidades del dominio.
- `Persistencia/`: contexto de base de datos.
- `Repository/`: acceso a datos.
- `Service/`: lógica de negocio.
- `EndPoints/`: definición de rutas de la API.
- `Migrations/`: migraciones de Entity Framework.

### Instrucciones de uso

1. Abrir el proyecto en Visual Studio o VS Code.
2. Restaurar paquetes NuGet y dependencias.
3. Ejecutar las migraciones y actualizar la base de datos si aplica.
4. Ejecutar el backend desde `BackEnd/`.
5. Ejecutar el frontend desde `FrontEnd/`.

### Seguridad

El repositorio incluye un `.gitignore` para evitar el versionado de archivos que contienen claves y cadenas de conexión, especialmente:
- `appsettings.json`
- `appsettings.Development.json`

### Escalabilidad

El diseño actual permite escalar el proyecto de forma sencilla:
- la lógica de negocio está separada en servicios,
- el acceso a datos está aislado en repositorios,
- los endpoints se pueden extender sin afectar la estructura principal,
- la base de datos puede crecer mediante migraciones de Entity Framework,
- se puede añadir fácilmente soporte para nuevas entidades como calificaciones, profesores o horarios.

