cavif.exe -i alph\%1 -o tmpalph.avif --enable-full-color-range --monochrome
cavif.exe -i tmpklean.png -o 0\af\%1 --enable-full-color-range --encode-target image --attach-alpha tmpalph.avif
del tmpklean.png
del tmpalph.avif