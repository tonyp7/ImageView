@echo off
cd /D "%~dp0"
set arg1=%1
if not exist bin\%1\lib mkdir bin\%1\lib
del /Q bin\%1\lib\*.dll
if exist bin\%1\fr Xcopy /Y /E /I bin\%1\fr bin\%1\lib\fr
if exist bin\%1\x64 Xcopy /Y /E /I bin\%1\x64 bin\%1\lib
copy /B /V /Y bin\%1\*.dll bin\%1\lib\
del /Q bin\%1\*.dll
if exist bin\%1\fr del /Q bin\%1\fr\*.dll
if exist bin\%1\x64 del /Q bin\%1\x64\*.dll
if exist bin\%1\x86 del /Q bin\%1\x86\*.dll
if exist bin\%1\x86 rmdir bin\%1\x86
if exist bin\%1\x64 rmdir bin\%1\x64
if exist bin\%1\fr rmdir bin\%1\fr
