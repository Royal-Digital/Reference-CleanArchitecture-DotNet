Write-Host "Adding migrations to Todo.Data using connection string from Todo.Api" -foregroundcolor "yellow"
Add-Migration -ProjectName Todo.Data -StartupProjectName Todo.Api