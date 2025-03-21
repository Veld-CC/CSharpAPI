# AdventureWorks API - .NET 8, C#, Repository Pattern

## Descripción

Esta API está desarrollada en **.NET 8** utilizando **C#** como lenguaje de programación. Se conecta a la base de datos **Microsoft AdventureWorks 2019**, una base de datos de ejemplo ampliamente utilizada para demostrar el uso de SQL Server en aplicaciones empresariales. La API sigue el **patrón de diseño Repository** para separar la lógica de acceso a datos de la lógica de negocio, lo que facilita la mantenibilidad, escalabilidad y testabilidad del código.

## Características principales

- **.NET 8**: Utiliza la última versión de .NET, aprovechando las mejoras de rendimiento y las nuevas características del framework.
- **Repository Pattern**: Implementa el patrón Repository para abstraer la lógica de acceso a datos, permitiendo un código más limpio y modular.
- **AdventureWorks 2019**: Se conecta a la base de datos AdventureWorks 2019, que contiene datos de ejemplo relacionados con ventas, productos, clientes y empleados.
- **Entity Framework Core**: Utiliza Entity Framework Core como ORM (Object-Relational Mapper) para interactuar con la base de datos de manera eficiente.
- **Swagger**: Integra Swagger para documentar y probar los endpoints de la API de manera interactiva.
- **Dependency Injection**: Aprovecha la inyección de dependencias para gestionar las instancias de los repositorios y servicios.

## Estructura del Proyecto

- **Controllers**: Contiene los controladores de la API que manejan las solicitudes HTTP.
- **Repositories**: Implementa el patrón Repository, encapsulando la lógica de acceso a datos.
- **Services**: Contiene la lógica de negocio que interactúa con los repositorios.
- **Models**: Define las entidades y DTOs (Data Transfer Objects) utilizados en la API.
- **Data**: Configura el contexto de la base de datos y las migraciones de Entity Framework Core.


## Requisitos

- **.NET 8 SDK**: Asegúrate de tener instalado el SDK de .NET 8.
- **SQL Server**: Necesitarás una instancia de SQL Server con la base de datos AdventureWorks 2019.
- **Visual Studio 2022 o Visual Studio Code**: Recomendado para el desarrollo y depuración.

## Configuración

1. Clona el repositorio.
2. Configura la cadena de conexión a la base de datos en el archivo `appsettings.json`.
