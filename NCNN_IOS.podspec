Pod::Spec.new do |spec|

  spec.name         = "NCNN_IOS"
  spec.version      = "20200225"
  spec.summary      = "ncnn powerby Tencent"

  spec.description  = <<-DESC
  ncnn is a high-performance neural network inference framework optimized for the mobile platform.
                   DESC

  spec.homepage     = "https://github.com/Tencent/ncnn"

  spec.license      = { :type => "BSD 3-Clause", :file => "https://github.com/Tencent/ncnn/LICENSE.txt" }

  spec.author             = { "DCTech" => "412200533@qq.com" }

  spec.platform     = :ios, "9.0"
  spec.source       = { :http=> "https://github.com/DaChengTechnology/ncnn/releases/download/20200224/ncnn-20200224-ios-vulkan-bitcode-pod.zip" }

  spec.vendored_libraries = "**/libglslang.a","**/libncnn.a","**/libOGLCompiler.a","**/libomp.a","**/libOSDependent.a","**/libSPIRV.a"

  spec.xcconfig = { 'HEADER_SEARCH_PATHS' => '**/Include'}

  spec.pod_target_xcconfig = {
    'EXCLUDED_ARCHS[sdk=iphonesimulator*]' => 'arm64'
  }
  spec.user_target_xcconfig = { 'EXCLUDED_ARCHS[sdk=iphonesimulator*]' => 'arm64' }

end
