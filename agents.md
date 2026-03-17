# Agents Guide

This file defines how coding agents should work in this monorepo.

## Project Identity

- Repository name: `cog-slop`
- Product name: `Cog Slop`
- Keep all user-facing naming and copy aligned to `Cog Slop`.
- Preserve the cog/gear pun theme across API and UI copy.

## Monorepo Layout

- API: `apps/api/CogSlop.Api`
- UI: `apps/ui`
- Database-first SQL: `database/sql`
- Solution file: `CogSlop.sln`

## Run and Build

- Build API solution:
  - `dotnet build CogSlop.sln`
- Run API:
  - `dotnet run --project apps/api/CogSlop.Api`
- UI install/start/build (PowerShell-safe):
  - `cd apps/ui`
  - `cmd /c npm install`
  - `cmd /c npm start`
  - `cmd /c npm run build`

## Database-First Workflow

- Source-of-truth create script:
  - `database/sql/001_create_cogslop.sql`
- Connection string key used by API:
  - `ConnectionStrings:CogSlopDb`
- EF scaffolding reference command:
  - `dotnet ef dbcontext scaffold "Server=(localdb)\\MSSQLLocalDB;Database=CogSlop;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --project apps/api/CogSlop.Api --context CogSlopDbContext --context-dir Data --output-dir Models/Entities --use-database-names --force`

## Authentication

- Google auth values are required in:
  - `apps/api/CogSlop.Api/appsettings.Development.json`
- Required callback URL:
  - `https://localhost:7298/signin-google`

## Guardrails for Agents

- Do not commit secrets (Google client secrets, tokens, credentials).
- Do not run destructive git commands (for example `reset --hard`) unless explicitly requested.
- Keep changes targeted and avoid unrelated refactors.
- Keep API namespaces and project references under `CogSlop.Api`.
- Maintain compatibility with:
  - .NET `net10.0`
  - Angular CLI/app scaffold already present in `apps/ui`.
