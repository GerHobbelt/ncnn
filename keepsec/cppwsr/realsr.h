// realsr implemented with ncnn library
#define DLL_EXPORT extern "C" __declspec(dllexport)

#ifndef REALSR_H
#define REALSR_H

#include <string>

// ncnn
#include "net.h"
#include "gpu.h"
#include "layer.h"

class ncnnNetPack
{
public:
	ncnn::Net net;
	int scale;
	int prepadding;
	int noise;
};


class RealSR
{
public:
    RealSR(int gpuid, bool tta_mode = false);
    ~RealSR();

    int load(const int model, int scale, int noise);
	int load(const int model, int scale, int noise, char dst);

    int process(const ncnn::Mat& inimage, ncnn::Mat& outimage, const ScaleParam sparam, const int InOutType) const;

public:
    // realsr parameters


    int tilesize;
    ncnnNetPack nets[3];


private:
    ncnn::Pipeline* realsr_preproc[2];
    ncnn::Pipeline* realsr_postproc[2];
    ncnn::Layer* bicubic_4x;
    bool tta_mode;
};

#endif // REALSR_H
