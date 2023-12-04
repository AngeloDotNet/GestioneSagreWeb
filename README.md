# Gestione Sagre Web

Open source project that was born with the idea of wanting to create free software that allows the management of festivals, events or shows where it is necessary to check orders for the kitchen and/or bar

## Features

- No server required
- Portable program
- Distributable via docker
- Cross platform

## Tech Stack

- **Client:** The source code is available by [clicking here](https://github.com/AngeloDotNet/GestioneSagreWeb.Client)
- **Server:** Microservices Web API, .NET 6
- **Database:** SQLServer 2022 CU10 ubuntu 20.04
- **Graphics:** MudBlazor 6.11.1
- **Logging:** Serilog with SEQ (Datalust) 2023.4

## Related projects

| [Gestione Sagre Web Infrastructure](https://github.com/AngeloDotNet/GestioneSagreWeb.Infrastructure) | [Gestione Sagre Web Tools](https://github.com/AngeloDotNet/GestioneSagreWeb.Tools) |
| :---------------------------------------------------------------------------------------------: | :--------------------------------------------------------------------------------: |

<!--
## Apply migrations

1. Add-Migration MIGRATION -Project GestioneSagre.PROJECT.DataAccessLayer
2. Update-Database -Project GestioneSagre.PROJECT.DataAccessLayer
3. Script-Migration -o script-PROJECT.sql (Generate the migration sql script)

- Example MIGRATION: **InitialMigration** and PROJECT: **Utility**
-->

## Demo (DEV)

Environment Web: http://sagre-web.aepserver.it/

Environment Monitoring: https://sagre-monitor.aepserver.it/health-ui

## Support

If you found this Implementation helpful or used it in your Projects, do give it a ⭐ (or follow me) on Github. Thanks!

## Contributing

Contributions and/or suggestions are always welcome.

## Badges

[![CodeQL](https://github.com/AngeloDotNet/GestioneSagreWeb/actions/workflows/codeql.yml/badge.svg)](https://github.com/AngeloDotNet/GestioneSagreWeb/actions/workflows/codeql.yml)
[![.NET Build and Test](https://github.com/AngeloDotNet/GestioneSagreWeb/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/AngeloDotNet/GestioneSagreWeb/actions/workflows/dotnet.yml)
[![Lint Code Base](https://github.com/AngeloDotNet/GestioneSagreWeb/actions/workflows/linter.yml/badge.svg)](https://github.com/AngeloDotNet/GestioneSagreWeb/actions/workflows/linter.yml)
[![Push DockerHub](https://github.com/AngeloDotNet/GestioneSagreWeb/actions/workflows/docker-publish.yml/badge.svg)](https://github.com/AngeloDotNet/GestioneSagreWeb/actions/workflows/docker-publish.yml)

## License

![GitHub](https://img.shields.io/github/license/angelodotnet/gestionesagreweb?style=for-the-badge)
