# base image 
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base 
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80
#next stage of build uses /sdk image. All build tools and libraries required to build a .NET7 app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
#copy this into root of current location
COPY ["Scith.API.csproj", "./"]
#then run dotnet restore to restore all packages
RUN dotnet restore "Scith.API.csproj"
#then copy every other file
COPY . .

#create new folder called publish that contains all files required to publish the app
RUN dotnet publish "Scith.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
#from build stage, copy everything from app/publish directory into current dir
COPY --from=build /app/publish .
#define how to start the api. Execute dotnet command with Scith.API.dll file to start rest api
ENTRYPOINT ["dotnet", "Scith.API.dll"]
