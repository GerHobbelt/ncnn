echo buildncc

curl -L --silent --show-error --output VulkanSDK.exe https://vulkan.lunarg.com/sdk/download/1.1.108.0/windows/VulkanSDK-1.1.108.0-Installer.exe?Human=true
.\VulkanSDK.exe /S

cd %APPVEYOR_BUILD_FOLDER%
md build-vs2017
cd build-vs2017

cmake -G"NMake Makefiles" -DCMAKE_BUILD_TYPE=Release -DCMAKE_INSTALL_PREFIX=%cd%/install -DProtobuf_INCLUDE_DIR==%APPVEYOR_BUILD_FOLDER%/ap//build-vs2017/install/include -DProtobuf_LIBRARIES==%APPVEYOR_BUILD_FOLDER%/ap//build-vs2017/install/lib/libprotobuf.lib -DProtobuf_PROTOC_EXECUTABLE==%APPVEYOR_BUILD_FOLDER%/ap//build-vs2017/install/bin/protoc.exe -DNCNN_VULKAN=ON ..


nmake
nmake install
