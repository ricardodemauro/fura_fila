dotnet restore

dotnet build

REM dotnet ef --startup-project ../FuraFila.WebApp/ database update

dotnet ef database update

xcopy /s /y core.db ..\FuraFila.WebApp\core.db