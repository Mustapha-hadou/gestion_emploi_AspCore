# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:2.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:2.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "miniPrpject_Asp.dll"]


# docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<1234@hadou>"  -p 1433:1433 --net hadou_net --name sql1 -d mcr.microsoft.com/mssql/server:2019-latest
# docker run --name hadou --net hadou_net -p 8787:80 -d hadou-image