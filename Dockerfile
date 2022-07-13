# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./CustomMiddleware/CustomMiddleware.csproj" --disable-parallel
RUN dotnet publish "./CustomMiddleware/CustomMiddleware.csproj" -c release -o /app --no-restore


# Serve Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT [ "dotnet", "CustomMiddleware.dll" ]