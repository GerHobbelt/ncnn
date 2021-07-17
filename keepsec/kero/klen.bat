cd g\%1\
move *.png ..\0000\
move *.gif ..\0000\
cd ok\
move *.jpg ..\
cd ..\

md ..\0000\ok
del ..\0000\jj.js
echo const jjpa='g/0000/';>>..\0000\jj.js
echo var jjp=[>>..\0000\jj.js

set maxbytesize=250000

FOR %%A in (*.jpg) do (
	if %%~zA LSS %maxbytesize% (
		move %%A ..\0000\
		echo '%%~nA',>>..\0000\jj.js
	)
)
echo ];>>..\0000\jj.js

cd ..\..\
rd /s /q g\%1

rd Q:\z\bookpdf\0bak\tu\ar\2\zc

SET /A nexz=%1+1
mklink /J Q:\z\bookpdf\0bak\tu\ar\2\zc g\%nexz%

color 6f