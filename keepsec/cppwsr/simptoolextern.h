
typedef struct scalpp
{
	float DstSize;
	char model;
	char ux1;
	char ux2;
	char ux3;
} ScaleParam;

enum
{
	_first8to32_ = 1,
	_mid32to32_ = 0,
	_end32to8_ = 2

};

extern int stepinit = 0;
extern ScaleParam ScaleSteps[8];
extern void FillScaleParam(ScaleParam* dst, float skale);

