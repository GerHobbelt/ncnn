rd /s /q ap
git clone -q --branch=master https://github.com/Klanly/protobuf.git "%APPVEYOR_BUILD_FOLDER%\ap"
md "%APPVEYOR_BUILD_FOLDER%\ap\build-vs2017"

call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvarsall.bat" amd64 10.0.10240.0



cd %APPVEYOR_BUILD_FOLDER%\ap\build-vs2017
cmake -G"NMake Makefiles" -DCMAKE_BUILD_TYPE=Release -DCMAKE_INSTALL_PREFIX=%cd%/install -Dprotobuf_BUILD_TESTS=OFF -Dprotobuf_MSVC_STATIC_RUNTIME=OFF ../cmake
nmake
nmake install

appveyor DownloadFile https://vulkan.lunarg.com/sdk/download/1.1.108.0/windows/VulkanSDK-1.1.108.0-Installer.exe?Human=true -FileName VulkanSDK.exe
VulkanSDK.exe /S

cd %APPVEYOR_BUILD_FOLDER%


