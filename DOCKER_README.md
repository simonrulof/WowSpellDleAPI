# WowSpellDle API - Docker Setup

This guide explains how to run the WowSpellDle API using Docker.

## Prerequisites

- Docker Desktop installed on your machine
- PostgreSQL database already set up with all tables and data

## Running the Application

### Using Docker Compose (Recommended)

1. **Update the connection string** in `docker-compose.yml`:
   - Replace `host.docker.internal` with your database host (use `host.docker.internal` for localhost databases)
   - Update username and password as needed

2. **Start the application**:
   ```bash
   docker-compose up -d
   ```

3. **Access the API**:
   - API: http://localhost:5000
   - Swagger UI: http://localhost:5000/swagger

4. **Stop the application**:
   ```bash
   docker-compose down
   ```

### Using Docker Only

1. **Build the Docker image**:
   ```bash
   docker build -t wowspelldle-api .
   ```

2. **Run the container**:
   ```bash
   docker run -d -p 5000:8080 \
     -e ConnectionStrings__DefaultConnection="Host=host.docker.internal;Database=WowSpellDle;Username=myuser;Password=mypassword" \
     --name wowspelldle-api \
     wowspelldle-api
   ```

3. **Stop the container**:
   ```bash
   docker stop wowspelldle-api
   docker rm wowspelldle-api
   ```

## Configuration

### Environment Variables

You can override the database connection string using environment variables:

- `ConnectionStrings__DefaultConnection`: PostgreSQL connection string

### Connecting to Local Database

When running the API in Docker and connecting to a database on your host machine:
- Use `host.docker.internal` instead of `localhost` in the connection string
- Ensure your PostgreSQL is configured to accept connections from Docker containers

### Ports

- API HTTP: 5000 (mapped to container port 8080)
- API HTTPS: 5001 (mapped to container port 8081)

## Logs

View API logs:
```bash
docker-compose logs -f api
```

Or with Docker only:
```bash
docker logs -f wowspelldle-api
```

## Troubleshooting

1. **Port already in use**: Change the port mappings in `docker-compose.yml` or the docker run command
2. **Database connection failed**: 
   - Verify the connection string
   - Use `host.docker.internal` to connect to localhost database
   - Check PostgreSQL is allowing connections from Docker
3. **API not responding**: Check logs using `docker-compose logs api` or `docker logs wowspelldle-api`
