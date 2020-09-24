gs\glslangValidator.exe -V -Os -o spv/CINZ0b cain_postproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/CINZ0m cain_postproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/CINZ0s cain_postproc.comp
gs\glslangValidator.exe -V -Os -o spv/CINA0b cain_preproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/CINA0m cain_preproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/CINA0s cain_preproc.comp
gs\glslangValidator.exe -V -Os -o spv/DINZ0b dain_postproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/DINZ0m dain_postproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/DINZ0s dain_postproc.comp
gs\glslangValidator.exe -V -Os -o spv/DINA0b dain_preproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/DINA0m dain_preproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/DINA0s dain_preproc.comp
gs\glslangValidator.exe -V -Os -o spv/RSRZ0b realsr_postproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/RSRZ0m realsr_postproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/RSRZ0s realsr_postproc.comp
gs\glslangValidator.exe -V -Os -o spv/RSRZ1b realsr_postproc_tta.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/RSRZ1m realsr_postproc_tta.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/RSRZ1s realsr_postproc_tta.comp
gs\glslangValidator.exe -V -Os -o spv/RSRA0b realsr_preproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/RSRA0m realsr_preproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/RSRA0s realsr_preproc.comp
gs\glslangValidator.exe -V -Os -o spv/RSRA1b realsr_preproc_tta.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/RSRA1m realsr_preproc_tta.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/RSRA1s realsr_preproc_tta.comp
gs\glslangValidator.exe -V -Os -o spv/SMDZ0b srmd_postproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/SMDZ0m srmd_postproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/SMDZ0s srmd_postproc.comp
gs\glslangValidator.exe -V -Os -o spv/SMDZ1b srmd_postproc_tta.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/SMDZ1m srmd_postproc_tta.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/SMDZ1s srmd_postproc_tta.comp
gs\glslangValidator.exe -V -Os -o spv/SMDA0b srmd_preproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/SMDA0m srmd_preproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/SMDA0s srmd_preproc.comp
gs\glslangValidator.exe -V -Os -o spv/SMDA1b srmd_preproc_tta.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/SMDA1m srmd_preproc_tta.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/SMDA1s srmd_preproc_tta.comp
gs\glslangValidator.exe -V -Os -o spv/W2XZ0b waifu2x_postproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/W2XZ0m waifu2x_postproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/W2XZ0s waifu2x_postproc.comp
gs\glslangValidator.exe -V -Os -o spv/W2XZ1b waifu2x_postproc_tta.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/W2XZ1m waifu2x_postproc_tta.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/W2XZ1s waifu2x_postproc_tta.comp
gs\glslangValidator.exe -V -Os -o spv/W2XA0b waifu2x_preproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/W2XA0m waifu2x_preproc.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/W2XA0s waifu2x_preproc.comp
gs\glslangValidator.exe -V -Os -o spv/W2XA1b waifu2x_preproc_tta.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -V -Os -o spv/W2XA1m waifu2x_preproc_tta.comp
gs\glslangValidator.exe -DNCNN_fp16_storage=1 -DNCNN_int8_storage=1 -V -Os -o spv/W2XA1s waifu2x_preproc_tta.comp