# CargoHubTeam2


- Valdier dos Santos

# Docker Set-Up
## Prerequiste
Download Docker set up https://www.docker.com/products/docker-desktop/

## When Starting
### 1 docker compose up -d  
creates an image that you can see running on Docker Desktop 
### 2 dotnet ef migrations add (enter in a random name here)
### 2 dotnet ef migrations add example
this creates a snapshot of the models attributes and there relations and will automatically will be seen as files in the folder named migrations
### 3 dotnet ef database update 
will use the latest created migrations and will push its database structure in the DB that can be seen at http://localhost:8081/login?next=/

dotnet ef database drop --force
