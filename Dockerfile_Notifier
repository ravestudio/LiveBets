FROM microsoft/dotnet:sdk AS build-env
WORKDIR /sln

# copy solution
COPY *.sln ./

COPY ./CommonLib/CommonLib.csproj ./CommonLib/CommonLib.csproj
COPY ./EventCollector/EventCollector.csproj ./EventCollector/EventCollector.csproj
COPY ./Notifier/Notifier.csproj ./Notifier/Notifier.csproj
COPY ./bkService/bkService.csproj ./bkService/bkService.csproj
RUN dotnet restore

# copy and build everything else
COPY ./CommonLib ./CommonLib
COPY ./EventCollector ./EventCollector
COPY ./Notifier ./Notifier
COPY ./bkService ./bkService

RUN dotnet publish "./Notifier/Notifier.csproj" -c Release -o /out

FROM microsoft/dotnet:runtime
WORKDIR /app
COPY --from=build-env /out .
ENTRYPOINT ["dotnet", "Notifier.dll"]
