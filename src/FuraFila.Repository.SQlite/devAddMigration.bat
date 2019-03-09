@echo off
set /p mig_name="enter migration name: "

dotnet restore

dotnet build

rem dotnet ef --startup-project ../FuraFila.WebApp/ migrations add %mig_name%

dotnet ef migrations add %mig_name%