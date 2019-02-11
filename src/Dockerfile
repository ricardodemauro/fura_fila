FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY . .
RUN dotnet restore "FuraFila.WebApp/FuraFila.WebApp.csproj"
WORKDIR "/src/FuraFila.WebApp"
RUN dotnet build "FuraFila.WebApp.csproj" -c Release -o /app
RUN dotnet ef database update

FROM build AS publish
RUN dotnet publish "FuraFila.WebApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

# Comment
RUN echo 'we are running some # of cool things'

ENTRYPOINT ["dotnet", "FuraFila.WebApp.dll"]