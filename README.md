# Convenios

![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-black?style=for-the-badge&logo=JSON%20web%20tokens)
[![Despliegue Pipeline](https://github.com/Sds-Dti/eFirma_BackEnd/actions/workflows/Pipeline.yml/badge.svg)](https://github.com/Sds-Dti/eFirma_BackEnd/actions/workflows/Pipeline.yml)

En N3XUS TECH INC, buscamos la innovación tecnológica y la excelencia en soluciones. Nuestra visión es construir una plataforma globalmente reconocida donde los programadores  puedan presentar sus códigos verificados y donde los innovadores tecnológicos encuentren respuestas confiables para sus desafíos en nuestra aplicación web N3XUS TECH.

### Para hacer un Scaffold

```
Scaffold-DbContext "Server=localhost;DataBase=fisiolabs_sofwaredb;User=root;Password=admin;Port=3306;" MySql.EntityFrameworkCore
```

### Crear las migraciones

**.NetCore CLI**

Comando para installar .NetCore CLI

```bash
dotnet tool install --global dotnet-ef
```

Para crear una nueva migración, el comando sugerido es el siguiente:

```bash
dotnet ef migrations add "MigracionInicial" --project Core --startup-project Presentation --output-dir Infraestructure\Persistance\Migrations
```

**Visual Studio**

---

```bash
Add-Migration "MigracionInicial" --project Core --startup-project Presentation --output-dir Infraestructure\Persistance\Migrations
```

Donde:

- `--project Core`: el proyecto que contiene el `dbcontext`.

- `--startup-project Presentation`: proyecto inicial de ejecución.

- `--output-dir Infraestructure\Persistance\Migrations`:  el directorio de destino que almacenará las migraciones.

### Aplicar las migraciones a la base de datos

En caso de que se quiera probar una versión en local, aplicar un nuevo parche o se migre la base de datos a otro servidor, el comando sugerido sería el siguiente:

**.NetCore CLI**
```bash
dotnet ef database update --project Core --startup-project Presentation
```

---

**Visual Studio**

```bash
Update-Database -Project Core -StartupProject Presentation
```

Donde:

- `--project Core`: se conoce como proyecto de destino porque es donde los comandos añaden o eliminan archivos. Por defecto, el proyecto en el directorio actual es el proyecto de destino. Puede especificar un proyecto diferente como proyecto de destino utilizando la opción `--project`.

- `--startup-project Presentation` : el proyecto de inicio es el que las herramientas construyen y ejecutan. Las herramientas tienen que ejecutar código de aplicación en tiempo de diseño para obtener información sobre el proyecto, como la cadena de conexión a la base de datos y la configuración del modelo. Por defecto, el proyecto en el directorio actual es el proyecto de inicio. Puede especificar un proyecto diferente como proyecto de inicio utilizando la opción `--startup-project`.
