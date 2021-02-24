Pod::Spec.new do |spec|

  spec.name         = "NCNN_MAC"
  spec.version      = "20210225"
  spec.summary      = "ncnn powerby Tencent."

  spec.description  = <<-DESC
  ncnn is a high-performance neural network inference framework optimized for the mobile platform.
    DESC

  spec.homepage     = "https://github.com/Tencent/ncnn"

  spec.license      = { :type => "BSD 3-Clause", :file => "https://github.com/Tencent/ncnn/LICENSE.txt" }

  spec.author             = { "DCTech" => "412200533@qq.com" }

  spec.platform     = :osx, "10.15"

  spec.source       = { :http => "https://github.com/Tencent/ncnn/releases/download/20210124/ncnn-20210124-macos-vulkan.zip"}

  spec.vendored_libraries = "**/libglslang.a","**/libncnn.a","**/libOGLCompiler.a","**/libomp.a","**/libOSDependent.a","**/libSPIRV.a"

  spec.xcconfig = { 'HEADER_SEARCH_PATHS' => '${PODS_ROOT}/NCNN_IOS/openmp.framework/Headers/;${PODS_ROOT}/NCNN_IOS/ncnn.framework/Headers/;${PODS_ROOT}/NCNN_IOS/glslang.framework/Headers/'
  }

end
