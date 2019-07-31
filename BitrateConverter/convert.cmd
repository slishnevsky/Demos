@ECHO OFF
ECHO.
ECHO BEGIN CONVERSION
ECHO.
CD %1
DIR *.mp3
ECHO -------------------------------------------------------------------------------
ECHO THESE MP3 FILES WILL BE CONVERTED TO BITRATE %2 KBPS
ECHO -------------------------------------------------------------------------------
PAUSE
FOR %%f IN (*.mp3) DO (
    ECHO -------------------------------------------------------------------------------
    ECHO CONVERTING: %%f
    ECHO -------------------------------------------------------------------------------
    D:\Apps\Lame\lame.exe -h -b %2 "%%f" "%%~nf.wav"
    DEL "%%f"
    REN "%%~nf.wav" "%%f"
)
ECHO.
ECHO END CONVERSION
ECHO.
PAUSE