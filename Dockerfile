# Full .NET Core SDK
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Move the sln file to the empty container 
COPY reminder.sln ./

# Move the api project
COPY ["api/api.csproj", "api/"]
COPY ["library/library.csproj", "library/"]
COPY ["test/*.csproj", "test/"]
COPY . .
RUN dotnet restore
RUN dotnet build "api/api.csproj" -c Release -o /app/src

# Publish 
FROM build AS publish 
RUN dotnet publish "api/api.csproj" -c Release -o /app/src

# Instruct where the image should run from 
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 
WORKDIR /app
EXPOSE 80 
COPY --from=publish /app/src .
ENTRYPOINT [ "dotnet", "api.dll"]

# Generated this image painfull using the following method
# docker build -t luisenalvar/reminderapi .
# docker run -it luisenalvar/reminderapi  ## to inspect the file system of the image 