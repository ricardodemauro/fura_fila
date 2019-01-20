FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["FuraFila.WebApp/FuraFila.WebApp.csproj", "FuraFila.WebApp/"]
RUN dotnet restore "FuraFila.WebApp/FuraFila.WebApp.csproj"
COPY . .
WORKDIR "/src/FuraFila.WebApp"
RUN dotnet build "FuraFila.WebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FuraFila.WebApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FuraFila.WebApp.dll"]