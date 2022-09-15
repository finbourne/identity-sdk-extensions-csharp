FROM mcr.microsoft.com/dotnet/core/sdk:3.1

RUN mkdir -p /usr/src
WORKDIR /usr/src

COPY Finbourne.Identity.Sdk.Extensions.sln /usr/src/
COPY Finbourne.Identity.Sdk.Extensions/Finbourne.Identity.Sdk.Extensions.csproj /usr/src/Finbourne.Identity.Sdk.Extensions/
COPY Finbourne.Identity.Sdk.Extensions.Tests/Finbourne.Identity.Sdk.Extensions.Tests.csproj /usr/src/Finbourne.Identity.Sdk.Extensions.Tests/
COPY Finbourne.Identity.Sdk.Extensions.IntegrationTests/Finbourne.Identity.Sdk.Extensions.IntegrationTests.csproj /usr/src/Finbourne.Identity.Sdk.Extensions.IntegrationTests/
RUN dotnet restore Finbourne.Identity.Sdk.Extensions

ENTRYPOINT dotnet test --configuration Release --verbosity normal