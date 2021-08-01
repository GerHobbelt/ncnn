FOR %%a in (zb*_v.png) do (
"D:\Program Files\irfanview\pngquant.exe" --speed 2 --quality=90-100 --ext j.png  %%a
del %%a
)
