FROM microsoft/dotnet:sdk  AS build-env

# BEGIN MODIFICATION - Node is needed for development (but not production)
RUN curl -sL https://deb.nodesource.com/setup_8.x | bash -
RUN apt-get install --assume-yes nodejs
# END MODIFICATION

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

RUN dotnet publish "./bkService/bkService.csproj" -c Release -o /out

COPY ./bkService/bkService.db /out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /out .
ENTRYPOINT ["dotnet", "bkService.dll"]
