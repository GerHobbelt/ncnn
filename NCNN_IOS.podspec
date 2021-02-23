Pod::Spec.new do |spec|

  spec.name         = "NCNN_IOS"
  spec.version      = "20210126"
  spec.summary      = "ncnn powerby Tencent"

  spec.description  = <<-DESC
  ncnn is a high-performance neural network inference framework optimized for the mobile platform.
                   DESC

  spec.homepage     = "https://github.com/Tencent/ncnn"

  spec.license      = { :type => "BSD 3-Clause", :file => "https://github.com/Tencent/ncnn/LICENSE.txt" }

  spec.author             = { "DCTech" => "412200533@qq.com" }

  spec.platform     = :ios, "9.0"
  spec.source       = { :http=> "https://github.com/Tencent/ncnn/releases/download/20210124/ncnn-20210124-ios-vulkan-bitcode.zip" }

  spec.vendored_libraries = "openmp.framework/openmp", "ncnn.framework/ncnn","glslang.framework/glslang"

  spec.xcconfig = { 'HEADER_SEARCH_PATHS' => '${PODS_ROOT}/NCNN_IOS/openmp.framework/Headers/;${PODS_ROOT}/NCNN_IOS/ncnn.framework/Headers/;${PODS_ROOT}/NCNN_IOS/glslang.framework/Headers/'}

  spec.pod_target_xcconfig = {
    'EXCLUDED_ARCHS[sdk=iphonesimulator*]' => 'arm64'
  }
  spec.user_target_xcconfig = { 'EXCLUDED_ARCHS[sdk=iphonesimulator*]' => 'arm64' }

end
