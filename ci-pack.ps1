dotnet --list-sdks
dotnet restore
dotnet clean -c Release

# Building for packing and publishing.
dotnet pack -c Release -p:PackageOutputPath="$PSScriptRoot/artifacts"