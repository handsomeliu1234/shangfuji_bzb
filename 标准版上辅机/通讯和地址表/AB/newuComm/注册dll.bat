@echo off
@echo opcdaauto.dll��32λ������Ҫ�ŵ�ָ��λ��32λϵͳ�е�system32��64λϵͳ�е�SysWOW64
if "%PROCESSOR_ARCHITECTURE%"=="x86" goto x86
if "%PROCESSOR_ARCHITECTURE%"=="AMD64" goto x64
:x64
@echo ��ʼע��1
copy %~dp0\opcdaauto.dll %windir%\SysWOW64\opcdaauto.dll
regsvr32 %windir%\SysWOW64\opcdaauto.dll /s
@echo opcdaauto.dllע��ɹ�
@pause
exit
:x86
@echo ��ʼע��2
copy %~dp0\opcdaauto.dll %windir%\system32\opcdaauto.dll
regsvr32 %windir%\system32\opcdaauto.dll /s
@echo opcdaauto.dllע��ɹ�
@pause
exit