FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ConsoleDemo.csproj", "./"]
RUN dotnet restore "ConsoleDemo.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ConsoleDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsoleDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsoleDemo.dll"]
