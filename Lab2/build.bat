if %1 == "" goto error
if %1 == "Src\Frontend" goto error
if %1 == "Src\Backend" goto error

set baseUrl=%CD%

cd "%baseUrl%\Src\Frontend"
dotnet publish -c Release

cd "%baseUrl%\Src\Backend"
dotnet publish -c Release

RD /s /q "%CD%\%1\"

mkdir "%baseUrl%\%1\Src\Frontend"
xcopy "%baseUrl%\Src\Frontend\bin" "%baseUrl%\%1\Src\Frontend\" /S /E /Y

mkdir "%baseUrl%\%1\Src\Backend"
xcopy "%baseUrl%\Src\Backend\bin" "%baseUrl%\%1\Src\Backend\" /S /E /Y

copy /Y "%baseUrl%\run.bat" "%baseUrl%\%1\run.bat"
copy /Y "%baseUrl%\config.txt" "%baseUrl%\%1\backendConfig.txt"
copy /Y "%baseUrl%\config.txt" "%baseUrl%\%1\frontendConfig.txt"
copy /Y "%baseUrl%\stop.bat" "%baseUrl%\%1\stop.bat"

if errorlevel 0 goto exit
:error
echo Empty param
goto exit
:exit

