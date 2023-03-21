var deg45gridsft=5; //=32px

function uintsqarr()
{
	allrg=1<<(hflv<<1);
	if(hflv<5){
		return new Uint8Array(allrg);
	} else if(hflv<9){
		return new Uint16Array(allrg);
	}
	return new Uint32Array(allrg);
	
}

function uintsqarr_withp(level)
{
	allrg=1<<(level);
	sqrg=1<<(level-1);
	if(level<9){
		return new Uint8Array(sqrg);
	} else if(level<17){
		return new Uint16Array(sqrg);
	}
	return new Uint32Array(sqrg);
	
}

function uintsqarr_wh(wh)
{
	allrg=wh*wh;
	if(wh<17){
		return new Uint8Array(allrg);
	} else if(wh<257){
		return new Uint16Array(allrg);
	}
	return new Uint32Array(allrg);
	
}

function mkmgsq_even()
{
	var retsq=uintsqarr();
	var lztidx=allrg-1;
	var hfallrg=allrg>>1;
	for(var i=0;i<hfallrg;i++)
	{
		var tztchg=(i^(i>>hflv));
		tztchg = (tztchg^(tztchg>>1))&1;
		
		var rvidx=lztidx-i;
		if(tztchg != 0){
			retsq[i]=rvidx;
			retsq[rvidx]=i;
		} else {
			retsq[i]=i;
			retsq[rvidx]=rvidx;
		}
	}
	return retsq;
}

function mkmgsq_odd(level)
{
	var retsq0=uintsqarr_withp(level);
	var retsq1=uintsqarr_withp(level);

	var lztidx=allrg-1;
	var hfallrg=allrg>>2;
	var odd_hf=level>>1;
	var i=0;
	
	for(var tk0=0;tk0<2;tk0++)
	{
		for(var tk1=0;tk1<hfallrg;tk1++){
			var tztchg=(i^(i>>odd_hf));
			tztchg = (tztchg^(tztchg>>1))&1;
			var rvidx=lztidx-i;
			if(tztchg != 0){
				retsq0[i]=rvidx;
				retsq0[rvidx-sqrg]=i;
			} else {
				retsq0[i]=i;
				retsq0[rvidx-sqrg]=rvidx;
			}
			i++;}
		var tmp=retsq0;
		retsq0=retsq1;
		retsq1=tmp;
	}
	
	
	return [retsq0,retsq1];
}

function mkmgsq(level)
{
	

	if((level&1)==1){return mkmgsq_odd(level);}
	hflv=(level>>1);
	return mkmgsq_even();

}

function mkmgsq_4x(wh)
{
	var retsq=uintsqarr_wh(wh);
	var lztidx=allrg-1;
	for(var y=0;y<wh;y++)
	{
		var y0a=y*wh;
		for(var x=0;x<wh;x++){
		var i=y0a+x;
		var tztchg=(i^y);
		tztchg = (tztchg^(tztchg>>1))&1;
		
		var rvidx=lztidx-i;
		if(tztchg != 0){
			retsq[i]=rvidx;
			retsq[rvidx]=i;
		} else {
			retsq[i]=i;
			retsq[rvidx]=rvidx;
		}}
	}
	return retsq;
}

function mkmgsq_hfrg(level)
{
	var pslv=is2n(level);
	if(pslv<0)
	{
		if((level&3)==0)
		{
			
			var ret = mkmgsq_4x(level);
			strliz=strliz2f;
			allrg=level;
			return ret;
		}
	}
	return mkmgsq(pslv);

}

function intsqrt2(v)
{
	for(var i=0;i<32;i++)
	{
		if((v&1)!= 0){return i>>1;}
		v>>=1;
	}
	return 0;
}

function oddsq_sum(level)
{
	return ((1<<level)-1)<<((level-2)-(level>>1));
}

function setupdeg45(wh,cot=1)
{
	ybs=wh-1;
	xmul=wh<<1;
	r45syd=xmul-1;
	ymul=r45syd-1;
	var ret=new Array(r45syd*r45syd*cot);
	ret.fill(-1);
	return ret;
	
}

function arr_deg45(wh,src,buf,sta_row=0)
{
	var ybs0=ybs;
	if(sta_row!=0){ybs0+=r45syd*sta_row;}

	for(var y=0;y<wh;y++)
	{
		var y0a=y*wh;
		for(var x=0;x<wh;x++)
		{
			buf[ybs0+y*ymul+x*xmul]=src[y0a+x];
		}

	}
	return buf;

}

function txt_deg45arr(src,cot)
{
	var yh=r45syd*cot;
	var rows=new Array(yh);
	for(var y=0;y<yh;y++)
	{
		var y0a=y*r45syd;
		var cols=new Array(r45syd);
		for(var x=0;x<r45syd;x++)
		{
			var v=src[y0a+x];
			if(v<0){cols[x]=' -';
			} else { cols[x]=' '+v; }
		}
		rows[y]=cols.join(' ');
	}
	return rows.join('\n');

}



function img_deg45arr(src,cot)
{
	var gridwh=1<<(deg45gridsft-1);
	var xsftmid=gridwh>>1;
	var canvw=r45syd<<deg45gridsft;
	var canvh=canvw*cot;
	canv.width = canvw;
	canv.height = canvh;
	ctx.clearRect(0, 0, canvw, canvh);
	var yh=r45syd*cot;
	for(var y=0;y<yh;y++)
	{
		var y0a=y*r45syd;
		for(var x=0;x<r45syd;x++)
		{
			var v=src[y0a+x];
			if(v>=0){
				var fystr=v.toFixed(0);
				var zhu=fystr.length;
				var rw=gridwh/zhu;
				var xsft=(x<<deg45gridsft)+xsftmid;
				var ysft=(y<<deg45gridsft)+10;
				for(var i=0;i<zhu;i++)
				{
					var rna=fystr.charCodeAt(i)-0x30;
					ctx.drawImage(numbs, rna*9, 0, 9, 9, xsft+i*rw, ysft, rw, 9);
				}
			}
		}
	}



}

function printmgsq_deg45(src,print_type=1)
{
	var srg=src.length;

	if(srg<16) {
		var bsft=intsqrt2(src[0].length);
		var wh=1<<bsft;
		var rotdeg45 = setupdeg45(wh,cot=srg);
		for(var kk =0;kk<srg;kk++)
		{
			arr_deg45(wh,src[kk],rotdeg45,sta_row=kk*r45syd);
		}

	} else {
		var bsft=intsqrt2(srg);
		var wh=1<<bsft;
		var rotdeg45=arr_deg45(wh,src,setupdeg45(wh));
		srg=1;
		
	}
	
	switch(print_type)
	{
			case 0:
			return rotdeg45;
			case 1:
			return txt_deg45arr(rotdeg45,srg);
			case 2:
			img_deg45arr(rotdeg45,srg);
			return;
	}

}


var septxt=false;

function strliz0(v)
{
	return v.toString(10);
}


function strliz1(v)
{
	return v.toString(16);
}

function strliz2(v)
{
	return ((v>>hflv)&bmsk).toString(16)+'.'+(v&bmsk).toString(16);
}

function strliz2f(v)
{
	return ((v/allrg)>>0).toString(16)+'.'+(v%allrg).toString(16);
}

var bqa='地雷水澤山火風天';
function strliz3(v)
{
	return bqa.charAt(v);
}

function strliz_none(v)
{
	return v;
}

var strliz=strliz1;

var zspe=' ';
var ztab='\t';
var zlb='\n';

function printmgsq_by4(src)
{
	var srg=src.length;
	if(srg<16) {
		var ret=new Array(srg);
		for(var i=0;i<srg;i++)
		{
			ret[i]=printmgsq_by4(src[i]);
		}
		return ret.join('----------\n');
	} else {
		var bsft=is2n(srg);
		if(bsft<0){return printmgsq_by4f(src);}
		bsft>>=1;
		var grpby4=1<<(bsft-2);
		var ret=[]
		for(var y0=0;y0<grpby4;y0++)
		{
			var y0a=y0<<2;
			for(var y1=0;y1<4;y1++)
			{
				var y1a=(y0a+y1)<<bsft;
				ret1=[];
				for(var x=0;x<grpby4;x++){
					var ydx=y1a+(x<<2);
					ret1.push(strliz(src[ydx])+zspe+strliz(src[ydx+1])+zspe+strliz(src[ydx+2])+zspe+strliz(src[ydx+3]));

				}
				ret.push(ret1.join(ztab));
			}
			ret.push('');
		}
		if(septxt){return ret;}
		return ret.join(zlb);

	}
	


}

function printmgsq_by4f(src)
{
	var srg=src.length;
	if(srg<16) {
		var ret=new Array(srg);
		for(var i=0;i<srg;i++)
		{
			ret[i]=printmgsq_by4f(src[i]);
		}
		return ret.join('----------\n');
	} else {
		var wh=Math.sqrt(srg)>>0;
		var grpby4=wh>>2;
		var ret=[]
		for(var y0=0;y0<grpby4;y0++)
		{
			var y0a=y0<<2;
			for(var y1=0;y1<4;y1++)
			{
				var y1a=(y0a+y1)*wh;
				ret1=[];
				for(var x=0;x<grpby4;x++){
					var ydx=y1a+(x<<2);
					ret1.push(strliz(src[ydx])+' '+strliz(src[ydx+1])+' '+strliz(src[ydx+2])+' '+strliz(src[ydx+3]));

				}
				ret.push(ret1.join('\t'));
			}
			ret.push('');
		}
		if(septxt){return ret;}
		return ret.join('\n');

	}
	


}

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



var sq8=null;



function mgspbit(bitloc)
{
	var ssq=sq8;
	var bbmsk=1<<bitloc;
	var srg=ssq.length;
	var rz=new Array(srg);
	rz.fill('.');
	for(var i=0;i<srg;i++)
	{
		if((ssq[i]&bbmsk)!=0){rz[i]='+';}

	}
	strliz=strliz_none;
	return printmgsq_by4(rz);

}

function sq8rot(hfluv=4)
{
	if(hfluv>4)
	{
		strliz=strliz2;
	}
	var luv=hfluv<<1;

	if(!sq8){
		sq4ord=mkmgsq(hfluv);
		sq8=mkmgsq(luv);
		bmsk=(1<<hflv)-1;
		
	}
	sq8len=1<<luv;
	var nyuout=new Array(sq8len);
	var hfluvd4=hfluv>>1;
	var sqr=1<<hfluvd4;
	var hfluvd4msk=sqr-1;
	var sqrl=1<<hfluv;
	for(var y0=0;y0<sqr;y0++)
	{
		var y0a=y0<<hfluvd4;
		for(var x0=0;x0<sqr;x0++)
		{
			var xofst0=x0<<hfluvd4;
			var idrow=sq4ord[y0a+x0];
			var src_yofst=idrow<<hfluv;
			for(var d=0;d<sqrl;d++)
			{	
				var dst4x4y=sq4ord[d];
				var dst4x4x=dst4x4y&hfluvd4msk;
				dst4x4y=(dst4x4y>>hfluvd4)&hfluvd4msk;
				nyuout[((y0a+dst4x4y)<<hfluv)+(xofst0+dst4x4x)] = sq8[src_yofst+d];
				//nyuout[src_yofst+d] = ((y0a+dst4x4y)<<hfluv)+(xofst0+dst4x4x);	
			}
		}
	}
	sq8=nyuout;
	hflv=hfluv;
	return printmgsq_by4(sq8);

}

function mapshi()
{

setupshi1();

var syz=1<<(hflv<<1);
var ret=new Array(syz);
for(var i=0;i<syz;i++)
{
	var cvv=sq8[i];
	var hgh=(cvv>>hflv)&bmsk;
	var low=cvv&bmsk;
	ret[i]=hflv4shi[	(low<<hflv)+hgh	];
}
return printmgsq_by4(ret);

}


function hflv3toshi(ypt=gka1)
{
	strliz=strliz1;
	var retk=[[],[]];
	for(var i=0;i<64;i++)
	{
		var v=ypt[i];
		var lowr=v%10;
		var hgh=((v-lowr)/10)>>0;
		retk[0][i]=strliz(shihflv3[(hgh<<3)+lowr]);
		retk[1][i]=strliz(shihflv3[(lowr<<3)+hgh]);
	}

	return printmgsq_by4(retk);
}


function hflv3todec(ypt=gka1)
{

	strliz=strliz0;
	var retk=[[],[]];
	for(var i=0;i<64;i++)
	{
		var v=ypt[i];
		var lowr=v%10;
		var hgh=((v-lowr)/10)>>0;
		retk[0][i]=strliz((hgh<<3)+lowr);
		retk[1][i]=strliz((lowr<<3)+hgh);
	}
	return printmgsq_by4(retk);
}

function hflv3tochi(ypt=gka1)
{

	strliz=strliz3;
	var retk=[[],[]];
	for(var i=0;i<64;i++)
	{
		var v=ypt[i];
		var lowr=v%10;
		var hgh=((v-lowr)/10)>>0;
		retk[0][i]=strliz(hgh)+strliz(lowr);
		retk[1][i]=strliz(lowr)+strliz(hgh);
	}
	strliz=strliz_none;
	return printmgsq_by4(retk);
}
function no_parseInt(v){return v;}

function seplv3loc()
{
	strliz=strliz_none;
	var retk=[[],[]];
	for(var i=0;i<64;i++)
	{
		retk[0][i]=parseInt(	lv3loc.charAt(i*2)	);
		retk[1][i]=parseInt(	lv3loc.charAt(i*2+1)	);
	}
	lv3loc_hi=retk[0];
	lv3loc_low=retk[1];
	return; //printmgsq_by4(retk);
	
}
var sq6=null;

function dowalk(src, allrg,loc_hi,loc_low,dst)
{
	for(var i=0;i<allrg;i++)
	{
		dst[(loc_hi[i]<<hflv)+loc_low[i]]=src[i];
	}

}

function tstmgsum(ssq)
{
	var sumtst=0;
	for(var i=0;i<hfrg;i++)
	{
		sumtst+=ssq[i];
	}
	if(sumtst==mgsum){return true;}
	return false;

}

function lv3walk(hfluv=3)
{
	var lv=hfluv<<1;
	var allrg=1<<lv;
	hfrg=1<<hfluv;
	hflv=hfluv;
	if(!sq6)
	{
		hfrg=1<<hflv;
		sq6=[mkmgsq(lv)];
		bmsk=(1<<hfluv)-1;
		mgsum=((1<<lv)-1)<<(hflv-1);
		seplv3loc();
	}
	
	var bsl=sq6.length;
	
	var bslx2=bsl;//bsl<<1;
	var retk=new Array(bslx2);

	for(var i = 0;i<bslx2;i++)
	{
		retk[i]=new Array(allrg);
	}

	for(var i=0;i<bsl;i++)
	{
		dowalk(sq6[i],allrg,lv3loc_low,lv3loc_hi,retk[i]);
		//dowalk(sq6[i],allrg,lv3loc_hi,lv3loc_low,retk[bsl+i]);
		
	}

	var retk2=[]
	for(var i = 0;i<bslx2;i++)
	{
		var retktst=retk[i]
		if(tstmgsum(retktst)){retk2.push(retktst);}
	}


	sq6=retk2;
	strliz=strliz2;
	return printmgsq_by4(retk2);

}

function lv3revloc(hfluv=3)
{
	seplv3loc();
	bmsk=(1<<hfluv)-1;
	var lv=hfluv<<1;
	var allrg=1<<lv;
	hflv=hfluv;
	var ret=new Array(allrg);
	for(var i=0;i<allrg;i++)
	{
		ret[(lv3loc_hi[i]<<hflv)+lv3loc_low[i]]=i;
	}

	strliz=strliz2;
	return printmgsq_by4(ret);
}
var binptn=[
[0,0,0,0],
[0,1,1,0],
[0,0,1,1],
[0,1,0,1],

[1,0,1,0],
[1,1,0,0],
[1,0,0,1],
[1,1,1,1],
[1,1,1,1,1,1,1,1]
];
var mtr8=[
'0110100101011010',
'1001011010100101',

'0110100100111100',
'1001011011000011',

'0011110000110011',
'1100001111001100',

'0101101000001111',
'1010010111110000',

'0110011001101001',
'1001100110010110',

'0110100101100110',
'1001011010011001',
]
var mtr8real=[
0,55,47,24,	32,23,15,56,
0,62,61,3,	4,58,57,7]	//baseswap4
/*
[0,59,55,12,	47,20,24,35,
0,49,54,7,	27,42,45,28];
*/

function xortst()
{
	var ret=new Array(64);
	for(var aaa=0;aaa<64;aaa++)
	{
		var raa=new Array(8);
		var rbb=new Array(8);
		for(var d=0;d<8;d++)
		{
			raa[d]=mtr8real[d]^aaa;
			rbb[d]=mtr8real[8+d]^aaa;
		}
		ret[aaa]='_ '+raa.join(' ')+'\n'+rbb.join(' ')+' _\n\n';
		
	}
	return ret.join('');
}

function calsle6(seleidx)
{
	var upr=new Array(19);
	
	upr.fill(0);
	for(var d=0;d<6;d++)
	{
		var mulp=1<<d;
		var sele=mtr8[seleidx[d]];
		for(var i=0;i<8;i++)
		{
			if(sele.charAt(i)=='1')
			{
				upr[1+i]+=mulp;
			}
			if(sele.charAt(8+i)=='1')
			{
				upr[10+i]+=mulp;
			}
		}
		

	}
	upr[0]='_';
	upr[9]='\n';
	upr[18]='_\n\n';
	return upr.join(zspe);
	
}

function cbn64()
{
	var ret=new Array(64);
	for(var aaa=0;aaa<64;aaa++)
	{
		var seleidx=new Array(6);
		var vv=aaa;
		for(var d=0;d<6;d++)
		{
			var ady=0;
			if((vv&1)!=0){ady=1;}
			seleidx[d]=(d<<1)+ady;
			vv>>=1;
		}
		
		ret.push(calsle6(seleidx));
	}
	document.body.innerText=ret.join('');
}

function ptnwalk()
{
var ret=[];
/*
zspe='';
ztab='';
zlb='';
*/
for(var a1=0;a1<8;a1++)
{
	for(var a2=0;a2<8;a2++)
	{
		var ka= binptn[8].concat(binptn[a1]).concat(binptn[a2]);

		//binptn[a1].concat(binptn[a2]).concat(binptn[8]);	
		//	
		var kastr='\n\n'+ka.join('')+'\n';
		for(var b1=0;b1<8;b1++)
		{
			for(var b2=0;b2<8;b2++)
			{
				var kb=binptn[b1].concat(binptn[b2]).concat(binptn[8]);
				//
				//binptn[8].concat(binptn[b1]).concat(binptn[b2]);
				ret.push(kastr+kb.join('')+'\n');
				ret.push(matmul(ka,kb).join(''));
			}
		}
	}
}
document.body.innerText=ret.join('');

}

function gf2mad(ysle,xsle)
{
	var sql=ysle.length;
	var ret=ysle[0]&xsle[0];
	for(var d=1;d<sql;d++)
	{
		ret^=(ysle[d]&xsle[d]);
	}
	return ret;

}

function matmul(v1,v2,n=8)
{
	var kw=(v1.length/n)>>0;
	var ret =new Array(n*n);
	for(var y=0;y<n;y++)
	{
		var ysle=new Array(kw);
		for(var d=0;d<kw;d++)
		{
			ysle[d]=v1[d*n+y];
		}
		for(var x=0;x<n;x++)
		{
			var xsle=new Array(kw);
			for(var d=0;d<kw;d++)
			{
				xsle[d]=v2[d*n+x];
			}
			ret[y*n+x]=gf2mad(ysle,xsle)
		}
	}
	strliz=strliz0;
	return ret;
	//return printmgsq_by4(ret);

}

