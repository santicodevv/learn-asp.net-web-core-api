# My Learning in ASP.NET Core Web API

## 📚 Task Manager API

Hoy me tocó poner en práctica la creación de una API, aplicando todo lo aprendido.

### 🧠 Conceptos aplicados

1. Data Annotations
2. Query String Optional
3. Route Constraints
4. Validaciones Personalizadas
5. DTOs
6. Mapping

---

## 🔗 Endpoints Disponibles

```http
GET /api/task
```
Obtiene todas las tareas con paginación.

```http
GET /api/task/{id}
```
Obtiene la tarea específica por su id.

```http
GET /api/task/search?completed=false
```
Nos devuelve todas las tareas filtradas, por la condición que le pasemos (`true` / `false`).

```http
POST /api/task
```
Para crear la tarea.

```http
PUT /api/task/{id}
```
Para actualizar la tarea.

---

## 🛣️ Route Constraints

Si entramos al Controller de la task, vemos el método `GetById`, donde utilizamos los Route Constraints indicándole que lo único que va a entrar por ahí serán números. Si enviamos algún alfanumérico, bool, etc., nos va a dar un error 400 automáticamente, porque solo deben ingresar enteros.

---

## 🔄 Mapping (Model ↔ DTO)

Dentro de varios métodos vemos el `MapToDto`. Tenemos un método que lo que hace es mapear un Model a un Model DTO, que es lo que se va a mandar al cliente. Esto es para no mandar datos sensibles.

Funciona así: **Model → DTO**

Le pasamos como argumento la instancia del Model, y eso nos devuelve una nueva instancia pero en DTO (que no es el Model en sí). Así nos mapea, que es lo que llamamos `Mapping`: nos convierte de **Model → DTO** y viceversa **DTO → Model**.

---

## ✅ Data Annotations

Si entramos al `CreateTaskDto`, vemos que estamos usando Data Annotations para agregarle instrucciones a los atributos. Pero son reglas, literalmente su ejecución se encarga de hacerla automáticamente el `[ApiController]` del Controller de Task.

Son reglas básicas para indicarle que algún campo es requerido, o que tiene que ser un rango de número de un inicio y un fin, etc.

Si no se cumplen esas reglas, devuelve un error **400 Bad Request** con las indicaciones de qué campo falta por completar.

---

## 🔧 Validaciones Personalizadas con `IValidatableObject`

Pero también tenemos otras validaciones que son las **personalizadas**, que se encargan de hacer validaciones más avanzadas que las Data Annotations no pueden hacer. Tipo comparar 2 campos, etc.

Porque las Data Annotations hacen cosas sencillas; para validaciones más avanzadas usamos las validaciones personalizadas con `IValidatableObject`.

Le indicamos que ese Model va a tener validaciones personalizadas y debe tener el método `Validate`. Lo demás se encarga C# internamente.

### El `yield return`

También usamos el `yield return`. Lo que C# hace es: como ve que se va a retornar una colección `IEnumerable`, ejecuta el método internamente.

Cuando comienza la ejecución, entra al `Validate` y verifica la condición. Si alguna se cumple, el `yield return` se encarga de agregarse a la colección que tiene C# creada internamente (la que él maneja).

Esto nos evita crearla nosotros manualmente e ir agregando 1 a 1, y después retornarla.

Si una condición se cumple y se agrega, se para en esa condición. Cuando se tenga que validar otra, sigue ejecutándose sin terminar el método como lo hace el `return` normal.

---

## ❓ Query String Optional

En el `GetAll`, manejamos la query string optional. Le estamos agregando parámetros con valores por defecto a la función, diciendo que eso se va a recibir como query string.

> **Nota:** Si tenemos un placeholder (digamos `{id}`), no se puede poner parámetro por defecto.

---

## 📝 Resumen

| Concepto | Aprendizaje |
|----------|-------------|
| Route Constraints | Validan el tipo de dato en la URL, devuelve 400 si no cumple |
| Mapping | Convierte Model ↔ DTO para no exponer datos sensibles |
| Data Annotations | Reglas básicas que `[ApiController]` valida automáticamente |
| `IValidatableObject` | Para validaciones avanzadas que las Data Annotations no pueden hacer |
| `yield return` | Agrega elementos a una colección sin crearla manualmente |
| Query String Optional | Parámetros con valores por defecto en el método |

---

Ese fue mi aprendizaje aplicado.