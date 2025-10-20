# Porty

A full-stack project with an **ASP.NET Core (.NET 8) API** and a **Vite (Node 20) Frontend**, orchestrated with **Docker Compose** and **SQL Server**.

- **Development**: hot reload for API, HTTPS locally.
- **Production (local compose)**: API via **HTTPS on `:8080`**, Front via **HTTPS on `:443`**, SQL Server isolated.

---

## Stack

- **API**: ASP.NET Core (.NET 8) — `api/`
- **Front**: Vite (Node 20) — `front/`
- **DB**: SQL Server 2022 (container)
- **Orchestration**: Docker Compose
- **TLS**: Dev certificate mounted into containers

---

## Repository layout (overview)

```
/api                   # ASP.NET Core project
/front                 # Vite app
/docker-compose.yml.example      # production-like local stack (rename to .yml)
/docker-compose.dev.yml.example  # development stack (rename to .yml)
```

> **Important:** The Compose files are provided as `*.example`.  
> To use them, **remove the `.example` suffix** so you end up with:
>
> - `docker-compose.yml`
> - `docker-compose.dev.yml`

---

## Prerequisites

- **Docker Desktop** (or compatible runtime)
- **.NET 8 SDK** (only required if you run EF tools from your host)
- **Node 20** (optional for local-only runs; the front is built in containers)

---

## HTTPS certificates (one-time setup on your host)

Generate the .NET **dev certificate** and export it to a **PFX**.

### Windows (PowerShell)
```powershell
mkdir "$env:USERPROFILE\.aspnet\https" -ErrorAction SilentlyContinue
dotnet dev-certs https --clean
dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\aspnetapp.pfx" -p devcert
dotnet dev-certs https --trust
```

### Linux / macOS (bash)
```bash
mkdir -p ~/.aspnet/https
dotnet dev-certs https --clean
dotnet dev-certs https -ep ~/.aspnet/https/aspnetapp.pfx -p devcert
dotnet dev-certs https --trust
```

The **API** uses the `aspnetapp.pfx` directly.  
The **Front** (static server) needs **CRT/KEY**; convert once from the PFX:

```bash
# (Linux/macOS or Git Bash on Windows)
openssl pkcs12 -in ~/.aspnet/https/aspnetapp.pfx -clcerts -nokeys -out ~/.aspnet/https/cert.crt -password pass:devcert
openssl pkcs12 -in ~/.aspnet/https/aspnetapp.pfx -nocerts -out ~/.aspnet/https/cert.key -password pass:devcert -nodes
```

**Files expected by the Compose files:**
- `~/.aspnet/https/aspnetapp.pfx` (API)
- `~/.aspnet/https/cert.crt` and `~/.aspnet/https/cert.key` (Front)

---

## Development (Docker Compose)

1) **Rename** `docker-compose.dev.yml.example` → `docker-compose.dev.yml`  
2) Start the stack:
```bash
docker compose -f docker-compose.dev.yml up --build
```
- **API (HTTPS)**: https://localhost:9091  
- **Front (Vite dev)**: http://localhost:5174  
- **SQL**: host `localhost`, port **1434**

Stop:
```bash
docker compose -f docker-compose.dev.yml down
```

> Tip: The dev stack is prepared to run alongside prod (separate ports & volumes).

---

## Production-like local stack (Docker Compose)

1) **Rename** `docker-compose.yml.example` → `docker-compose.yml`  
2) Start the stack:
```bash
docker compose up --build
```
- **Front (HTTPS)**: https://localhost  
- **API (HTTPS)**: https://localhost:8080  
- **SQL**: host `localhost`, port **1435**

Stop:
```bash
docker compose down
```

> Tip: you can name the stack for clarity in Docker Desktop:
> ```bash
> docker compose -p porty-prod up --build
> ```

---

## Useful URLs

- **API – Swagger**: `https://localhost:8080/swagger`  
  If you get a 404, see **Swagger in Production** below.
- **Front**: `https://localhost` (prod compose) • `http://localhost:5174` (dev)

---

## Configuration

### Connection string (in containers)
The Compose files set:
```
ConnectionStrings__DefaultConnection=Server=db;Database=Porty;User Id=Porty;Password=IUSR_PORTY;TrustServerCertificate=True;MultipleActiveResultSets=True
```
In code:
```csharp
var cs = builder.Configuration.GetConnectionString("DefaultConnection");
```

**From host tools (EF CLI)** use:
- Dev DB: `Server=localhost,1434;...`
- Prod compose DB: `Server=localhost,1435;...`

---

## EF Core migrations

### Option A — run from your host
> `dotnet ef` doesn’t read Compose env vars; pass the connection string via env.

**Windows PowerShell**
```powershell
$env:ConnectionStrings__DefaultConnection = "Server=localhost,1435;Database=Porty;User Id=Porty;Password=IUSR_PORTY;TrustServerCertificate=True;MultipleActiveResultSets=True"
dotnet ef database update --project api --startup-project api
```

**bash**
```bash
ConnectionStrings__DefaultConnection="Server=localhost,1435;Database=Porty;User Id=Porty;Password=IUSR_PORTY;TrustServerCertificate=True;MultipleActiveResultSets=True" \
dotnet ef database update --project api --startup-project api
```

### Option B — run inside the API container
```bash
docker exec -it porty-api-prod dotnet ef database update --project /app --startup-project /app
```

---

## Swagger in Production

If `/swagger` returns 404, enable Swagger explicitly in `Program.cs`:

```csharp
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Porty API v1");
    c.RoutePrefix = "swagger";
});
```

> Optional: gate it with an env var:
> ```csharp
> if (app.Environment.IsDevelopment() || app.Configuration["ENABLE_SWAGGER"] == "true")
> {
>     app.UseSwagger();
>     app.UseSwaggerUI();
> }
> ```
> and in Compose: `ENABLE_SWAGGER: "true"`

---

## CORS (front ↔ API)

If the dev front (5174) is blocked by CORS, add in `Program.cs`:

```csharp
var cors = "_cors";
builder.Services.AddCors(o => o.AddPolicy(cors, p =>
{
    p.WithOrigins("http://localhost:5174", "https://localhost")
     .AllowAnyHeader()
     .AllowAnyMethod();
}));
app.UseCors(cors);
```

---

## Troubleshooting

- **Cannot configure HTTPS / dev cert missing**  
  Ensure `~/.aspnet/https/aspnetapp.pfx` exists on the host and is mounted with `~/.aspnet/https:/https:ro`.

- **`/https/aspnetapp.pfx` not found in container**  
  The host path is empty or incorrect. Re-generate the certs and confirm the mount.

- **SQL connection fails from API**  
  Inside Docker, use `Server=db;...` (service name), not `localhost`.

- **`ConnectionStrings:DefaultConnection` missing when running `dotnet ef`**  
  Export the env var before the command (see EF section).

- **404 on `/swagger`**  
  Enable Swagger in production (see above).

- **Ports not reachable**  
  Ensure `ASPNETCORE_URLS` (internal listen port) matches the Compose `ports:` mapping.

---

## Handy commands

```bash
# Show mapped ports
docker ps

# Tail container logs
docker logs -f porty-api-dev
docker logs -f porty-api-prod

# Check the connection string env inside the container
docker exec -it porty-api-prod printenv | grep ConnectionStrings
```

---

## License

Add your preferred license (e.g., MIT).

---

## Support

Open an issue and include:
- the Compose file you used (dev/prod),
- the exact command you ran,
- relevant logs (API/front/DB),
- your OS and Docker version.
