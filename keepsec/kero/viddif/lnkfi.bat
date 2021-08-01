mklink /H enc\1.png zb%1.png
mklink /H enc\2.png zb%2.png
"D:\Program Files\Adobe\3Ds\bldr\ffmpeg.exe" -r 0.1 -i enc\%%d.png -pix_fmt yuv420p enc\%2.mp4
del enc\1.png
del enc\2.png