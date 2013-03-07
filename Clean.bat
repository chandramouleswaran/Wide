msbuild Wide.sln /t:clean
FOR /D %%p IN ("Build\*.*") DO rmdir "%%p" /s /q
rmdir Build /s /q