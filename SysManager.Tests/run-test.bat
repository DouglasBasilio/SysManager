cmd /c "dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=./TestResults/coverage.cobertura.xml"

:: é necessário instalar o reportgenerator com o seguinte comando: dotnet tool install -g dotnet-reportgenerator-globaltool
cmd /c "reportgenerator "-reports:.\TestResults\coverage.cobertura.xml" "-targetdir:.\TestResults" -reporttypes:Html"

start ./TestResults/index.html