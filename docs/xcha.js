var xcha={3:[
0,1,2,
1,2,0,
2,0,1]};

function is2n(v)
{
	var hasdigi=false;
	for(var i=0;i<32;i++)
	{
		if(v==0){return i-1;}
		if((v&1)==1)
		{
			if(hasdigi){return -1;
			} else {hasdigi=true;}
		
		}
		v>>=1;
	}
	return -2;

}

function xorfunc0(v1,v2){return (v1+v2)%gdv;}

function xorfunc2m(v1,v2){return v1^v2;}

function xorfunc_odd(v1,v2){return cur_xormap[v1*gdv+v2];}

var xorfunc=xorfunc0;

function w2n(weistr,muzr=-1)
{
	if(muzr<0){muzr=bgdv;}

	var ztr=weistr.split('-');
	var ztrl=ztr.length;
	var ret=0n;
	var curmul=1n;
	for(var d=0;d<ztrl;d++)
	{
		ret+=BigInt(ztr[d])*curmul;
		curmul*=muzr;
	}
	return ret;

}

function gdv_setup(v)
{
	if(v<=0){return;}
	gdv=v;
	bgdv=BigInt(gdv);

	if((v&1)==1)
	{
		odd_xorchart(gdv);
		xorfunc=xorfunc_odd;
		return;
	}
	
	var is2m=is2n(v);
	if(is2m<0)
	{
		xorfunc=xorfunc0;
		return;
	}
	
	xorfunc=xorfunc2m;
	return;

}

function cbnxcha(v1,v2)
{
	var xc1=xcha[v1];
	var xc2=xcha[v2];
	if(v1==1){return xc2;}
	else if(v2==1){return xc1;}
	var rzwh=v1*v2;
	var rz=odd_clamp4bit(rzwh*rzwh);
	
	for(var y0=0;y0<v1;y0++)
	{
		var y0a=y0*v1;
		var y0abg=y0*v2;
		for(var x0=y0;x0<v1;x0++)
		{
			var x0abg=x0*v2;
			var jidi=xc1[y0a+x0]*v2;
			for(var y1=0;y1<v2;y1++)
			{
				var y1a=y1*v2;
				for(var x1=y1;x1<v2;x1++)
				{
					var cvv=xc2[y1a+x1]+jidi;
					rz[(y0abg+y1)*rzwh+(x0abg+x1)]=cvv;
					rz[(x0abg+x1)*rzwh+(y0abg+y1)]=cvv;
					rz[(y0abg+x1)*rzwh+(x0abg+y1)]=cvv;
					rz[(x0abg+y1)*rzwh+(y0abg+x1)]=cvv;
				}
			}
		}
	}
	xcha[rzwh]=rz;
	return rz;

}


var extype=1;
var x3use=0;

function odd_clamp4bit(allrg,sft=1)
{
	var kallrg = allrg<<sft;
	if(allrg<257){return [4,new Uint8Array(kallrg)];}
	else if(allrg<65537){return [8,new Uint16Array(kallrg)];}
	return [16,new Uint32Array(kallrg)];
}

function x3_xorchat(cot3,rz)
{
switch(x3use) {
	case 0:
		if(rz==1){rz=3; cot3--;}
		else{odd_xorchart(rz,-1);}
		cot3--;
		var bz3=3;
		for(var i=0;i<cot3;i++)
		{
			cbnxcha(bz3,3);
			bz3*=3;
		}
	return cbnxcha(bz3,rz);


	case 1:
		if(rz!=1){odd_xorchart(rz,-1);}
		var bz3=rz;
		var lzt=0;
		for(var i=0;i<cot3;i++)
		{
			lzt=cbnxcha(bz3,3);
			bz3*=3;
		}
	return lzt;
}}

function setupdgtsft(ret)
{
	dgtsft=ret[0];
	bmsk=(1<<dgtsft)-1;
	return ret[1];
}

function odd_xorchart(oddn,xortyp=-1)
{

	print_moddiv=oddn;
	var allrg=oddn*oddn;
	if(xortyp>0){oddn=xortyp;}
	cur_moddiv=oddn;
	var ret=setupdgtsft(odd_clamp4bit(allrg));

	var slb=oddn-3;
	var ksta=3;
	var revksta=slb;
	var rvallrg=allrg-1;
	var lztmod=oddn-1;


	if(!xcha[oddn]){
	cur_xormap=ret.subarray(0,allrg);
	ret=ret.subarray(allrg,allrg<<1);
	
	for(var d=0;d<oddn;d++)
	{
		cur_xormap[d]=d;
		cur_xormap[d*oddn]=d;
		cur_xormap[(d+1)*lztmod]=lztmod;
	}
	
	cur_xormap[rvallrg]=1;
	for(var dkr=1;dkr<slb;dkr++)
	{
		
		var dy=oddn;
		for(var dx=dkr;dx>=1;dx--)
		{
			var ydx=dy+dx;
			cur_xormap[ydx]=ksta;
			cur_xormap[rvallrg-ydx]=revksta;
			dy+=oddn;
		}
		ksta++;
		revksta--;
	}
	
	slb=oddn+2*lztmod;
	rvallrg=oddn+lztmod;
	ksta=lztmod*oddn-1;
	for(var d=2;d<lztmod;d++)
	{
		cur_xormap[slb]=1;
		cur_xormap[rvallrg]=d;
		cur_xormap[ksta+d]=d;
		slb+=lztmod;
		rvallrg+=oddn;
	}
		xcha[oddn]=cur_xormap;
	} else {
		cur_xormap=xcha[oddn];
		ret=ret.subarray(allrg,allrg<<1);}
	
	ret[0]=0;
	slb=oddn>>1;
	ret[slb]=(lztmod<<dgtsft)|(slb-1);
	slb-=1;
	rvallrg=slb;
	ksta=2;
	revksta=3;
	var nxidx=slb+(oddn>>1);
	var nxvv=oddn-3;
	var ydridx=oddn*slb+lztmod-1;
	ret[lztmod]=(1<<dgtsft)|(nxvv+1);
	for(var d=0;d<slb;d++)
	{
		ret[rvallrg]=ksta<<dgtsft|cur_xormap[ydridx];
		ret[nxidx]=(revksta<<dgtsft)|nxvv;
		rvallrg--;
		nxidx--;
		nxvv--;
		ksta+=2;
		revksta+=2;
		ydridx-=oddn;
	}
	


	return ret;

}