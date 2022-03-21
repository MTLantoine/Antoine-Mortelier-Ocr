FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Antoine.Mortelier.Ocr.Console/Antoine.Mortelier.Ocr.Console.csproj", "Antoine.Mortelier.Ocr.Console/"]
RUN dotnet restore "Antoine.Mortelier.Ocr.Console/Antoine.Mortelier.Ocr.Console.csproj"
COPY . .
WORKDIR "/src/Antoine.Mortelier.Ocr.Console"
RUN dotnet build "Antoine.Mortelier.Ocr.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Antoine.Mortelier.Ocr.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Antoine.Mortelier.Ocr.Console.dll"]
