# ClinicaWeb ASP.NET

Este proyecto es un sistema web para la gestión de una clínica, desarrollado en ASP.NET Core MVC con conexión a base de datos MySQL. Permite la administración de pacientes y muestra información pública de la clínica.

## Características principales

- Página de inicio
- Información de la clínica
- Sección de especialidades médicas
- Página de contacto
- Barra de navegación responsiva
- Diseño moderno con Bootstrap y CSS personalizado
- Panel de administración protegido por login
- CRUD completo de pacientes
- Validaciones básicas en formularios
- Restricción de acceso a la gestión de pacientes

## Requisitos
- .NET 10.0 SDK o superior
- MySQL Server (recomendado usar MySQL Workbench para gestión visual)
- Visual Studio Code o Visual Studio

## configuración

**Configura la base de datos**
   - Crea una base de datos en MySQL llamada `clinica_db`.
   - Ajusta la cadena de conexión en `appsettings.json` si es necesario:
     ```json
     "DefaultConnection": "server=127.0.0.1;database=clinica_db;user=TU_USUARIO;password=TU_PASSWORD;port=3306;"

## Script SQL de la base de datos

Puedes crear y poblar la base de datos ejecutando el siguiente script en MySQL Workbench o tu cliente favorito:

```sql
CREATE DATABASE clinica_db;
USE clinica_db;

CREATE TABLE Pacientes (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreCompleto VARCHAR(100) NOT NULL,
    Run VARCHAR(20) NOT NULL,
    Edad INT,
    Telefono VARCHAR(20),
    Direccion VARCHAR(150),
    Email VARCHAR(150),
    Diagnostico VARCHAR(200)
);

INSERT INTO Pacientes (NombreCompleto, Run, Edad, Telefono, Direccion, Email, Diagnostico) VALUES
('Pablo Perez', '12345678-9', 24, '912345678', 'Calle 1 #123', 'pablo.perez@gmail.com', 'Sin diagnóstico'),
('María González', '23456789-0', 21, '922345678', 'Calle 2 #234', 'maria.gonzalez@gmail.com', 'Sin diagnóstico'),
('Andrés Muñoz', '34567890-1', 23, '932345678', 'Calle 3 #345', 'andres.munoz@gmail.com', 'Sin diagnóstico');

CREATE TABLE Admins (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Usuario VARCHAR(50) NOT NULL,
    Clave VARCHAR(100) NOT NULL
);

INSERT INTO Admins (Usuario, Clave) VALUES
('admin', 'admin123');

select * from Pacientes;
select * from Admins;
```

## Uso del sistema

- **Páginas públicas:** Inicio, Clínica, Especialidades y Contacto están disponibles para cualquier visitante.
- **Panel de Pacientes:** Solo visible y accesible tras iniciar sesión como administrador.
- **Login:** Usa el botón "Iniciar sesión" en la barra de navegación. Tras autenticarte, verás el enlace "Pacientes" y podrás gestionar los registros.
- **Cerrar sesión:** Usa el botón "Cerrar sesión" en la barra de navegación.

## Funcionamiento de cada pantalla

### Página de Inicio
- Muestra una bienvenida y presentación de la clínica.
- Accesible para cualquier visitante.

### Información de la Clínica
- Describe la misión, visión y datos relevantes de la clínica.
- Accesible para todos.

### Especialidades
- Lista las especialidades o servicios médicos que ofrece la clínica.
- Accesible para todos.

### Contacto
- Muestra información de contacto (dirección, teléfono, correo) y un formulario de contacto.
- Accesible para todos.

### Login (Iniciar sesión)
- Accesible desde el botón "Iniciar sesión" en la barra de navegación.
- Solo los administradores pueden iniciar sesión.
- Al iniciar sesión correctamente, aparecen los enlaces "Pacientes" y "Cerrar sesión" en la barra.

### CRUD de Pacientes (Panel de Administración)
- Solo accesible para administradores autenticados.
- Permite:
  - **Crear:** Agregar un nuevo paciente mediante un formulario.
  - **Leer:** Ver el listado de todos los pacientes registrados.
  - **Actualizar:** Editar los datos de un paciente existente.
  - **Eliminar:** Borrar un paciente de la base de datos.
- Incluye validaciones básicas en los formularios.
- El acceso directo a esta sección está protegido: si no has iniciado sesión, serás redirigido al login.

### Cierre de sesión
- El botón "Cerrar sesión" aparece solo para el administrador autenticado.
- Al cerrar sesión, se ocultan los enlaces de administración y vuelves a la navegación pública.

## Explicación técnica de cada pantalla y funcionalidad

### Página de Inicio (`HomeController`, `Views/Home/Index.cshtml`)
- Controlador: Renderiza la vista principal con información general.
- Vista: HTML y Razor, muestra bienvenida y enlaces a otras secciones.

### Información de la Clínica (`HomeController`, `Views/Home/Clinica.cshtml`)
- Controlador: Acción `Clinica` retorna la vista con información estática.
- Vista: Contenido sobre la clínica, misión, visión, etc.

### Especialidades (`HomeController`, `Views/Home/Especialidades.cshtml`)
- Controlador: Acción `Especialidades` retorna la vista con la lista de servicios.
- Vista: Lista de especialidades médicas.

### Contacto (`HomeController`, `Views/Home/Contacto.cshtml`)
- Controlador: Acción `Contacto` retorna la vista con datos de contacto.
- Vista: Muestra dirección, teléfono, correo y puede incluir un formulario.

### Login (Iniciar sesión) (`AccountController`, `Views/Account/Login.cshtml`)
- Controlador: 
  - `GET Login`: Muestra el formulario de login.
  - `POST Login`: Valida usuario y clave contra la tabla `Admins` en la base de datos.
    - Si es correcto, guarda la sesión (`Session["Admin"]`) y redirige al panel.
    - Si es incorrecto, muestra mensaje de error.
- Vista: Formulario estilizado con Bootstrap y validaciones HTML.

### Barra de navegación (`Views/Shared/_Layout.cshtml`)
- Usa Razor para mostrar enlaces públicos a todos y enlaces de administración solo si hay sesión de admin.
- El botón "Iniciar sesión" aparece solo si no hay sesión; "Pacientes" y "Cerrar sesión" solo si el admin está autenticado.

### CRUD de Pacientes (`PacientesController`, `Views/Pacientes/`)
- Controlador:
  - `Index`: Lista todos los pacientes (requiere sesión admin).
  - `Create` (GET/POST): Muestra y procesa el formulario para agregar pacientes.
  - `Edit` (GET/POST): Permite modificar datos de un paciente existente.
  - `Delete` (GET/POST): Confirma y elimina un paciente.
  - Todas las acciones están protegidas: si no hay sesión de admin, redirige a login.
- Vistas:
  - `Index.cshtml`: Tabla con todos los pacientes y botones para editar/eliminar.
  - `Create.cshtml`, `Edit.cshtml`: Formularios con validaciones Razor y Bootstrap.
  - `Delete.cshtml`: Confirma la eliminación.

### Validaciones
- En el modelo `Paciente` se usan atributos como `[Required]`, `[EmailAddress]`, etc., para validar datos en el servidor y en el cliente.
- Los formularios muestran mensajes de error si los datos no cumplen las reglas.

### Seguridad y Restricción de Acceso
- El middleware en `Program.cs` restringe el acceso a `/Pacientes/*` y `/Account/Logout` solo a usuarios autenticados.
- El resto del sitio es público.
- Si intentas acceder al panel sin sesión, eres redirigido al login.

### Cierre de sesión (`AccountController`, `Logout`)
- Elimina la variable de sesión y redirige al inicio.
- El menú de administración desaparece automáticamente.

---

Cada sección está separada en controladores y vistas, siguiendo el patrón MVC de ASP.NET Core. El acceso a datos se realiza mediante Entity Framework Core y el contexto `ClinicaContext`.

## Trabajo colaborativo
- Utiliza GitHub para control de versiones.
- Realiza commits frecuentes y claros.
- Mantén la organización del repositorio.

## Créditos
Desarrollado por Ian Barria y Tomas Quintana.

Docente: Francisco Calfún Gutiérrez

## Validaciones básicas al agregar o editar pacientes

- **Nombre completo:** Obligatorio (campo requerido).
- **RUN:** Obligatorio (campo requerido).
- **Edad:** Debe ser un número entre 0 y 120.
- **Teléfono:** Opcional, pero puede validarse formato en el futuro.
- **Email:** Debe tener formato de correo electrónico válido si se ingresa.
- **Dirección:** Opcional.
- **Diagnóstico:** Opcional.

**En el código:**
- En el modelo `Paciente` se usan los atributos `[Required]`, `[Range(0,120)]`, `[EmailAddress]` para validar en el servidor.
- En los formularios de la vista (crear y editar) se usan los atributos HTML `required`, `type="number"`, `min`, `max`, y `type="email"` para validación en el cliente.
- Si los datos no cumplen las reglas, el formulario no se envía y/o el servidor rechaza la petición mostrando mensajes de error.

**Ejemplo de validación en el modelo:**
```csharp
public class Paciente
{
    public int Id { get; set; }
    [Required] public string NombreCompleto { get; set; } = "";
    [Required] public string Run { get; set; } = "";
    [Range(0, 120)] public int Edad { get; set; }
    public string Telefono { get; set; } = "";
    public string Direccion { get; set; } = "";
    [EmailAddress] public string Email { get; set; } = "";
    public string Diagnostico { get; set; } = "";
}
```

**Ejemplo de validación en el formulario (HTML):**
```html
<p><label>Nombre: <input name="NombreCompleto" required /></label></p>
<p><label>RUT: <input name="Run" required /></label></p>
<p><label>Edad: <input name="Edad" type="number" min="0" max="120" /></label></p>
<p><label>Email: <input name="Email" type="email" /></label></p>
```

Esto asegura que los datos ingresados sean correctos y completos antes de guardarlos en la base de datos.

