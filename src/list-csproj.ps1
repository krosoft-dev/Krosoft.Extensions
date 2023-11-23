(Get-ChildItem -Filter *.csproj -Recurse) | ForEach-Object {
    Write-Host =========== $_.BaseName
    Get-Content $_.FullName `
  | Find "<ProjectReference Include=" `
  | ForEach-Object { $_ -replace '<ProjectReference Include=', '' -replace '/>', '' }  `
  | Sort-Object -Unique
    Write-Host ===========
    Write-Host  
}