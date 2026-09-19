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
