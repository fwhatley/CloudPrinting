# multi stage - https://app.pluralsight.com/player?course=docker-images-containers-aspdotnet-core&author=wes-mcclure&name=docker-images-containers-aspdotnet-core-m6&clip=4&mode=live
# INSTRUCTIONS TO BUILD
# build with: docker build -t fredywhatley/cloudprintingservice:1.0.1 .
# run with: 


# 1. BUILD IMAGE STAGE
FROM microsoft/dotnet:latest as build-image-stage
WORKDIR /cloudprintingservice

# separate  dotnet restore to cache and improve build speed
COPY cloudprintingservice.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -o /publish


# 2. RUNTIME IMAGE STATE
FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
WORKDIR /publish

# take image from stage 1
COPY --from=build-image-stage /publish .
ENTRYPOINT ["dotnet", "cloudprintingservice.dll"]
