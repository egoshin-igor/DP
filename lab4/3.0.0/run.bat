set baseUrl=%CD%

cd "%baseUrl%\Src\Frontend\Release\netcoreapp2.1\publish"
start dotnet frontend.dll
cd "%baseUrl%\Src\BackendApi\Release\netcoreapp2.1\publish"
start dotnet backend.dll
cd "%baseUrl%\Src\TextListener\Release\netcoreapp2.1\publish"
start dotnet TextListener.dll
cd "%baseUrl%\Src\TextRankCalc\Release\netcoreapp2.2\publish"
start dotnet TextRankCalc.dll

if errorlevel 0 goto exit
:error
echo Empty param
goto exit
:exit