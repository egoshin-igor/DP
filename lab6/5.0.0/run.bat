set baseUrl=%CD%

cd "%baseUrl%\Src\Frontend\Release\netcoreapp2.1\publish"
start "Frontend" dotnet frontend.dll
cd "%baseUrl%\Src\BackendApi\Release\netcoreapp2.1\publish"
start "Backend" dotnet backend.dll
cd "%baseUrl%\Src\TextListener\Release\netcoreapp2.1\publish"
start "TextListener" dotnet TextListener.dll
cd "%baseUrl%\Src\TextRankCalc\Release\netcoreapp2.2\publish"
start "TextRankCalc" dotnet TextRankCalc.dll

cd "%baseUrl%
SetLocal EnableDelayedExpansion
set /a c=0
for /f "UseBackQ Delims=" %%A IN ("config.txt") do (
  set /a c+=1
  if !c!==1 set "vowelConsCounter=%%A"
  if !c!==2 set "vowelConsRater=%%A"
)
set vowelConsCounter=%vowelConsCounter:~-1%
set vowelConsRater=%vowelConsRater:~-1%


cd "%baseUrl%\Src\VowelConsCounter\Release\netcoreapp2.2\publish"
for /l %%n in (1,1,!vowelConsCounter!) DO start "VowelConsCounter" dotnet VowelConsCounter.dll

cd "%baseUrl%\Src\VowelConsRater\Release\netcoreapp2.2\publish"
for /l %%n in (1,1,!vowelConsRater!) DO start "VowelConsRater" dotnet VowelConsRater.dll

if errorlevel 0 goto exit
:error
echo Empty param
goto exit
:exit