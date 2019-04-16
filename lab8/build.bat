if %1 == "" goto error
if %1 == "Src\Frontend" goto error
if %1 == "Src\BackendApi" goto error
if %1 == "Src\TextListener" goto error
if %1 == "Src\TextRankCalc" goto error
if %1 == "Src\VowelConsCounter" goto error
if %1 == "Src\VowelConsRater" goto error
if %1 == "Src\TextProcessingLimiter" goto error

set baseUrl=%CD%

cd "%baseUrl%\Src\Frontend"
dotnet publish -c Release -o "%baseUrl%\%1\Src\Frontend\"

cd "%baseUrl%\Src\BackendApi"
dotnet publish -c Release -o "%baseUrl%\%1\Src\BackendApi\"

cd "%baseUrl%\Src\TextListener"
dotnet publish -c Release -o "%baseUrl%\%1\Src\TextListener\"

cd "%baseUrl%\Src\TextRankCalc"
dotnet publish -c Release -o "%baseUrl%\%1\Src\TextRankCalc\"

cd "%baseUrl%\Src\VowelConsCounter"
dotnet publish -c Release -o "%baseUrl%\%1\Src\VowelConsCounter\"

cd "%baseUrl%\Src\VowelConsRater"
dotnet publish -c Release -o "%baseUrl%\%1\Src\VowelConsRater\"

cd "%baseUrl%\Src\TextStatistics"
dotnet publish -c Release -o "%baseUrl%\%1\Src\TextStatistics\"

cd "%baseUrl%\Src\TextProcessingLimiter"
dotnet publish -c Release -o "%baseUrl%\%1\Src\TextProcessingLimiter\"
copy /Y "%baseUrl%\Src\TextProcessingLimiter\appsettings.json" "%baseUrl%\%1\Src\TextProcessingLimiter\appsettings.json"
--------------
copy /Y "%baseUrl%\run.bat" "%baseUrl%\%1\run.bat"
copy /Y "%baseUrl%\config.txt" "%baseUrl%\%1\config.txt"
copy /Y "%baseUrl%\stop.bat" "%baseUrl%\%1\stop.bat"

if errorlevel 0 goto exit
:error
echo Empty param
goto exit
:exit

