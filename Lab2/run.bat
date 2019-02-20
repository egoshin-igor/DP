set baseUrl=%CD%
set /p frontendLauncSettings=<frontendConfig.txt
set /p backendLauncSettings=<backendConfig.txt

cd "%baseUrl%\Src\Frontend\Release\netcoreapp2.1\publish"
start dotnet frontend.dll %frontendLauncSettings%

cd "%baseUrl%\Src\Backend\Release\netcoreapp2.1\publish"
start dotnet backend.dll %backendLauncSettings%

if errorlevel 0 goto exit
:error
echo Empty param
goto exit
:exit