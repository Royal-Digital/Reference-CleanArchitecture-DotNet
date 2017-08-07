Write-Host "Running migrations from Todo.Data using connection string from Todo.Api" -foregroundcolor "yellow"
Update-Database -ProjectName Todo.Data -StartupProjectName Todo.Api