# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0.103 AS build
WORKDIR /build

RUN curl -sL https://deb.nodesource.com/setup_16.x |  bash -

RUN apt-get install -y nodejs

# copy csproj and restore as distinct layers
WORKDIR /build/DataScienseProject

COPY ./DataScienseProject/*.csproj .

WORKDIR /build/DataScienseProject

RUN dotnet restore DataScienseProject.csproj

# copy everything else and build app
COPY ./DataScienseProject/ .


RUN dotnet publish -c release -o published --no-cache DataScienseProject.csproj

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /build/DataScienseProject/published ./
RUN sed -i 's/DEFAULT@SECLEVEL=2/ECDHE-ECDSA-AES256-GCM-SHA384:ECDHE-ECDSA-AES128-GCM-SHA256:ECDHE-RSA-AES256-GCM-SHA384:ECDHE-RSA-AES128-GCM-SHA256:ECDHE-ECDSA-AES256-SHA384:ECDHE-ECDSA-AES128-SHA256:ECDHE-RSA-AES256-SHA384:ECDHE-RSA-AES128-SHA256/g' /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet", "DataScienseProject.dll"]
