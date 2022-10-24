#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CitadellesDotIO.Client/CitadellesDotIO.Client.csproj", "CitadellesDotIO.Client/"]
RUN dotnet restore "CitadellesDotIO.Client/CitadellesDotIO.Client.csproj"
COPY . .
WORKDIR "/src/CitadellesDotIO.Client"
RUN dotnet build "CitadellesDotIO.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CitadellesDotIO.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CitadellesDotIO.Client.dll"]