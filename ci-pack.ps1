dotnet clean -c Release

$repositoryUrl = "https://github.com/$env:GITHUB_REPOSITORY"

$folder=".\NuSpecs"
$filetypes="*.nuspec"

Get-ChildItem $folder -Filter $filetypes | Foreach-Object {
    $name=$_.Name

    $name="../../NuSpecs/$name"

    $name | Write-Host

    dotnet pack -c Release -p:PackageOutputPath="$PSScriptRoot/artifacts" -p:RepositoryUrl=$repositoryUrl -p:NuspecFile="$name"

}
