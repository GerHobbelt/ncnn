cavif.exe -i tmpklean.png -o tmpalph.avif --enable-full-color-range --encode-target alpha --monochrome
cavif.exe -i tmpklean.png -o 0\af\%1 --enable-full-color-range --encode-target image --attach-alpha tmpalph.avif
del tmpklean.png
del tmpalph.avif
"D:\Program Files\irfanview\pngquant.exe" --speed 2 --quality=5-10 --ext j.png  0\%1
move 0\%~n1j.png 0\pq\%1