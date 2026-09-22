# My Learning in ASP.NET Core Web API

## 📚 Lo aprendido hoy

Hoy aprendí sobre los conceptos básicos que están detrás de un **backend** y, específicamente, de una **API Web**.

En mi primer proyecto, llamado **MyFirstAPI**, puse en práctica estos conocimientos desarrollando mi primera API: una **BookStore API**.

En esta API tenemos varios **endpoints (URLs)** que permiten realizar diferentes operaciones sobre los libros.

### 🔗 Endpoints disponibles

```http
GET /api/book
```

Obtiene todos los libros disponibles en la lista.

```http
GET /api/book/{id}
```

Obtiene un libro específico utilizando su `id`.

```http
GET /api/book/test
```

Obtiene solamente los libros que están disponibles.

---

## 🌐 HTTP y códigos de estado

También aprendí sobre **HTTP** y algunos de los códigos de estado que se utilizan para indicar el resultado de una petición.

Algunos códigos importantes son:

* **200 OK** → La petición se realizó correctamente.
* **201 Created** → Se creó un nuevo recurso correctamente.
* **404 Not Found** → El recurso solicitado no fue encontrado.
* **500 Internal Server Error** → Ocurrió un error en el servidor.

En este proyecto principalmente utilicé:

* **200 OK** → Cuando la petición fue exitosa y se encontraron resultados.
* **404 Not Found** → Cuando no se encontró el recurso solicitado.

---

## ⚙️ `[ApiController]`

También aprendí sobre el atributo:

```csharp
[ApiController]
```

Este atributo activa comportamientos específicos para las APIs de ASP.NET Core.

Entre ellos:

* Realiza **validaciones automáticas del modelo**.
* Puede devolver automáticamente una respuesta **400 Bad Request** cuando los datos recibidos no son válidos.
* Permite la **inferencia del model binding**, ayudando a ASP.NET Core a determinar de dónde debe obtener los datos de los parámetros de una acción.

> Nota: `[ApiController]` no devuelve automáticamente errores `500`. Los errores `500` normalmente indican un error no controlado en el servidor.

---

## 🧠 Conceptos que practiqué

Durante este proyecto practiqué principalmente:

* Qué es una **API Web**.
* Qué es un **endpoint**.
* Métodos HTTP, principalmente `GET`.
* Códigos de estado HTTP.
* Uso de `[ApiController]`.
* Model Binding e **inferencia de binding**.
* Creación de controladores.
* Creación de rutas/endpoints.
* Obtención de información mediante peticiones HTTP.

## 🚀 Proyecto

**Nombre:** `MyFirstAPI`

**Tipo:** BookStore API

El objetivo principal del proyecto fue poner en práctica los conceptos básicos de **ASP.NET Core Web API** y comprender cómo funciona una API desde el lado del backend.

---

## 📚 POST, PUT e Inferencia de Binding

### 🔗 Nuevos Endpoints

#### Crear un libro (POST)

```http
POST /api/book
```

Este endpoint permite crear un nuevo libro enviando los datos en el **body** de la petición.

```csharp
[HttpPost]
public ActionResult<Book> Create(Book newBook)
{
    newBook.Id = _books.Count + 1;
    _books.Add(newBook);

    return CreatedAtAction(nameof(GetById), new { Id = newBook.Id }, newBook);
}
```

Aquí la inferencia funciona automáticamente: como `Book` es un tipo complejo, ASP.NET Core infiere que viene del **body** (`[FromBody]`).

#### Actualizar precio de un libro (PUT)

```http
PUT /api/book/{id}/price
```

Este endpoint permite actualizar **solo el precio** de un libro.

```csharp
[HttpPut("{id}/price")]
public ActionResult<Book> Update(int id, [FromBody] decimal price)
{
    var findProduct = _books.FirstOrDefault(b => b.Id == id);
    if (findProduct is null)
        return NotFound();

    findProduct.Price = price;

    return Ok(findProduct);
}
```

---

### 🧠 Inferencia de Binding: ¿Cuándo funciona automáticamente?

El atributo `[ApiController]` activa la inferencia automática de binding:

| Tipo de parámetro | Inferencia automática |
|-------------------|----------------------|
| Tipo complejo (clases como `Book`) | `[FromBody]` |
| Tipo simple (`int`, `string`, `decimal`) | `[FromQuery]` |
| Parámetro en la ruta `{id}` | `[FromRoute]` |

---

### ❌ El problema que tuve

La tarea era actualizar **solo el precio** de un libro. Mi primer intento fue enviar el objeto `Book` completo, pensando que la inferencia detectaría automáticamente el `[FromBody]`.

**Pero la tarea no era esa.** Solo debía recibir el precio como parámetro.

#### El problema

Cuando usé `decimal price` sin especificar de dónde viene, ASP.NET Core lo infería como `[FromQuery]`:

```csharp
// ASP.NET Core infiere: [FromQuery] decimal price
public ActionResult<Book> Update(int id, decimal price)
```

Esto significaba que el precio se enviaría así:

```
PUT /api/book/1/price?price=29.99
```

#### ¿Por qué esto no es buena idea?

Enviar datos por **query string** para actualizar un recurso no es recomendable porque:

- Los datos quedan visibles en la URL
- Se guardan en logs del servidor
- Se guardan en el historial del navegador
- No es semánticamente correcto para operaciones de actualización

---

### ✅ La solución

Especificar explícitamente `[FromBody]` para indicar que el precio viene en el body:

```csharp
[HttpPut("{id}/price")]
public ActionResult<Book> Update(int id, [FromBody] decimal price)
```

Ahora el precio se envía en el **body** de la petición:

```json
29.99
```

---

### 📝 Resumen de Inferencia

| Concepto | Aprendizaje |
|----------|-------------|
| `[FromBody]` en tipos simples | No se infiere automáticamente, hay que especificarlo |
| `[FromBody]` en tipos complejos | Se infiere automáticamente |
| Query string para actualizar | No es buena práctica |
| Body para actualizar | Es la forma correcta |

---

## 🛣️ Route Constraints y Query Strings Opcionales

### 🔒 Route Constraints (Restricciones de Ruta)

Las restricciones de ruta permiten validar el **tipo de dato** que recibe un placeholder en la URL. Si el dato no cumple con la restricción, ASP.NET Core automáticamente devuelve un **404 Not Found** sin necesidad de validar manualmente.

#### Nuevo Endpoint: Filtrar libros por rango de precio

```http
GET /api/book/price/{minPrice}/{maxPrice}
```

```csharp
[HttpGet("price/{minPrice:decimal}/{maxPrice:decimal}")]
public ActionResult<List<Book>> GetBooksByMinMaxPrice(decimal minPrice, decimal maxPrice)
{
    var result = _books.Where(b => b.Price >= minPrice && b.Price <= maxPrice)
                    .ToList();

    if (result.Count == 0)
        return NotFound();

    return Ok(result);
}
```

#### ¿Qué hace `:decimal`?

El constraint `:decimal` indica que el parámetro **debe ser un número decimal**. Si alguien intenta acceder con un valor inválido:

```
GET /api/book/price/abc/xyz
```

ASP.NET Core devuelve automáticamente **404 Not Found** sin ejecutar el método.

#### Tipos de constraints disponibles

| Constraint | Descripción | Ejemplo |
|------------|-------------|---------|
| `:int` | Debe ser un entero | `{id:int}` |
| `:decimal` | Debe ser un decimal | `{price:decimal}` |
| `:bool` | Debe ser true/false | `{active:bool}` |
| `:guid` | Debe ser un GUID válido | `{id:guid}` |
| `:minlength(n)` | Mínimo n caracteres | `{name:minlength(3)}` |
| `:maxlength(n)` | Máximo n caracteres | `{name:maxlength(50)}` |
| `:min(n)` | Valor mínimo | `{age:min(18)}` |
| `:max(n)` | Valor máximo | `{age:max(100)}` |

---

### ❓ Query Strings Opcionales con Valores por Defecto

Podemos hacer que los parámetros de query string sean **opcionales** asignándoles un valor por defecto directamente en el método.

#### Endpoint actualizado: Paginación

```http
GET /api/book?page=1&pageSize=10
```

```csharp
[HttpGet]
public ActionResult<List<Book>> GetAll(int page = 1, int pageSize = 10)
{
    var result = _books.Skip((page - 1) * pageSize)
                 .Take(pageSize).ToList();

    return Ok(result);
}
```

#### ¿Cómo funciona?

- Si no se envía `page`, usa el valor **1** por defecto
- Si no se envía `pageSize`, usa el valor **10** por defecto
- Esto permite llamar al endpoint de diferentes formas:

```http
GET /api/book                     → page=1, pageSize=10
GET /api/book?page=2              → page=2, pageSize=10
GET /api/book?pageSize=5          → page=1, pageSize=5
GET /api/book?page=3&pageSize=20  → page=3, pageSize=20
```

---

### 📝 Resumen

| Concepto | Aprendizaje |
|----------|-------------|
| Route Constraints | Validan el tipo de dato en la URL automáticamente |
| `:decimal`, `:int`, etc. | Restricciones de tipo que devuelven 404 si no se cumplen |
| Parámetros opcionales | Se definen con `= valorDefault` en el método |
| Paginación | Se implementa con `Skip()` y `Take()` de LINQ |

---

## ✅ Validaciones con Data Annotations

Después modifiqué algunos endpoints para recibir datos más específicos y agregué validaciones usando **Data Annotations**.

Las Data Annotations son atributos que se colocan sobre las propiedades de una clase para declarar las reglas que deben cumplir. No tienen magia: simplemente describen las condiciones que queremos aplicar a nuestros datos.

Por ejemplo, el DTO para crear un libro tiene estas reglas:

```csharp
public class CreateBookDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = "";

    [MaxLength(100)]
    public string? Author { get; set; }

    [Range(0.1, 100000)]
    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }
}
```

En este caso:

* `[Required]` indica que `Title` es obligatorio.
* `[MaxLength(100)]` indica que el texto no puede superar los 100 caracteres.
* `[Range(0.1, 100000)]` indica que `Price` debe estar dentro de ese rango.

### ¿Qué hace `[ApiController]` con estas reglas?

El atributo `[ApiController]` activa la validación automática del modelo. Cuando ASP.NET Core recibe una petición, comprueba si los datos cumplen las reglas definidas en las propiedades.

Si los datos no son válidos, la acción no se ejecuta y ASP.NET Core responde automáticamente con `400 Bad Request`, incluyendo información sobre los errores de validación. De esta manera no tenemos que comprobar manualmente cada propiedad dentro del endpoint.

> Las Data Annotations declaran las reglas y `[ApiController]` hace que ASP.NET Core valide automáticamente esas reglas al recibir la petición.

---

## 📦 DTOs: enviar y recibir solo los datos necesarios

Un **DTO** (*Data Transfer Object*) es un objeto sencillo que define los datos que queremos recibir o enviar a través de la API.

Podemos verlo como un recorte del modelo original: elegimos únicamente las propiedades que necesitamos exponer en una operación concreta. Esto evita enviar información innecesaria y permite controlar mejor los datos que entran y salen de la API.

### DTO para crear un libro

El modelo `Book` tiene un `Id`, pero al crear un libro no necesitamos recibirlo porque el sistema lo genera automáticamente. Por eso el endpoint `POST /api/book` recibe un `CreateBookDto`:

```csharp
[HttpPost]
public ActionResult<BookDto> Create(CreateBookDto newBook)
{
    var book = MapToModel(newBook);
    _books.Add(book);

    var dto = MapToDto(book);

    return CreatedAtAction(nameof(GetById), new { Id = dto.Id }, dto);
}
```

El `CreateBookDto` solo recibe los datos necesarios para crear el libro:

* `Title`
* `Author`
* `Price`
* `IsAvailable`

El `Id` no se recibe desde el cliente. En `MapToModel`, la API lo asigna internamente:

```csharp
Id = _books.Count + 1
```

Después de crear el libro, la API devuelve un `BookDto`. Este DTO sí incluye el `Id`, porque ahora forma parte de la información que podemos mostrar al cliente.

### ¿Por qué usar DTOs?

Los DTOs permiten:

* Recibir únicamente los campos necesarios para una operación.
* Evitar que el cliente envíe propiedades que debe controlar el sistema, como el `Id`.
* No exponer propiedades internas del modelo.
* Aplicar validaciones específicas para cada caso de uso.
* Mantener separados los modelos internos de los datos que viajan por la API.

En resumen, los DTOs hacen que los métodos trabajen con los datos exactos que nosotros decidimos recibir o enviar, en lugar de aceptar o devolver siempre el modelo completo.

### 📝 Resumen de validaciones y DTOs

| Concepto | Aprendizaje |
|----------|-------------|
| Data Annotations | Atributos que declaran reglas para las propiedades |
| `[ApiController]` | Activa la validación automática del modelo |
| `400 Bad Request` | Respuesta automática cuando los datos no cumplen las reglas |
| `CreateBookDto` | Define los datos permitidos al crear un libro |
| `BookDto` | Define los datos que la API devuelve al cliente |
| `Id` | Lo genera el sistema y no se recibe al crear el libro |


