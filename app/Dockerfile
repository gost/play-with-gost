FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app
COPY ./ .
RUN dotnet restore pwg.console.sln
RUN dotnet publish -c Release -o out ./pwg.web/pwg.web.csproj

# build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/pwg.web/out/ .
ENTRYPOINT ["dotnet", "pwg.web.dll"]

