FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["src/OzonEdu.StockApi/OzonEdu.StockApi.csproj","src/OzonEdu.StockApi/"]
RUN dotnet restore "src/OzonEdu.StockApi/OzonEdu.StockApi.csproj"

COPY . .

WORKDIR "/src/src/OzonEdu.StockApi"

RUN dotnet build "OzonEdu.StockApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OzonEdu.StockApi.csproj" -c Release -o /app/publish
COPY "entrypoint.sh" "/app/publish/."

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

WORKDIR /app

EXPOSE 5000
EXPOSE 5002

FROM runtime AS final
WORKDIR /app

COPY --from=publish /app/publish .

RUN chmod +x entrypoint.sh
CMD /bin/bash entrypoint.sh
