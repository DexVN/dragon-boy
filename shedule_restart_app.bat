@echo off
:: --- CẤU HÌNH ---
:: NHỚ: Tên app phải khớp 100% với trong Task Manager (cột Details)
set "app_name=DragonBoy.exe"
set "app_path=C:\Users\duykhanh\Documents\github\dragon-boy\DragonBoy.exe"
:: ----------------

:loop
cls
echo [Status] Dang xu ly %app_name%...

:: Thu dong app va hien loi neu co (bo >nul 2>&1 de xem thong bao)
taskkill /f /im "%app_name%" /t

:: Cho 3 giay de app dong hoan toan
timeout /t 3 /nobreak >nul

:: Kiem tra xem app con song khong truoc khi mo moi
tasklist /fi "ImageName eq %app_name%" | find /i "%app_name%" >nul
if %errorlevel% equ 0 (
    echo [LOI] Khong the dong app! Hay chay Script bang quyen Admin.
    pause
    exit
)

echo [Status] Dang mo ban moi...
start "" "%app_path%"

echo [Status] Da xong. Cho 5 phut...
timeout /t 300 /nobreak
goto loop