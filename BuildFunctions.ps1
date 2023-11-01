# Taken from psake https://github.com/psake/psake

<#
.SYNOPSIS
  This is a helper function that runs a scriptblock and checks the PS variable $lastexitcode
  to see if an error occcured. If an error is detected then an exception is thrown.
  This function allows you to run command-line programs without having to
  explicitly check the $lastexitcode variable.
.EXAMPLE
  exec { svn info $repository_trunk } "Error executing SVN. Please verify SVN command-line client is installed"
#>

function Exec {
    [CmdletBinding()]
    param(
        [Parameter(Position=0, Mandatory=$true)]
        [scriptblock]$cmd,

        [Parameter(Position=1, Mandatory=$false)]
        [string]$errorMessage = "Execution failed."
    )

    try {
        & $cmd
        $exitCode = $LASTEXITCODE  # Capture the exit code after executing the script block
        if ($exitCode -ne 0) {
            throw $errorMessage
        }
    }
    catch {
        throw "Exec: $_"
    }
}


function Poke-Xml($filePath, $xpath, $value) {
    try {
        [xml]$fileXml = Get-Content $filePath
        $node = $fileXml.SelectSingleNode($xpath)

        if ($node -ne $null) {
            if ($node.NodeType -eq "Element") {
                $node.InnerText = $value
            } else {
                $node.Value = $value
            }

            $fileXml.Save($filePath)
        } else {
            throw "XPath '$xpath' not found."
        }
    } catch {
        throw "Error updating XML: $_"
    }
}