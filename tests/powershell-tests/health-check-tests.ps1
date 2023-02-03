$ports = "9000", "9002", "9004", "9006"
Start-Sleep 30
foreach($port in $ports) {
    $apiUrl = "http://localhost:$port/health"
    $response = Invoke-WebRequest -Uri $apiUrl
    if ($response.StatusCode -eq 200 -and $response.Content -like "*Healthy*") {
        Write-Output "Test passed successfully"
    }
    else {
        exit 1
    }
}