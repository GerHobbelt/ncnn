Pod::Spec.new do |spec|

  spec.name         = "NCNN_IOS"
  spec.version      = "20210125"
  spec.summary      = "ncnn powerby Tencent"

  spec.description  = <<-DESC
  ncnn is a high-performance neural network inference framework optimized for the mobile platform.
                   DESC

  spec.homepage     = "https://github.com/Tencent/ncnn"

  spec.license      = { :type => "BSD 3-Clause", :file => "https://github.com/Tencent/ncnn/LICENSE.txt" }

  spec.author             = { "DCTech" => "412200533@qq.com" }

  spec.platform     = :ios, "9.0"
  spec.source       = { :http=> "https://hub.fastgit.org/Tencent/ncnn/releases/download/20210124/ncnn-20210124-ios-vulkan-bitcode.zip" }

  spec.source_files  = "*.framework"


  spec.public_header_files = "openmp.framework/Headers/*.h", "ncnn.framework/Header/SPIRV/*.h","ncnn.framework/Header/ncnn/*.h","ncnn.framework/Header/glslang/Public/*.h"

  spec.vendored_frameworks = "openmp.framework", "ncnn.framework","glslang.framework"

  spec.xcconfig = { 'HEADER_SEARCH_PATHS' => '${PODS_ROOT}/NCNN_IOS/openmp.framework/Headers/;${PODS_ROOT}/NCNN_IOS/ncnn.framework/Headers/;${PODS_ROOT}/NCNN_IOS/glslang.framework/Headers/'
  }

end
