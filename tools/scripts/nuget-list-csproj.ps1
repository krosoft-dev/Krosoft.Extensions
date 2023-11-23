(Get-ChildItem ./src -Filter *.csproj -Recurse) | ForEach-Object {
  Write-Host =========== $_.BaseName
  Get-Content $_.FullName `
  | Find "<ProjectReference Include=" `
  | ForEach-Object { $_ -replace '<ProjectReference Include=', '' -replace '/>', '' }  `
  | Sort-Object -Unique  `
  | Split-Path -Leaf  
  Write-Host ===========
  Write-Host  
}

 