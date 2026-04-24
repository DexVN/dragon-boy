$games = @(
    @{ Name = "DragonBoy"; Path = "C:\Users\duykhanh\Documents\github\dragon-boy\DragonBoy.exe" }
)

$interval = 1800

while ($true) {
    Clear-Host
    Write-Host "--- Thoi diem thuc thi: $(Get-Date -Format 'HH:mm:ss') ---" -ForegroundColor Magenta

    foreach ($game in $games) {
        $pName = $game.Name
        $pPath = $game.Path

        # 1. Tat game
        $proc = Get-Process $pName -ErrorAction SilentlyContinue
        if ($proc) {
            Write-Host "Dang tat: $pName..." -ForegroundColor Yellow
            Stop-Process -Name $pName -Force
            Start-Sleep -Seconds 2
        }

        # 2. Mo lai game (Kem theo WorkingDirectory de tranh loi nhay roi tat)
        if (Test-Path $pPath) {
            Write-Host "Dang khoi dong: $pName..." -ForegroundColor Green
            $workingDir = Split-Path $pPath -Parent
            Start-Process -FilePath $pPath -WorkingDirectory $workingDir
        } else {
            Write-Host "Loi: Khong tim thay file tai $pPath" -ForegroundColor Red -BackgroundColor Black
        }
    }

    Write-Host "`nCho $interval giay de tiep tuc..." -ForegroundColor Cyan
    Start-Sleep -Seconds $interval
}