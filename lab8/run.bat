set baseUrl=%CD%
TextProcessingLimiter
cd "%baseUrl%\Src\Frontend
start "Frontend" dotnet frontend.dll
cd "%baseUrl%\Src\BackendApi"
start "Backend" dotnet backend.dll
cd "%baseUrl%\Src\TextListener"
start "TextListener" dotnet TextListener.dll
cd "%baseUrl%\Src\TextRankCalc"
start "TextRankCalc" dotnet TextRankCalc.dll
cd "%baseUrl%\Src\TextStatistics"
start "TextStatistics" dotnet TextStatistics.dll
cd "%baseUrl%\Src\TextProcessingLimiter"
start "TextProcessingLimiter" dotnet TextProcessingLimiter.dll

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


cd "%baseUrl%\Src\VowelConsCounter"
for /l %%n in (1,1,!vowelConsCounter!) DO start "VowelConsCounter" dotnet VowelConsCounter.dll

cd "%baseUrl%\Src\VowelConsRater"
for /l %%n in (1,1,!vowelConsRater!) DO start "VowelConsRater" dotnet VowelConsRater.dll

if errorlevel 0 goto exit
:error
echo Empty param
goto exit
:exit