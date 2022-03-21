FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Antoine.Mortelier.Ocr.WebApi/Antoine.Mortelier.Ocr.WebApi.csproj", "Antoine.Mortelier.Ocr.WebApi/"]
RUN dotnet restore "Antoine.Mortelier.Ocr.WebApi/Antoine.Mortelier.Ocr.WebApi.csproj"
COPY . .
WORKDIR "/src/Antoine.Mortelier.Ocr.WebApi"
RUN dotnet build "Antoine.Mortelier.Ocr.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Antoine.Mortelier.Ocr.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Antoine.Mortelier.Ocr.WebApi.dll"]
