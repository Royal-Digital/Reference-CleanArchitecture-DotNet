function Get-ProjectReferences
{
    param(
        [Parameter(Mandatory)]
        [string]$rootFolder,
        [string[]]$excludeProjectsContaining
    )
	
    dir $rootFolder -Filter *.csproj -Recurse |
        # Exclude any files matching our rules
        where { "*$($_.Name)*" -notlike $excludeProjectsContaining } |
        Select-References
}
function Select-References
{
    param(
        [Parameter(ValueFromPipeline, Mandatory)]
        [System.IO.FileInfo]$project,
        [string[]]$excludeProjectsContaining
    )
    process
    {
		$baseProjectName = $_.BaseName
		[System.Xml.XmlDocument]$file = new-object System.Xml.XmlDocument
		$file.load($_.FullName)
		$projectReferences = $file.SelectNodes("/Project/ItemGroup/ProjectReference")
		# .NetCore and .NetStandard
		if ( $projectReference.length -gt 0 ){
			foreach ($projectReference in $projectReferences) {
			  $reference = $projectReference.Include
			  $parts = $reference.split("\");
			  $projectNameIndex = $parts.length - 1;
			  $projectName = $parts[$projectNameIndex];
			  $projectNameLength = $lastPart.length;
			  $projectReference = $projectName.Replace(".csproj","")
			  "[" + $baseProjectName + "] -> [" + $projectReference + "]"
			}
		}else{ # .NetFramework
			$projectName = $_.BaseName
			[xml]$projectXml = Get-Content $_.FullName
			$ns = @{ defaultNamespace = "http://schemas.microsoft.com/developer/msbuild/2003" }
			$projectXml |
				# Find the references xml nodes
				Select-Xml '//defaultNamespace:ProjectReference/defaultNamespace:Name' -Namespace $ns |
				# Get the node values
				foreach { $_.node.InnerText } |
				# Exclude any references pointing to projects that match our rules
				where { $excludeProjectsContaining -notlike "*$_*" } |
				# Output in yuml.me format
				foreach { "[" + $projectName + "] -> [" + $_ + "]" }
		}
    }
}

$excludedProjects = "*Tests*"
Get-ProjectReferences "." -excludeProjectsContaining $excludedProjects | Out-File "ProjectDependencies.txt"
Write-Host "Goto -> https://yuml.me/diagram/nofunky/class/draw to view your ProjectDependencies.txt graph" -foregroundcolor "yellow"