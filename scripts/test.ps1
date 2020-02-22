dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov ./tests

# dotnet coverlet .\tests\bin\Debug\netcoreapp3.1\CodeWarsTests.dll --target "dotnet" --targetargs "test ./tests/ --no-build"

# dn build && dotnet vstest ./tests/bin/Debug/netcoreapp3.0/CodeWarsTests.dll /Tests:DomainNameValidatorTest
