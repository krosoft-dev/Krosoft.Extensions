(Get-ChildItem ./src -Filter *.csproj -Recurse) | ForEach-Object {
  Write-Host =========== $_.BaseName
 
  $fileName = "./temp/nuget-$($_.BaseName)-pipeline.yml"
  New-Item $fileName -Force #-ErrorAction SilentlyContinue
  Set-Content $fileName $content

  $content = Get-Content $_.FullName `
  | Find "<ProjectReference Include=" `
  | ForEach-Object { $_ -replace '<ProjectReference Include=', '' -replace '/>', '' }  `
  | Sort-Object -Unique  `
  | Split-Path -Leaf   `
  | ForEach-Object {$_.replace('.csproj"','')} 
  
  Set-Content $fileName $content
  Write-Host $content
  Write-Host ===========
  Write-Host  
}

 

# New-Item D:\temp\test\test.txt
 

# Set-Content D:\temp\test\test.txt 'Welcome to TutorialsPoint'