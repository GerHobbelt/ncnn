echo buildncc



cd %APPVEYOR_BUILD_FOLDER%
md build-vs2017
cd build-vs2017

cmake -G"Visual Studio 15 2017 Win64" -DCMAKE_BUILD_TYPE=Release -DCMAKE_INSTALL_PREFIX=%cd%/install -DProtobuf_INCLUDE_DIR=%APPVEYOR_BUILD_FOLDER%/ap/build-vs2017/install/include -DProtobuf_LIBRARIES=%APPVEYOR_BUILD_FOLDER%/ap/build-vs2017/install/lib/libprotobuf.lib -DProtobuf_PROTOC_EXECUTABLE=%APPVEYOR_BUILD_FOLDER%/ap/build-vs2017/install/bin/protoc.exe -DNCNN_VULKAN=ON ..



cd %APPVEYOR_BUILD_FOLDER%
del VulkanSDK.exe
rd /s /q ap
