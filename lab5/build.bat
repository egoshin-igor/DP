if %1 == "" goto error
if %1 == "Src\Frontend" goto error
if %1 == "Src\BackendApi" goto error
if %1 == "Src\TextListener" goto error
if %1 == "Src\TextRankCalc" goto error
if %1 == "Src\VowelConsCounter" goto error
if %1 == "Src\VowelConsRater" goto error

set baseUrl=%CD%

cd "%baseUrl%\Src\Frontend"
dotnet publish -c Release

cd "%baseUrl%\Src\BackendApi"
dotnet publish -c Release

cd "%baseUrl%\Src\TextListener"
dotnet publish -c Release

cd "%baseUrl%\Src\TextRankCalc"
dotnet publish -c Release

cd "%baseUrl%\Src\VowelConsCounter"
dotnet publish -c Release

cd "%baseUrl%\Src\VowelConsRater"
dotnet publish -c Release
-------------
mkdir "%baseUrl%\%1\Src\Frontend"
xcopy "%baseUrl%\Src\Frontend\bin" "%baseUrl%\%1\Src\Frontend\" /S /E /Y

mkdir "%baseUrl%\%1\Src\BackendApi"
xcopy "%baseUrl%\Src\BackendApi\bin" "%baseUrl%\%1\Src\BackendApi\" /S /E /Y

mkdir "%baseUrl%\%1\Src\TextListener"
xcopy "%baseUrl%\Src\TextListener\bin" "%baseUrl%\%1\Src\TextListener\" /S /E /Y

mkdir "%baseUrl%\%1\Src\TextRankCalc"
xcopy "%baseUrl%\Src\TextRankCalc\bin" "%baseUrl%\%1\Src\TextRankCalc\" /S /E /Y

mkdir "%baseUrl%\%1\Src\VowelConsCounter"
xcopy "%baseUrl%\Src\VowelConsCounter\bin" "%baseUrl%\%1\Src\VowelConsCounter\" /S /E /Y

mkdir "%baseUrl%\%1\Src\VowelConsRater"
xcopy "%baseUrl%\Src\VowelConsRater\bin" "%baseUrl%\%1\Src\VowelConsRater\" /S /E /Y
--------------
copy /Y "%baseUrl%\run.bat" "%baseUrl%\%1\run.bat"
copy /Y "%baseUrl%\config.txt" "%baseUrl%\%1\config.txt"
copy /Y "%baseUrl%\stop.bat" "%baseUrl%\%1\stop.bat"

if errorlevel 0 goto exit
:error
echo Empty param
goto exit
:exit

