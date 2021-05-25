
var PozMode=0x2;
const pozimg='<img src="poz.svg" />';
const pozinput='<input type="number" value=0 onfocus=disabsk() onblur=fpt(this)>';
const pozinputN='<input type="number" value=50 onfocus=disabsk() onblur=repg(this)>';


var tblarea = document.getElementById('team');
var recarea = document.getElementById('urlrec');
var timgarea = document.getElementById('timg');
var pozcur=new Array(2);
var pozcurpic=timgarea.nextSibling;
var SVGf=timgarea.previousSibling.firstChild;

var selefocus=null;

var erocount=0;
var keyerocount=0;
var hardlim=0;
var klyi=0x0;
var fpgMode=1;


var RDMarr=null;
var iRDMarr=0xF00000;




function flazt20(arr,num)
{
for(i=0;i<8;i++) {arr[num+i]=arr[i];}
return arr;
}

function ShuffleArray(arr)
{var ll=arr.length-8;

zsta=ll>>1;
for(i=0;i<zsta;i++)
{
	y=i<<1;
	nx=1+((Math.random() *zsta)<<1);
	tmp=arr[y];
	arr[y]=arr[nx];
	arr[nx]=arr[y+1];
	arr[y+1]=tmp;
}

ibz=2;
if(ll&1){ibz=3;}

for(i=1;i<ibz;i++)
{
	zsta=ll-i;
	nx=(Math.random() *ll)>>1;
	tmp=arr[nx];
	arr[nx]=arr[zsta];
	arr[zsta]=tmp;
}


return flazt20(arr,ll);}

function chkdst(arr,y)
{
	var adst=arr[y];
	if(adst===undefined){return y;}
	return adst;
}



function mkRDMarr(num)
{var arr=new Array(num+8);
var zsta=(num>>1);
for(var i=0;i<zsta;i++)
{
	var nx=1+((Math.random() *zsta)<<1);

	var y=i<<1;
	

	//adst=arr[nx];
	//if(adst===undefined){adst=nx;}
	arr[y]=chkdst(arr,nx);//adst;

	var y1=y+1;

	//adst=arr[y1];
	//if(adst===undefined){adst=y1;}
	arr[nx]=chkdst(arr,y1);//adst;

	arr[y1]=y;
	
}


var yz=num-2;

if(num&1){
yz=num-3;
zsta=num-1;
nx=(Math.random() *num)>>1;
arr[zsta]=arr[nx];
arr[nx]=zsta;
zsta=(num>>1);
}

zsta=(zsta<<1)-1;
if(arr[zsta]==yz)
{
nx=(Math.random() *num)>>1;
arr[zsta]=arr[nx];
arr[nx]=yz;
}

return flazt20(arr,num);}

function chaglims()
{klyi=0x0;
keyerocount=curEro+50;
hardlim=curEro+501;}

function RDMcurEro()
{
	if(iRDMarr>=erocount)
	{
		
		if(iRDMarr==0xF00000){RDMarr=mkRDMarr(erocount);}
		else{RDMarr=ShuffleArray(RDMarr);}

		iRDMarr=-1;
	}
	
	iRDMarr++;
}

function findimgerr()
{
	var imgsetz=document.getElementsByTagName("img");
	var imgsetzl=imgsetz.length;
	var zsrcrec="";
	for(var i=0;i<imgsetzl;i++)
	{
	if(imgsetz[i].naturalWidth==0)
	{
		var srck=imgsetz[i].src;
		if(srck.slice(-6)==':thumb')
		{
			zsrcrec+='\n'+srck.slice(0,-6);
		}
		
	}
	
	}
	recarea.value+=zsrcrec;

	return zsrcrec;

}

const SVGheader='http://www.w3.org/2000/svg';

function goodxyele(x,y)
{
	//recarea.value+='\nAT '+x+', '+y;
	var tele=document.elementFromPoint(x,y);
	if(tele)
	{
		var c0=tele.tagName.charCodeAt(0)
		switch(c0)
		{
			case 73:	//I
			case 65:	//A
			return tele;

		}
	}
	return null;
}

const area8=[2,0,-2,-2,-2, 0, 2,2,	//x
	1,1, 1, 0,-1,-1,-1,0];	//y

function searchxy(x,y)
{
	var tele=goodxyele(x,y);
	if(tele){return tele;}


	var hlim=window.innerHeight;
	var wlim=window.innerWidth;
	for(var m=128;m<1024;m+=128)
	{
		for(var d=0;d<8;d++)
		{
			var nx=x+area8[d]*m;
			var ny=y+area8[d+8]*m;

			if(nx<0||nx>wlim||ny<0||ny>hlim){continue;}
			tele=goodxyele(nx,ny);
			if(tele){return tele;}
		}
	}



	return null;
	

}
const focusborder='5px solid #ff0';

function simpfocus()
{
	if(selefocus)
	{
		selefocus.style.border = focusborder;
		selefocus.scrollIntoView();
		setTimeout(function(){selefocus.style.border=null;}, 10000);
		return selefocus;
	}
	return null;
}

function pPick(coord)
{
	var x;
	var y;
	if(coord)
	{
		x=coord[0];
		y=coord[1];
	}
	else
	{
		x=window.outerWidth>>1;
		y=window.outerHeight>>1;
	}
	selefocus=searchxy(x,y);
	return simpfocus();
	
}

function ParseSVGblock(sst)
{
SVGf.insertAdjacentHTML('beforeend',sst);
var fltr_cot=SVGf.children.length;
var fltr_names="";
for(var i=2;i<fltr_cot;i++)
{
	fltr_names+='\n>>'+SVGf.children[i].id;
}

recarea.value=fltr_names;
}
function setNscroll(sst)
{
	selefocus.style.border = '10px dotted #fff';
	selefocus.style.webkitFilter='url(#'+sst+')';
	selefocus.scrollIntoView();
	setTimeout(function(){selefocus.style.border=null;}, 5000);
}

function pSVG()
{
	
	if(!selefocus){return;}
	var sst=recarea.value;
	if(sst.charCodeAt(1)==62){
			
			if(sst.charCodeAt(2)==64){
				ParseSVGblock(sst.split('@')[1]);
				
				return;
			}
			
			var syp=sst.indexOf('\n');
			if(syp==2){selefocus.style.webkitFilter=null;return;}
			else if(syp>2){sst=sst.substring(2,syp);}
			else{sst=sst.substring(2);}
			setNscroll(sst);
			return;
		}
	
	
	
	var syp= sst.indexOf('\n');
	var ftr_id=sst.substring(1,syp);

	var prev=document.getElementById(ftr_id);
	if(prev){prev.innerHTML=sst.substring(syp);}
	else{
		var ftr_elem=document.createElementNS(SVGheader, 'filter');
		ftr_elem.id=ftr_id;
		ftr_elem.innerHTML=sst.substring(syp);
		SVGf.appendChild(ftr_elem);
		recarea.value='>>'+ftr_id;
	}
	setNscroll(ftr_id);
	

}

function prtcsvll(i)
{
	return thumb[i]+'\t'+vids[i]+'\t'+msgs[i]+'\n';
}

function printsele()
{
	var lynz=recarea.value.split('\n');
	var lynzl=lynz.length;
	var zsrcrec="\n\n======\n\n";
	for(var i=0;i<lynzl;i++)
	{
		var c0=lynz[i].charCodeAt(0);
		if(c0==58)
		{
			var yr=parseInt(lynz[i].substring(1),10);
			zsrcrec+=prtcsvll(yr);

		}
		
	}

	recarea.value+=zsrcrec;
	return zsrcrec;

}

var FuncList = {'svg':pSVG,'p':pPick};
function kall(funcname,param)
{
	return FuncList[funcname](param);
}

function disabsk()
{
	if(keyerocount<0){keyerocount=-200;}
	else{keyerocount=0;}
}


const nwvv1='<html><body style="background-color: black;"><center><video width="auto" id="media" controls loop muted autoplay><source src="';
const nwvv2='" type="video/mp4"></video></center></body><scr'+'ipt src="zvid.js"></scr'+'ipt></html>';

function nwdvid(vidurl)
{
	var newWindow = window.open(vidurl, null, null);

	//var newWindow = window.open("", null, null);
	//newWindow.document.write(nwvv1+vidurl+nwvv2);

}

var canfire=true;

function repg(ele){repgv(ele.value);}

function repgv(elevalue)
{
	
	timgarea.src='';
	timgarea.style.maxHeight = '1%';

	if(keyerocount<0)
	{
		keyerocount=-100;
		curEro=parseInt(elevalue,10);
		klyi=0x400;
		setTimeout(fullpgALL[fpgMode], 0);
		return;
	}


	
	evv=curEro+((parseInt(elevalue,10)*5)>>2);
	if(evv<0){evv=0;}
	else if(evv>erocount){evv=erocount-8;}

	
		curEro=evv;
		
		chaglims();
		tblarea.innerHTML=bz1+curEro+bz2;
		
		
		menuFunction();
	

}

const vtwsig=	[0,				64/*@*/,				33/*!*/,			43/*+*/];
const vtwimg=['https://pbs.twimg.com/ext_tw_video_thumb/',	'https://pbs.twimg.com/amplify_video_thumb/',	'https://pbs.twimg.com/tweet_video_thumb/','https://pbs.twimg.com/media/'];
const vtwvid=['https://video.twimg.com/ext_tw_video/',		'https://video.twimg.com/amplify_video/',		'https://video.twimg.com/tweet_video/'];


function vidstr(src)
{
	var c0=src.charCodeAt(0);
	for(var i=1;i<3;i++){if(c0==vtwsig[i]){return vtwvid[i]+src.substring(1);}}
	return vtwvid[0]+src;
}

function thumbstr(src)
{
	var c0=src.charCodeAt(0);
	for(var i=1;i<4;i++){if(c0==vtwsig[i]){return vtwimg[i]+src.substring(1);}}
	return vtwimg[0]+src;
}

const bz1='<h1><a href="0bak/bkero.html">#.';
const bz2='</a></h1>';


const kx1='<br><a href="https://twitter.com/';
const kx1t='<a href="https://twitter.com/';
const kx2b1='" >==========<br>==>';
const kx2b2='<==</a><br><a href="';
const kx2a='" >====</a><a onmouseover=xt(this)>i';
const kx3='</a><br><a href="';
const kx3t='<a href="';
const kx4='" ><img src="';
const kx5a=':thumb" width=211 /></a>.';
const kx5b='" /></a>';
const kx5t=':thumb" /></a><a href="https://twitter.com/';
const kx5t2='" > #= </a><a href="https://twitter.com/';
const kx5t3='" width=200 /></a>';
const kx5bl='" width=75% /></a>';
const aTAGend='</a>';

function SetPozMode00()
{
	switch(PozMode)
	{
		case 0:
		pozcurpic.innerHTML=pozinputN;
		pozcurpic.className='nv';
		pozcurpic.style.top=null;
		pozcurpic.style.left=0;
		pozcurpic.style.bottom=10;
		return;
		//case 0x100:
		//return;
	}
}

function SetPozMode()
{
	
	switch(PozMode)
	{
		case 0x1:
		pozcurpic.innerHTML=pozimg;
		pozcurpic.className='pz';
		pozcurpic.style.bottom=null;
		return;
		case 0x2:
		pozcurpic.innerHTML=null;
		pozcurpic.className='nv';
		pozcurpic.style.bottom=null;
		return;
		case 0x101:
		pozcurpic.innerHTML=pozinput;
		pozcurpic.className='nvmid';
		pozcurpic.style.bottom=null;
		pozcurpic.style.left=5;
		return;
		default:
		PozMode=(PozMode&0xffffff00);
		return SetPozMode00();
	}
}

function mydav()
{
	var yina = document.createElement('FL');
	//yina.className = 'FL';

	var kole7='=';
	var zko=(curEro<<3);
	var endo=zko+8;
	for(var iszrda=zko;iszrda<endo;iszrda++){
kole7+=kx1+msgs[iszrda]+
kx2a+iszrda+
kx3+vidstr(vids[iszrda])+
kx4+thumbstr(thumb[iszrda])+
kx5a;

	}
	yina.innerHTML = kole7;
	tblarea.appendChild(yina);
}



var ovrcount=0;

function skrback()
{
nymu=-1;
document.body.background='';
document.body.scrollTop+=20;
document.body.scrollLeft=0;
}

var nymu=-10;


function xtp()
{

	if(ovrcount==0)
	{
		//tblarea.appendChild(timgarea);
		var tsstr=thumbstr(thumb[nymu]);
		document.body.background = tsstr;
		timgarea.src=tsstr;
		timgarea.style.maxHeight = '800%';
	}

	recarea.value+='\n:'+nymu;
	document.body.scrollLeft+=2000;
	setTimeout(skrback, 1000);
	//recarea.value+='\nhttps://twitter.com/'+msgs[nymu];
}



function xt(ele)
{


var c0=ele.innerText.charCodeAt(0);


var iszrda = parseInt(ele.innerText.substring(1),10);


	if(nymu!=iszrda)
	{
		nymu=iszrda;
		ovrcount=0;
		
		switch(c0)
		{
			case 103:	//g
				timgarea.src=thumbstr(thumb[iszrda]);
			return;
			case 105:	//i
			{
				var ymgg=ele.nextSibling.nextSibling.firstChild;
				if(ymgg.naturalWidth==0)
				{
					ymgg.alt=msgs[iszrda];
					ymgg.src=thumbstr(thumb[iszrda]);
					ymgg.height=ymgg.width;
					ele.innerText='r'+iszrda;
					
				}
				else
				{
					ele.innerText='g'+iszrda;
					
				}
				nymu=-10;
			}
			return;
			case 114:	//r
			{
				var ymgg=ele.nextSibling.nextSibling.firstChild;
				if(ymgg.naturalWidth==0)
				{
					var niki=msgs[iszrda].split('/')[0];
					
					
					if(niki=='i'){niki=' ';}
					else{ymgg.alt='\n:'+niki+'\n';
					niki=' href="https://nitter.kavin.rocks/'+niki+'/media" '; //' href="https://twitter.com/'+niki+'/with_replies"';
					}
					ele.onmouseover=null;
					ele.onclick=null;
					ele.outerHTML='<a'+niki+'>...X'+iszrda+aTAGend;
					nymu=-10;
				}
				else
				{
					ymgg.alt=null;
					ele.innerText='g'+iszrda;
					timgarea.src=ymgg.src;
				}
				
				
			}
			return;

		}

		

		
		
		
		
		
		
	}
	else
	{
	
		if(ovrcount==0)
		{
			//tblarea.appendChild(timgarea);
			
			document.body.background =timgarea.src;
			//timgarea.style.maxHeight = '800%';
			
		}
		else if(ovrcount==1){
			document.body.scrollLeft+=2000;
			setTimeout(skrback, 1000);
			

		}
		ovrcount++;
	}
}

function rmvimg()
{
if(nymu>=0){
	//timgarea.src='';
	//timgarea.style.maxHeight = '1%';
	nymu=-1;
}
}

function apyed()
{
	mydav();
	
	//mydav(0,4,0);
	//mydav(4,6,1);
	//mydav(6,10,0);
	
	
	curEro++;
	
}



function menuFunction() {

	if(curEro<erocount){apyed();
	rmvimg();
	keyerocount=curEro+50;
	if(keyerocount>erocount){keyerocount=erocount;}
	canfire=true;
	document.title=curEro + "0 to "+ keyerocount+'0 key';}
	
	return false;
	
}



function symfire()
{
	apyed();
	canfire=true;
}
function setPozCur(x,y)
{
	pozcur[0]=x;
	pozcur[1]=y;
	pozcurpic.style.left=x-32;
	pozcurpic.style.top=y-32;
}

function setPozCur2(y)
{
	pozcur[1]=y;
	pozcurpic.style.top=y-5;
}

function PozCurKlicK()
{document.elementFromPoint(pozcur[0],pozcur[1]).click();}

var mouseRDM=false;


function symfireCon(t)
{
if(mouseRDM){
RDMcurEro();
setTimeout(mydavRDM, t);
return;
}
else if(curEro>=hardlim){return;}


setTimeout(symfire, t);

}

var kuriakey=function(ev){
	if(canfire)
	{
		canfire=false;
		
		if(PozMode==1){setPozCur(ev.clientX,ev.clientY);}
		
		symfireCon(1200);

		

		keyerocount=curEro+50;
		
		
		
	}
	
}

var settrue=function(){canfire=true;}


var kuriakeysimp=function(ev){
	klyi++;
	if(klyi>0x20) {
	klyi=0x0;
	setPozCur2(ev.clientY);
	}
			
	
	
}

function fpt(ele){fptv(ele.value);}

function fptv(elevalue)
{


keyerocount=-100;
var evv=((parseInt(elevalue,10)*5)>>2);
	if(evv<0){
		iRDMarr+=evv;
		if(iRDMarr<=0)
		{
			iRDMarr=0;
			//fullpg();
			//return;
		}
		
	}
	else
	{
		if(evv>erocount){evv=erocount>>1;}
		iRDMarr=evv;
	}



fullpg();


}
function partpg()
{
	var yina = document.createElement('center');
	var kole7='<h1>++'+curEro+'++</h1>';
	var zko=(curEro<<3);
	var endo=zko+16;
	
	for(var iszrda=zko;iszrda<endo;iszrda++){
		kole7+=kx1+msgs[iszrda]+
		kx2b1+iszrda+kx2b2+vidstr(vids[iszrda])+
		kx4+thumbstr(thumb[iszrda])+
		kx5b;
	}
	yina.innerHTML = kole7;
	tblarea.appendChild(yina);

	curEro+=2;
}

function mydavRDM()
{
	var yina = document.createElement('div');
	yina.className = 'fl';

	
	var kole7=iRDMarr.toString(16);
	var xma=(iRDMarr&7);
	var endo=iRDMarr+8;
	for(var j=iRDMarr;j<endo;j++){
var iszrda=(RDMarr[j]<<3)+xma;
kole7+=kx1+msgs[iszrda]+
kx2a+iszrda+
kx3+vidstr(vids[iszrda])+
kx4+thumbstr(thumb[iszrda])+
kx5a;

	}
	yina.innerHTML = kole7;
	tblarea.appendChild(yina);

	canfire=true;
}



var dsmall=function(e){
var ele=e.target;
var tgg=ele.tagName;
if(tgg=='IMG')
{

if(fpgMode==1)
{
	if(ele.src.endsWith(':thumb'))
	{
		ele.src=ele.src.slice(0,-6);
		return false;
		
	}
	else if(ele.width&&(ele.width<201))
	{
		ele.width=100;
		if(ele.naturalWidth!=0){ele.style.width='auto';}
	}
	else
	{
		document.body.background=ele.src;
		setTimeout(function(){ele.src=ele.src+':thumb';}, 1500);
	}
	return true;
	

}	


if(ele.src.endsWith('.jpg'))
{
	var naa=ele.alt;
	if(naa)
	{
	ele.onmouseover=null;
	ele.onclick=null;
	var sig=parseInt(naa,10);
	var vidurl=vidstr(vids[sig]);
	var thumburl=ele.src;
	ele.outerHTML=kx3t+vidurl+kx4+thumburl+kx5t+msgs[sig]+'" >@'+sig+aTAGend;
	
	}
	else if(ele.width==200)
	{
		ele.width=100;
		if(ele.naturalWidth!=0){ele.style.width='auto';}
	}
	else
	{
		ele.width=200;
		ele.style.width=200;
	}
	return false;
}

return true;
}
else if(tgg=='A'){return true;}

document.body.scrollTop+=1000;
return false;
}

var kv=function(e){
var ele=e.currentTarget;
var sig=parseInt(ele.alt,10);
var vidurl=vidstr(vids[sig]);
var thumburl=ele.src;

window.open(vidurl, null, null);
ele.onmouseover=null;
ele.onclick=null;
if(ele.naturalWidth){ele.outerHTML=kx1+msgs[sig]+kx2b1+sig+kx2b2+vidurl+kx4+thumburl+kx5b;}
else{ele.outerHTML=kx3t+vidurl+kx5t2+msgs[sig]+'" >'+sig+aTAGend;}




}

const llgif='0bak/longbar.png';

function hv2(ele)
{
ele.style.webkitFilter='';
ele.src=thumbstr(thumb[parseInt(ele.alt,10)]);
ele.onmouseover=null;
ele.onclick=kv;
}

function llimgThis(ele,nx,depth)
{
	if(ele)
	{
		var tgg=ele.tagName;
		var isIMG=(tgg=='IMG');
		if(isIMG&&ele.src.endsWith('/poz.png')) {
		ele.src=llgif;
		ele.style.webkitFilter='url(#longbar)';
		}
		else if(depth<5)
		{
			
			
			depth++;
			if(isIMG)
			{
				if(ele.src.endsWith(llgif))
				{
				hv2(ele);
				klyi-=0x200;
				return;
				}
			}
			else
			{
				depth--;
				if(tgg=='B')
				{
				
				var nux=null;
				if(nx){nux=ele.nextSibling;}
				else {nux=ele.previousSibling;}
				ele.remove();
				if(nx){llimgThis(nux,true,depth);}
				else{llimgThis(nux,false,depth);}
				return;
				}
				
			}

			
			if(nx){llimgThis(ele.nextSibling,true,depth);}
			else{llimgThis(ele.previousSibling,false,depth);}
			return;

		}
	}
	
	klyi-=0x200;
}

var kv_s=function(e){
var ele=e.currentTarget;
var sig=parseInt(ele.alt,10);
var vidurl=vidstr(vids[sig]);
var thumburl=ele.src;

window.open(vidurl, null, null);


ele.onmouseover=null;
ele.onclick=null;
ele.outerHTML=kx3t+vidurl+kx4+thumburl+kx5b;



}

var hv_s=function(e)
{
if(klyi&1)
{klyi=0x400;
return;}

klyi=0x401;

var ele=e.currentTarget;
var sig=ele.alt;
var c0=sig.charCodeAt(0);
if(c0==120)	//==x
{
	sig=sig.substring(1);
	ele.alt=sig;
	ele.style.webkitFilter='';
	ele.src=thumbstr(thumb[parseInt(sig,10)])+':thumb';
	ele.onclick=kv_s;
	return;
}

ele.onmouseover=null;
if(ele.naturalWidth==0)
{
ele.onmouseover=null;
ele.onclick=null;
var sigN=parseInt(sig,10);
var thumburl=ele.src.slice(0,-6);

ele.outerHTML=kx3t+vidstr(vids[sigN])+kx4+thumburl+kx5t3;

}


return;




}

var hv =function(e) {
var ele=e.currentTarget;
if(klyi<0x200){klyi=0x400;}

klyi+=0x1FF;
setTimeout(function(){llimgThis(ele.nextSibling,true,0);}, klyi);
klyi+=0x200;
setTimeout(function(){llimgThis(ele.previousSibling,false,0);}, klyi);

ele.style.webkitFilter='';
ele.src=thumbstr(thumb[parseInt(ele.alt,10)]);
ele.onclick=kv;
ele.onmouseover=null;
document.body.scrollTop+=100;

}
const mgx1sma='</a><img class=tFlo alt=x';
const mgx1='<img style="-webkit-filter: url(#cur)" src="poz.png" alt=';
const mgx2=' title=';
const mgx3=' />';	// onmouseover=hv(this) onclick=kv(this)
const cxh1a='</center><h1>*****8x';
const cxh1b='***</h1><center id=';

function sixbkmark(dvvsta)
{
	var sugg=8+(dvvsta<<3);
	var stiaa="";
	for(var i=0;i<6;i++)
	{
		stiaa+='<br><a href="#'+sugg+'x" target=_self >**8x'+sugg+'**</a>';
		sugg+=8;
	}
	return stiaa+'</h1>';
}

function asgn()
{
	document.body.scrollLeft=100;
	klyi=0x400;
	var ymgs=document.getElementsByTagName('img');
	var ymgsl=ymgs.length;
	for(var i=1;i<1025;i++){ymgs[i].onmouseover=hv;}
	
}

function asgn_s(ym)
{
	document.body.scrollLeft=0;
	tblarea.innerHTML=ym.join(' =');
	var ymgs=document.getElementsByTagName('img');
	var ymgsl=ymgs.length;
	for(var i=1;i<1025;i++){ymgs[i].onmouseover=hv_s;}
}

function fullpgALLcur0()
{


var dvvsta=(curEro>>3);
var dvvendo=dvvsta+8;
var kole7='<center><h1>Serial<br>*<br>*<br>*<br>*<br>512+64*'+dvvendo+sixbkmark(dvvsta);//'<br>*<br>*<br>*<br>*<br>*<br>*</h1>';

for(var jj=dvvsta;jj<dvvendo;jj++)
{
	
	var zko=jj<<3;
	var endo=zko+8;
	kole7+=cxh1a+zko+cxh1b+zko+'x >';
	for(var jjx=0;jjx<8;jjx++)
	{
		
		kole7+='<b><br>+'+jjx+'</b>';
		
		
		for(var j=zko;j<endo;j++){
			var iszrda=(j<<3)+jjx;
			kole7+=mgx1+iszrda+mgx3;
			
			iszrda=((j+64)<<3)+jjx;
			kole7+=mgx1+iszrda+mgx3;
		}
	}

}

tblarea.innerHTML=kole7+'</center>';
curEro=(dvvendo+8)<<3;

asgn();

}

function fullpgALLcur1()
{
	
	var dvvsta=(curEro>>3);
	var dvvendo=dvvsta+8;
	var pidx=0;
	var ym=new Array(1024);
for(var jj=dvvsta;jj<dvvendo;jj++)
{
	var zko=jj<<3;
	var endo=zko+8;
	for(var jjx=0;jjx<8;jjx++)
	{
		for(var j=zko;j<endo;j++){
			var iszrda=(j<<3)+jjx;
			ym[pidx]=kx1t+msgs[iszrda]+'" >'+iszrda+mgx1sma+iszrda+mgx3;
			pidx++;
			
			iszrda=((j+64)<<3)+jjx;
			ym[pidx]=kx1t+msgs[iszrda]+'" >'+iszrda+mgx1sma+iszrda+mgx3;
			pidx++;
		}
	}
}
	
	asgn_s(ym);
	curEro=(dvvendo+8)<<3;
}

function fullpgALLrdm0()
{


var dvvsta=(iRDMarr>>3);
var dvvendo=dvvsta+8;
var kole7='<center><h1>Random<br>*<br>*<br>*<br>*<br>128*'+dvvendo+sixbkmark(dvvsta);//'<br>*<br>*<br>*<br>*<br>*</h1>';

for(var jj=dvvsta;jj<dvvendo;jj++)
{
	
	var zko=jj<<3;
	var endo=zko+8;
	kole7+=cxh1a+zko+cxh1b+zko+'x >';
	for(var jjx=0;jjx<8;jjx++)
	{
		
		kole7+='<b><br>+'+jjx+'</b>';
		
		
		for(var j=zko;j<endo;){
			var iszrda=(RDMarr[j]<<3)+jjx;
			kole7+=mgx1+iszrda+mgx3;
			j++;
			iszrda=(RDMarr[erocount-j]<<3)+jjx;
			kole7+=mgx1+iszrda+mgx3;
		}
	}

}

tblarea.innerHTML=kole7+'</center>';
iRDMarr=dvvendo<<3;

asgn();

}

function fullpgALLrdm1()
{
	
	var dvvsta=(iRDMarr>>3);
	var dvvendo=dvvsta+8;
	var pidx=0;
	var ym=new Array(1024);
for(var jj=dvvsta;jj<dvvendo;jj++)
{

	var zko=jj<<3;
	var endo=zko+8;
	for(var jjx=0;jjx<8;jjx++)
	{
		for(var j=zko;j<endo;){
			var iszrda=(RDMarr[j]<<3)+jjx;
			ym[pidx]=kx1t+msgs[iszrda]+'" >'+iszrda+mgx1sma+iszrda+mgx3;
			pidx++;
			j++;
			iszrda=(RDMarr[erocount-j]<<3)+jjx;
			ym[pidx]=kx1t+msgs[iszrda]+'" >'+iszrda+mgx1sma+iszrda+mgx3;
			pidx++;
		}
	}
}

	asgn_s(ym);
	iRDMarr=dvvendo<<3;
}

var fullpgALLcur=[fullpgALLcur0,fullpgALLcur1];
var fullpgALLrdm=[fullpgALLrdm0,fullpgALLrdm1];

var fullpgALL=fullpgALLcur;

function fullpg()
{
	var kole7='<center><h1>**'+iRDMarr+'**</h1>';


	var xma=(iRDMarr&7);
	var zko=iRDMarr-xma;
	var endo=zko+8;
	for(var j=zko;j<endo;){
		var iszrda=(RDMarr[j]<<3)+xma;
		kole7+=kx1+msgs[iszrda]+
		kx2b1+iszrda+kx2b2+vidstr(vids[iszrda])+
		kx4+thumbstr(thumb[iszrda])+
		kx5b;
		j++;
		iszrda=(RDMarr[erocount-j]<<3)+xma;
		kole7+=kx1+msgs[iszrda]+
		kx2b1+iszrda+kx2b2+vidstr(vids[iszrda])+
		kx4+thumbstr(thumb[iszrda])+
		kx5b;
	}
	
	
	tblarea.innerHTML =kole7+'</center>';
	document.body.scrollLeft=100;
}

function fastrscroll(ev) {return false;}

function fullpgmenu()
{
RDMcurEro();
fullpg();
return false;
}

function chgfpgMode()
{
	if(fullpgALL===fullpgALLcur&&curEro>0x7F){curEro-=0x80;}
	
	fpgMode++;
	if(fpgMode>1){fpgMode=0;}
	document.title='GachaMode '+fpgMode;
}



function dotxcmd()
{
	recarea.rows=5;
	recarea.cols=26;
	recarea.className='';
	recarea.onblur=null;
	document.onkeydown=kycmd;

	var sst=recarea.value;
	if(sst.length==0){return;}
	var c0=sst.charCodeAt(0);
	switch(c0)
	{
		case 33:
		return simpfocus();
		case 62:
		return pSVG();
	}



	sst=sst.replaceAll('\n','').split(',');
	var sstl=sst.length;
	if(sstl==1)
	{
		
		
		if(keyerocount==-100&&klyi<0x100){fptv(sst[0]);}
		else {repgv(sst[0]);}
		
	}
	else if(sstl==2&&sst[1]==''){kall(sst[0],null);}
	else {
		sstl-=1;
		var param=new Array(sstl);
		for(var k=0;k<sstl;k++)
		{
			param[k]=parseInt(sst[k+1],10);
		}
		kall(sst[0],param);
	}
	
	
}

function entertxcmd()
{
	recarea.rows=30;
	recarea.cols=60;
	recarea.className='cmdbox';
	recarea.onblur=dotxcmd;
	document.onkeydown=null;
	recarea.focus();
}

var kycmd=function(e) {

var ekeyCode=e.keyCode;




if(keyerocount==-100){
klyi++;

switch (ekeyCode) {
	case 27:
	case 99:
	case 102:
	case 105:
	case 106:
	case 112:
		if(klyi<0x100)
		{fullpgmenu();
		return;}
		break;

	case 87:
		chgfpgMode();
	case 90:
		if(PozMode>0x100){
			PozMode=0x2;
			SetPozMode();
			document.onmousemove=null;
			document.oncontextmenu=dsmall;
		}
		klyi=0x400;
		setTimeout(fullpgALL[fpgMode], 0);
	return;

	case 81:
		if(klyi>0x100&&curEro>0x7F){curEro-=0x80;}
		
		
		PozMode=0x0;
		SetPozMode00();
		
		
		
		if(iRDMarr!=0xF00000){iRDMarr=0;}
		tblarea.innerHTML=bz1+curEro+bz2;
		chaglims();
		menuFunction();
		//window.onmousewheel = document.onmousewheel =null;
		document.oncontextmenu=menuFunction;
		document.onmousemove=kuriakey;
		fullpgALL=fullpgALLcur;
		document.body.scrollLeft=0;
	return;


	case 98:
		document.body.scrollTop+=600;
		
		if(klyi>0x10&&klyi<0x100)
		{klyi=0x0;
		partpg();}
		
		
	return;

}


} else if(keyerocount>0)  {
switch (ekeyCode) {
	case 65:
		mouseRDM=false;
		keyerocount=1;
		hardlim=0;
		setTimeout(function() {document.execCommand('copy');recarea.focus();}, 500);
		
	return;
	case 80:
		PozMode++;
		SetPozMode();
	return;
	case 70:
		if(PozMode==1)
		{
			selefocus=searchxy(pozcur[0],pozcur[1]);
			return simpfocus();
		}
	break;
	case 87:
		chgfpgMode();
	case 90:
		
		if(PozMode!=0){PozMode=0x0; SetPozMode00();}
		document.onmousemove=null;
		document.oncontextmenu=dsmall;
		keyerocount=-100;
		document.body.background='';
		timgarea.src='';
		timgarea.style.maxHeight = '1%';
		
		klyi=0x400;
		setTimeout(fullpgALL[fpgMode], 0);
	return;
	
	case 81:
		if(iRDMarr!=0xF00000){iRDMarr=erocount+100;}
		fullpgALL=fullpgALLrdm;
		fullpgmenu();
		keyerocount=-100;
		pozcur[0]=window.outerWidth>>1;
		PozMode=0x101;
		SetPozMode();
		document.onmousemove=kuriakeysimp;
		document.oncontextmenu=fullpgmenu;
		//window.onmousewheel = document.onmousewheel =fastrscroll;
		
		document.body.background='';
		timgarea.src='';
		timgarea.style.maxHeight = '1%';

		var uv=tblarea.id.split('.');
		if(uv.length>1) { curEro=parseInt(uv[1],10);}
		else {curEro=0;}

		
	return;
	case 105:
	case 106:
		mouseRDM=false;
		menuFunction();
		document.body.scrollTop+=300;
	return;


	
	
	case 111:
		if(mouseRDM)
		{
			mouseRDM=false;
			break;
		}
		tblarea.innerHTML=bz1+curEro+bz2;
		mouseRDM=true;
		RDMcurEro();
		mydavRDM();
		document.body.scrollTop+=300;
	return;


	case 98:
		document.body.scrollTop+=300;
	break;
}

	if(canfire){canfire=false; symfireCon(500);}
} else {return;}

switch (ekeyCode) {
	
	case 109:
		curEro-=4;
		partpg();
	return;

	case 100:
		document.body.scrollLeft-=500;
		if(document.body.scrollLeft<600){document.body.background='';}
		document.body.scrollTop+=10;
	return;

	case 102:
		document.body.scrollLeft+=500;
		document.body.scrollTop+=10;
	return;

	case 75:
		setTimeout(entertxcmd, 100);
	return;
	

	case 37:
		document.body.scrollLeft-=100;
	return;

	case 39:
		document.body.scrollLeft+=100;
	return;

	case 27:
	case 101:
		if((PozMode&0xff)==1){PozCurKlicK();}
		return;

	case 107:
		partpg();
		document.body.scrollTop+=1000;
	return;

	case 104:
		document.body.scrollTop-=600;
	return;
}

}

function installclo(){


erocount=thumb.length>>3;

if(curEro>erocount){curEro=erocount-8;}

chaglims();



document.onkeydown=kycmd;

PozMode=0x0;
SetPozMode00();

document.oncontextmenu=dsmall;
keyerocount=-100;
timgarea.style.maxHeight = '1%';

klyi=0x400;
setTimeout(fullpgALL[fpgMode], 0);

}

function mkscript(sksrc)
{
	var sk = document.createElement('script');
	sk.src=sksrc;
	document.body.appendChild(sk);
	return sk;
}


function insp(apa)
{

var uv=location.href.split('#');
if(uv.length>1){
var uvv1=uv[1];
tblarea.id=uvv1;

uv=uvv1.split('.');
if(uv.length>1) {
curEro=parseInt(uv[1],10);
	if(uv[0]) {uvv1=uv[0];}
	else {
	mkscript('aadata.js').onload=installclo;
	return;
	}
}

if(apa){apa+='aadata.'+uvv1+'.js';}
else{apa='aadata.'+uvv1+'.js';}


} else{apa='aadata.js';}


mkscript(apa).onload=installclo;
}