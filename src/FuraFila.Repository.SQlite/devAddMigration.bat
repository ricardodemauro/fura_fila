@echo off
set /p mig_name="enter migration name: "

dotnet restore

dotnet build

dotnet ef --startup-project ../FuraFila.WebApp/ migrations add %mig_name%

rem dotnet ef migrations add %mig_name%