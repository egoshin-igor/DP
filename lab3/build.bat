if %1 == "" goto error
if %1 == "Src\Frontend" goto error
if %1 == "Src\BackendApi" goto error
if %1 == "Src\TextListener" goto error

set baseUrl=%CD%

cd "%baseUrl%\Src\Frontend"
dotnet publish -c Release

cd "%baseUrl%\Src\BackendApi"
dotnet publish -c Release

cd "%baseUrl%\Src\TextListener"
dotnet publish -c Release

mkdir "%baseUrl%\%1\Src\Frontend"
xcopy "%baseUrl%\Src\Frontend\bin" "%baseUrl%\%1\Src\Frontend\" /S /E /Y

mkdir "%baseUrl%\%1\Src\BackendApi"
xcopy "%baseUrl%\Src\BackendApi\bin" "%baseUrl%\%1\Src\BackendApi\" /S /E /Y

mkdir "%baseUrl%\%1\Src\TextListener"
xcopy "%baseUrl%\Src\TextListener\bin" "%baseUrl%\%1\Src\TextListener\" /S /E /Y

copy /Y "%baseUrl%\run.bat" "%baseUrl%\%1\run.bat"
copy /Y "%baseUrl%\config.txt" "%baseUrl%\%1\config.txt"
copy /Y "%baseUrl%\stop.bat" "%baseUrl%\%1\stop.bat"

if errorlevel 0 goto exit
:error
echo Empty param
goto exit
:exit

