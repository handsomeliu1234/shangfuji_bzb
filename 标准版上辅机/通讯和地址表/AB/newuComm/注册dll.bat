@echo off
@echo opcdaauto.dll是32位程序需要放到指定位置32位系统中的system32，64位系统中的SysWOW64
if "%PROCESSOR_ARCHITECTURE%"=="x86" goto x86
if "%PROCESSOR_ARCHITECTURE%"=="AMD64" goto x64
:x64
@echo 开始注册1
copy %~dp0\opcdaauto.dll %windir%\SysWOW64\opcdaauto.dll
regsvr32 %windir%\SysWOW64\opcdaauto.dll /s
@echo opcdaauto.dll注册成功
@pause
exit
:x86
@echo 开始注册2
copy %~dp0\opcdaauto.dll %windir%\system32\opcdaauto.dll
regsvr32 %windir%\system32\opcdaauto.dll /s
@echo opcdaauto.dll注册成功
@pause
exit