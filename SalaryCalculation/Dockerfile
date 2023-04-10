#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SalaryCalculation/SalaryCalculation.csproj", "SalaryCalculation/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Service/Service.csproj", "Service/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Dto/Dto.csproj", "Dto/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["WebFramework/WebFramework.csproj", "WebFramework/"]
COPY ["Mapper/Mapper.csproj", "Mapper/"]
RUN dotnet restore "SalaryCalculation/SalaryCalculation.csproj"
COPY . .
WORKDIR "/src/SalaryCalculation"
RUN dotnet build "SalaryCalculation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalaryCalculation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalaryCalculation.dll"]