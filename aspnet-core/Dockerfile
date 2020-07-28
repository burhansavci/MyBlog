FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app
COPY ./src/MyBlog.Core/*.csproj ./src/MyBlog.Core/
COPY ./src/MyBlog.Entities/*.csproj ./src/MyBlog.Entities/
COPY ./src/MyBlog.DataAccess/*.csproj ./src/MyBlog.DataAccess/
COPY ./src/MyBlog.Business/*.csproj ./src/MyBlog.Business/
COPY ./src/MyBlog.WebAPI/*.csproj ./src/MyBlog.WebAPI/
COPY *.sln .
RUN dotnet restore 
COPY . .
WORKDIR /app/src/MyBlog.WebAPI
RUN dotnet build MyBlog.WebAPI.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish MyBlog.WebAPI.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet MyBlog.WebAPI.dll
