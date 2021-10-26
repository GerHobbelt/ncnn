


var PozMode=0x2;
const pozimg='<img style="-webkit-filter: url(#cur)" src="poz.png" />';
const pozinput='<input type="number" value=0 onfocus=disabsk() onblur=fpt(this)>';
const pozinputN='<input type="number" value=50 onfocus=disabsk() onblur=repg(this)>';


var tblarea = document.getElementById('team');
var recarea = document.getElementById('urlrec');
var timgarea = document.getElementById('timg');
var pozcur=new Array(2);
var pozcurpic=timgarea.nextSibling;
var SVGf=new Array(3);
SVGf[2]=tblarea.previousSibling.firstChild;
SVGf[0]=SVGf[2].children[0];
SVGf[1]=SVGf[2].children[1];

var selefocus=null;

var erocount=0;
var keyerocount=0;
var hardlim=0;
var klyi=0x0;
var fpgMode=0;


var RDMarr=null;
var iRDMarr=0xF00000;

function byvid(vy)
{
	var msgl=thumb.length;
	for(var i=0;i<msgl;i++)
	{
		if(vids[i].includes(vy)){return i+'\nhttps://twitter.com/'+msgs[i];}
	}
}

function bythum(thuna)
{
	var msgl=thumb.length;
	
	for(var i=0;i<msgl;i++)
	{
		if(thumb[i].includes(thuna)){return vidstr(vids[i]);}
	}

}

function istatus()
{
	var msgl=msgs.length;
	var retstr="";
	for(var i=0;i<msgl;i++)
	{
		var st=msgs[i].replace('?','/').split('/');
		for(var j=0;j<st.length;j++)
		{
			if(st[j]=='status')
			{
				retstr+=st[j+1]+'\n';
				break;
			}
		}
	}
	return retstr;

}


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
{klyi=klyi&0xFF;
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

function xchg6(y,nx,lop)
{
	y*=6;
	nx*=6;

	for(var m=0;m<lop;m++)
	{
		
		var tmp=chika[y];
		chika[y]=chika[nx];
		chika[nx]=tmp;
		
		y++;
		nx++;
	}

}

function xchg6J(y,nx,lop)
{
	y*=6;
	nx*=6;

	for(var m=0;m<lop;m++)
	{
		
		var tmp=chikaJ[y];
		chikaJ[y]=chikaJ[nx];
		chikaJ[nx]=tmp;
		
		y++;
		nx++;
	}

}

function shuflocfi()
{
	
	var ibz=((chika.length/6)&(-1))-1;
	var ibzJ=((chikaJ.length/6)&(-1))-1;

	var ypit=1+((cimh.length/locfiL)>>0);

	for(var i=0;i<locfiL;i++)
	{
		
		var nx=(Math.random() *ibz)>>0;

		var exlop=6+(i&0x3f)*6;
		xchg6(ibz,nx,exlop);
		//xchg6J(ibzJ,nx%ibzJ,exlop);
		ibz--;
		ibzJ--;
		
		//if(ibz<=0){ibz=((chika.length/6)&(-2))-2;}

		
		nx=nx%locfiL;

		var tmp=locfi[i];
		locfi[i]=locfi[nx];
		locfi[nx]=tmp;

		var y=i;
		for(var vv=0;vv<ypit;vv++)
		{
			tmp=cimh[y];
			cimh[y]=cimh[nx];
			cimh[nx]=tmp;
			y+=locfiL;
			nx+=locfiL;
		}
		
	}
	

}

function testdov()
{
	var nk6=chika.length;
	var ymgstr='';
	for(var i=0;i<nk6;i+=6)
	{
		var dov=chika[i+1]%10;
		if(dov>0){
			var vfna=chika[i+5];
			var byord=dov-((i%11)%(dov+1));
			ymgstr+='<img class="ykix" src="'+chikagifpa[byord]+vfna+'.gif">'+vfna+'='+byord+'<br>';

		}
	}

	tblarea.innerHTML=ymgstr;
	shuflocfi();
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
var effectfilter=[];
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
SVGf[2].insertAdjacentHTML('beforeend',sst);
var fltr_cot=SVGf[2].children.length;
var fltr_names="";
for(var i=3;i<fltr_cot;i++)
{
	fltr_names+='\n>>'+SVGf[2].children[i].id;
}

recarea.value=fltr_names;
}

var gloefya=null;

function ParseSVG_name(dst,sst)
{
	var oldsst=sst;
	sst+='pg';
	if(!effectfilter.includes(oldsst))
	{
		
		var lvdb=localStorage.getItem(sst);
		if(lvdb)
		{
			effectfilter.push(oldsst);
			var ftr_elem=document.createElementNS(SVGheader, 'filter');
			
			ftr_elem.id=sst;
			ftr_elem.innerHTML=lvdb;
			SVGf[2].appendChild(ftr_elem);
			
			
		} else {return;}
	}

	var efya='url(#'+sst+')';
	if(lvdb){gloefya=efya;};
	dst.style.webkitFilter=efya;

}

function setNscroll(sst)
{

if(selefocus){
	selefocus.style.border = '10px dotted #fff';
	ParseSVG_name(selefocus,sst);
	
	selefocus.scrollIntoView();
	setTimeout(function(){selefocus.style.border=null;}, 5000);
} else{

var senta=document.getElementsByTagName("center");
var sefo=senta.length;
if(sefo)
{
	for(var i=0;i<sefo;i++)
	{
		var rect=senta[i].getBoundingClientRect();
		if(rect.y<0&&(rect.y+rect.height)>0)
		{
			sefo=senta[i];
			break;
		}

	}


} else {sefo=tblarea;}
ParseSVG_name(sefo,sst);


}

}

function pSVG()
{
	if(selefocus){tblarea.style.webkitFilter='';}
	
	var sst=recarea.value;
	if(sst.charCodeAt(1)==62){
			
			if(sst.charCodeAt(2)==64){
				ParseSVGblock(sst.split('@')[1]);
				
				return;
			}
			
			var syp=sst.indexOf('\n');
			if(syp==2){
				if(selefocus){selefocus.style.webkitFilter='';}
				else{tblarea.style.webkitFilter='';}
				gloefya=null;
			return;}
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
		var ktx=sst.substring(syp);
		localStorage.setItem(ftr_id, ktx);
		effectfilter.push(ftr_id);

		ftr_elem.innerHTML=ktx;
		SVGf[2].appendChild(ftr_elem);
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

const imhscale=' 0.7x';
const exxt=['.png','.b.png','.c.png','.d.png','.e.png',
	'.f.png','.g.png','.h.png','.i.png','.j.png','.jpg'];

function imhimgset(ymg)
{
	return ymg.srcset.split(' ')[0];
	
}

function imhlarge()
{
	var ymg=this.ctrlimg;
	var ostr=prompt('enlarge 1/x', '-2 z'+ymg.diz+' '+ymg.srcset);
	var yx =parseFloat(ostr);

	if(isNaN(yx)){
		imhnxt(ostr.substr(1),this);
		return;
	} else if(yx>10) {
		var imhbox=this.parentElement;
		var ymg=document.createElement('img');
		var y=(yx<<0);
		ymg.srcset=imhbox.bs+y+imhbox.ext+imhscale;
		ymg.diz=y+1;
		ymg.dizend=y+5;
		ymg.onerror=imherr;
		ymg.onclick=imhnxt;
		mkctrlarw(imhbox,ymg,y);
		return;
	}

	var doall=false;
	if(yx<0)
	{
		doall=true;
		yx=-yx;
	}
	var pixla=false;
	if(yx<1){pixla=true;}
	else{yx=1.0/yx;}

	if(doall)
	{
		var imhbox=this.parentElement;
		var imhboxl=imhbox.children.length;
		yx=' '+yx.toFixed(2)+'x';
		for(var k=0;k<imhboxl;k+=2)
		{
			var ymg=imhbox.children[k];
			var ymgsrc =imhimgset(ymg);
			

			ymg.srcset=ymgsrc+yx;
			
			
			if(pixla){ymg.style.imageRendering = 'pixelated';}
		}
	}
	else
	{
		
		var ymgsrc =imhimgset(ymg);
		ymg.srcset=ymgsrc+' '+yx.toFixed(2)+'x';
		
		if(pixla){ymg.style.imageRendering = 'pixelated';}
	}

	
}

function mkctrlarw(imhbox,ymg,y)
{
	var ctrlarw=document.createElement('b');
	ctrlarw.innerText=' < '+y+' + ';
	ctrlarw.ctrlimg=ymg;
	ctrlarw.onclick=imhlarge;
	ymg.ctrlarw=ctrlarw;
	imhbox.appendChild(ymg);
	imhbox.appendChild(ctrlarw);
}

function imhnxt(dst,ctrlarw)
{
	var ymg=this;
	if(ctrlarw){
		var y=dst;
		ymg=ctrlarw.ctrlimg;
		ymg.diz=Number(y)+1;
		
	}
	else {
		var y=ymg.diz;
		ymg.diz++;
		ctrlarw=this.ctrlarw;
	}

	

	var imhbox=ymg.parentElement;


	
	ctrlarw.innerText=' < '+y+' + ';
	
	var skl=' '+ymg.srcset.split(' ')[1];
	ymg.srcset=imhbox.bs+y+imhbox.ext+skl;
	
	

}

function imherr()
{

	var y=this.diz;
	if(y>=this.dizend){
		this.ctrlarw.remove();
		this.remove();
		return;}

	var imhbox=this.parentElement;
	var ymg=document.createElement('img');
	
	
	ymg.srcset=imhbox.bs+y+imhbox.ext+imhscale;
	ymg.diz=y+1;
	ymg.dizend=this.dizend;
	ymg.onerror=imherr;
	ymg.onclick=imhnxt;
	mkctrlarw(imhbox,ymg,y);
	
	this.srcset=imhbox.bs+(y-1)+'t.jpg';
	this.onclick=null;
	this.onerror=null;
	this.ctrlarw.remove();
	//this.remove();
	


}




function mkimh_fake(imhbox,sig)
{

	var sta=sig>>2;//(sig&0x0FF8)>>1;
	//var imhbox=document.createElement('b');
	var endo=sta+4;
	for(var i=sta;i<endo;i++)
	{
		var ymg=document.createElement('img');
		ymg.srcset='0bak/tu/ar/2/g/'+cimh[i]+imhscale;
		ymg.diz=i;
		mkctrlarw(imhbox,ymg,i);

	}
	//contan.appendChild(imhbox);
	
}



function mkimh_real(imhbox,sig)
{
	var sta=(sig>>4)%cimh.length;
	var imhsig=cimh[sta];//[(sig&0x1FF8)>>3];	//[(sig&0x1FF)|((sig&0xC00)>>1)];
	var humi=imhsig.split('@');
	
	var tmb=false;
	var ext=exxt[10];
	var endo=humi[1].charCodeAt(0);
	switch(endo)
	{
		case 103:
		ext='.gif';
		break;
		case 116:
		tmb=true;
		ext='t.jpg';
		break;
		case 112:
		ext=exxt[0];
		break;

	}
	endo=parseInt(humi[1].substr(1),10);
	var sta=1+(sta%endo);

	
	

	

	
	imhsig='https://m'+humi[0]+'.imhentai.xxx/'+humi[2]+'/'

	
	imhbox.innerText='*** '+humi[2]+' ***';
	imhbox.bs=imhsig;
	var dizadd=1;
	if(tmb)
	{
		imhbox.ext='.jpg';
		dizadd=0;
	}
	else {imhbox.ext=ext;}
	
	
	for(var i=0;i<5;i++)
	{
		var ymg=document.createElement('img');
		
		ymg.srcset=imhsig+sta+ext+imhscale;
		ymg.diz=sta+dizadd;
		ymg.dizend=sta+5;
		ymg.onerror=imherr;
		ymg.onclick=imhnxt;
		
		mkctrlarw(imhbox,ymg,sta);

		sta+=endo;
		
	}

	

	
	
}

function pChgChika()
{
	SVGf[cursvg].changechika((Math.random() *2048)&0x3ff);
}

function pChika()
{
	cursvg=1;
	recarea.value=recarea.value.substr(6);
	delete FuncList['chick'];
	changechikaChain();
}

var FuncList = {'chick0':pChgChika,'svg':pSVG,'p':pPick,'chick':pChika};
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
		klyi=0x400|(klyi&0xFF);
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


const kx1='<a href="https://twitter.com/';
const kx1t='<br><a href="https://twitter.com/';
const kx2b1='" >==========<br>==>';
const kx2b2='<==</a><br><a href="';
const kx2a='" >====</a><a onmouseover=xt(this)>i';
const kx3='</a><br><a href="';
const kx3t='<a href="';
const kx4='" ><img src="';
const kx5a=':thumb" width=211 /></a>.<br>';
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

	var kole7='=<br>';
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



var klyi2=0;

function skrback()
{
pvklyi=-1;
document.body.background='';
document.body.scrollTop+=20;
document.body.scrollLeft=0;
}

var pvklyi=-10;


function xtp()
{

	if(klyi2==0)
	{
		//tblarea.appendChild(timgarea);
		var tsstr=thumbstr(thumb[pvklyi]);
		document.body.background = tsstr;
		timgarea.src=tsstr;
		timgarea.style.maxHeight = '800%';
	}

	recarea.value+='\n:'+pvklyi;
	document.body.scrollLeft+=2000;
	setTimeout(skrback, 1000);
	//recarea.value+='\nhttps://twitter.com/'+msgs[pvklyi];
}



function xt(ele)
{


var c0=ele.innerText.charCodeAt(0);


var iszrda = parseInt(ele.innerText.substring(1),10);


	if(pvklyi!=iszrda)
	{
		pvklyi=iszrda;
		klyi2=0;
		
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
				pvklyi+=0x10;
			}
			return;
			case 114:	//r
			{
				var ymgg=ele.nextSibling.nextSibling.firstChild;
				if(ymgg.naturalWidth==0)
				{
					var niki=msgs[iszrda].split('/')[0];
					
					

					if(!ymgg.noap){
					var containfl=ymgg.parentElement.parentElement;
					if(!containfl.hazbp)
					{
						containfl.hazbp=true;
						var al=document.createElement('a');
						var bp=document.createElement('img');
						//bp.style.display='block';
						al.appendChild(bp);
						containfl.appendChild(al);
						bigparit(bp,2);
					}
					}

					ymgg.noap=true;
			//if(iszrda&1){}
			//else{ymgg.parentElement.nextElementSibling.outerHTML=lynklocfi_small(iszrda);}
					
					if(niki=='i'){niki=' ';}
					else{ymgg.alt='\n:'+niki+'\n';
					niki=' href="https://nitter.kavin.rocks/'+niki+'/media" ';
					}
					ele.onmouseover=null;
					ele.onclick=null;
					ele.outerHTML='<a'+niki+'>...X'+iszrda+aTAGend;
					pvklyi+=0x10;
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
	
		if(klyi2==0)
		{
			//tblarea.appendChild(timgarea);
			
			document.body.background =timgarea.src;
			//timgarea.style.maxHeight = '800%';
			
		}
		else if(klyi2==1){
			document.body.scrollLeft+=2000;
			setTimeout(skrback, 1000);
			

		}
		klyi2++;
	}
}

function rmvimg()
{
if(pvklyi>=0){
	//timgarea.src='';
	//timgarea.style.maxHeight = '1%';
	pvklyi=-1;
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
	//document.title=curEro + "0 to "+ keyerocount+'0 key';
	}
	
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
		kole7+=kx1t+msgs[iszrda]+
		kx2b1+iszrda+kx2b2+vidstr(vids[iszrda])+
		kx4+thumbstr(thumb[iszrda])+
		kx5b;
	}
	yina.innerHTML = kole7;
	tblarea.appendChild(yina);

	//mkimh(ele,(Math.random()*0x4000)<<0);

	curEro+=2;
}

function mydavRDM()
{
	var yina = document.createElement('FL');

	
	var kole7=iRDMarr.toString(16);
	var xma=(iRDMarr&7);
	var endo=iRDMarr+8;
	for(var j=iRDMarr;j<endo;j++){
var iszrda=(RDMarr[j]<<3)+xma;
kole7+=kx1t+msgs[iszrda]+
kx2a+iszrda+
kx3+vidstr(vids[iszrda])+
kx4+thumbstr(thumb[iszrda])+
kx5a;

	}
	yina.innerHTML = kole7;
	tblarea.appendChild(yina);

	canfire=true;
}

function lynklocfi(sig)
{



	

	var n6=sig&0x3FF;
	var locfisyg=locfi[n6>>1];
	if(!locfisyg){locfisyg=locfi[n6>>2];}


	n6*=6;
	
	
	
	var dov=chika[61+n6]%10;
	if(dov>0){
		var zipi='<img class="ykixm5" srcset="'+chikagifpa[dov-((n6%11)%(dov+1))]+chika[65+n6]+'.gif '+chika[64+n6].toFixed(1)+'x" />';
	}
	else
	{
		var zipi='<img class="yki'+SVGf_cursvg_id+'" src="poz.png" />';
	}
	
	var skei='==<img srcset="0bak/tu/xx/_';
	var skeix=chika[4+n6];
	if(skeix>1.0){
		skei='==<img class="ykix" srcset="0bak/tu/xx/_';
		skeix-=1.0;
	}

	return skei+chika[5+n6]+'.gif '+skeix.toFixed(1)+'x" /></a>==<a class="yka" href="0bak/tu/lu/'+locfisyg+'.mp4">'+zipi+locfisyg;
	
	
	

}

function lynklocfi_small(sig)
{
	
	var n6=((sig>>1)%0x3FF);
	var locfisyg=locfi[n6>>1];
	if(!locfisyg){locfisyg=locfi[n6>>2];}


	locfisyg='<a href="0bak/tu/lu/'+locfisyg+'.mp4"><img class="ykis';

	if(n6&2)
	{
		n6*=6;
		var chkkpai=chikagifpa1;
		var dov=chika[1+n6]%10;
		if(dov>0){
		
			chkpai=chikagifpa[dov-((n6%11)%(dov+1))];
		}
		
		return locfisyg+'2" src="'+chkkpai+chika[5+n6]+'.gif"></a>.<br>';
	}
	else
	{
		
		return locfisyg+'1'+SVGf_cursvg_id+'" src="poz.png"></a>.<br>';

	}
	
					
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
	else if(ele.diz)
	{
		setTimeout(function(){ele.src='m.gif';}, 3000);
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


if(ele.src.endsWith(exxt[10]))
{
	var naa=ele.alt;
	if(naa)
	{
	ele.onmouseover=null;
	ele.onclick=null;
	var sig=parseInt(naa,10);
	var vidurl=vidstr(vids[sig]);
	var thumburl=ele.src;

	
	

	ele.outerHTML=kx3t+vidurl+kx4+thumburl+kx5t+msgs[sig]+'" >@'+sig+lynklocfi(sig)+'</a>';
	
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
if(ele.naturalWidth){ele.outerHTML=kx1t+msgs[sig]+kx2b1+sig+kx2b2+vidurl+kx4+thumburl+kx5b;}
else{




ele.outerHTML=kx3t+vidurl+kx5t2+msgs[sig]+'" >'+sig+lynklocfi(sig)+'</a>';

}




}

const llgif='0bak/longbar.png';

function clearlbr(ele)
{
try {
delete ele.lbr;
delete ele.repl;
delete ele.nrepl;
delete ele.crepl;
ele.removeAttribute('srcset');
}catch (e){}
ele.style.cssText = null;
ele.onmousemove=null;
ele.onmouseover=null;

}

function hv2(ele)
{

clearlbr(ele);


var vnba=parseInt(ele.alt,10);

ele.src=thumbstr(thumb[vnba]);

ele.onclick=kv;

}



function rstomovr_3(ele,tresto)
{
	if(ele.nrepl){ele.onmousemove=tresto;};
}

function rstomovr(ele,tresto)
{
klyi&=0xffffff0f;
ele.style.pointerEvents='auto';
if(ele.nrepl){
	setTimeout(function(){rstomovr_3(ele,tresto);}, 1500);
}
}

function tranzsw()
{

	klyi++;
	if(klyi&0xC0)
	{
	
		

	var ele=this;
	ele.onmousemove=null;
	if(!ele.nrepl){return;}

	ele.style.pointerEvents='none';
	
	
	var ycrepl=ele.crepl+1;
	if(ycrepl>=ele.nrepl){ycrepl=0};

	var bsfna=ele.repl[0];
	ele.src=bsfna+exxt[ycrepl];
	ele.crepl=ycrepl;
	
	var bgdff=ele.repl[1];
	if(bgdff)
	{
		var gx=bgdff[ycrepl];
		if(gx<10){ele.style.background='url('+bsfna+exxt[gx]+') 0% 0% / 100%';}
		else{ele.style.background='';}
	}

	
	setTimeout(function(){rstomovr(ele,tranzsw);}, 1500);
	}

}

function tranzsw2()
{

	klyi++;
if(klyi&0xC0){
	
		

	var ele=this;
	ele.onmousemove=null;
	if(!ele.nrepl){return;}

	ele.style.pointerEvents='none';
	
	
	var ycrepl=ele.crepl+1;
	

	var bsfna=ele.repl[0];

	

	

	
	
	ele.style.background='url('+bsfna+exxt[ycrepl%(ele.nrepl)]+') 0% 0% / 100%';
	if((ycrepl&3)==3)
	{
		
		var dvv2=(ycrepl>>2)%(ele.repl[1]);
		ele.srcset=bsfna+'f'+exxt[dvv2]+ele.repl[2];
	}
		
	
	
	

	
	
	ele.crepl=ycrepl;

	setTimeout(function(){rstomovr(ele,tranzsw2);}, 1500);
}

}


function through15s3(ele)
{
	if(ele.nrepl){ele.onmouseover=through15s;}
}

function through15s2(ele)
{
	ele.style.pointerEvents='auto';
	if(ele.nrepl){
	setTimeout(function(){through15s3(ele);}, 1500);
	}
}

function through15s()
{
	var ele=this;
	ele.onmouseover=null;
	ele.style.pointerEvents='none';
	setTimeout(function(){through15s2(ele);}, 1500);

}


function traspscale()
{
	this.onload=null;
	var skl=(this.wu/this.naturalWidth).toFixed(3);
	this.style.webkitTransform='matrix('+skl+',0,0,'+skl+',0,-'+this.hu+')';

}

function bigparit(ele,ack)
{
			ele.onmouseover=through15s;
			ele.nrepl=99;
			ele.onclick=hv0fast[ack];


			var n6=Math.random() *chikaJ.length;//144;//
			n6-=(n6%6);
			var fnaya=chikaJ[n6+5];
			ele.style.cssText = null;
		
			var skl=chikaJ[n6+4];
		
		if(skl<1.0){
			ele.src=xjpa[0]+fnaya+'.jpg';
			ele.style.webkitMask='url('+xjpa[1]+fnaya+'_j.png) 0% 0% / 100%';
		} else{
			skl-=1.0;
			
			var dov=chikaJ[n6+1]%10;

			var kees=0;
			var fumpa='';
			
			if(dov>0){
				ele.crepl=0;
				var repl=null;
				dov+=1;
				switch(fnaya.charCodeAt(2))
				{
					case 42:	//*
						kees=2;
						var soya=fnaya.split('*');
						
						fnaya=soya[1];
						fumpa=xjpa[0]+fnaya;
						repl=[fumpa,parseInt(soya[0],10),' '+soya[2]+'x'];
						ele.removeAttribute('src');
						ele.srcset=xjpa[0]+fnaya+'f.png'+repl[2];
						ele.style.background='url('+fumpa+'.png) 0% 0% / 100%';

						for(var juju=1;juju<dov;juju++)
						{
							let siok=fumpa+exxt[juju];
							
							setTimeout(function(){timgarea.src=siok;}, 100+juju*200);
							
						}
					break;
					case 94:	//^
						kees=1;
						var sygi=bgdiff[fnaya.substr(0,2)];
						fnaya=fnaya.substr(3);
						fumpa=xjpa[0]+fnaya;
						repl=[fumpa,sygi];
						var gx=sygi[0];
						if(gx<10){ele.style.background='url('+fumpa+exxt[gx]+') 0% 0% / 100%';}
						for(var juju=1;juju<dov;juju++)
						{
							let siok=sygi[juju];
							if(siok<10)
							{
							setTimeout(function(){timgarea.src=fumpa+exxt[siok]}, 100+juju*200);
							}
						}
						
					break;
					default:
						repl=[xjpa[0]+fnaya];
					break;

					
				}
				
				
				

				ele.repl=repl;
				
				ele.nrepl=dov;
				ele.onmouseover=null;
				if(kees==2){
					ele.onmousemove=tranzsw2;
					
				}
				else {ele.onmousemove=tranzsw;
					ele.src=xjpa[0]+fnaya+'.png';}
				
				
			} else {ele.src=xjpa[0]+fnaya+'.png';}
			

		}
		var mvtt=chikaJ[n6+3];
		
		if(ack==2)
		{
			skl=1.3;
			//mvtt=(mvtt*6)>>3;
			mvtt=',70,-'+mvtt;
			ele.lf=n6%locfiL;
		} else {
			mvtt=',0,-'+(mvtt>>2);
			skl=(1.0/skl).toFixed(3);}
			
		
		ele.style.webkitTransform='matrix('+skl+',0,0,'+skl+mvtt+')';


}


function llimgThis(ele,nx,depth)
{
	if(ele)
	{
		var tgg=ele.tagName;
		var isIMG=(tgg=='IMG');
		if(isIMG&&ele.src.endsWith('/poz.png')) {
		
		ele.lbr=true;
		
		if(cursvg==1)
		{
			cursvg=0;
			bigparit(ele,1);

			
		}
		else
		{
			cursvg=1;
			ele.onmouseover=hv0fast[0];
			ele.src=llgif;		
			ele.style.webkitFilter='url(#longbar)';
		}

		
		}
		else if(depth<5)
		{
			
			
			depth++;
			if(isIMG)
			{
				if(ele.lbr)
				{
				
				hv2(ele);
				klyi-=0x100;
				return;
				}
			}
			else
			{
				depth--;
				if(tgg=='B')
				{
					ele.innerText='';
					mkimh(ele,(Math.random()*0x4000)<<0);
				}
				
			}

			
			if(nx){llimgThis(ele.nextSibling,true,depth);}
			else{llimgThis(ele.previousSibling,false,depth);}
			return;

		}
	}
	
	klyi-=0x100;
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
klyi++;
if(klyi&1){return;}



var ele=e.currentTarget;
var sig=ele.alt;
var c0=sig.charCodeAt(0);
if(c0==120)	//==x
{
	sig=sig.substring(1);
	ele.alt=sig;
	ele.src=thumbstr(thumb[parseInt(sig,10)])+':thumb';
	ele.onclick=kv_s;
	return;
}

ele.onmouseover=null;
var sigN=parseInt(sig,10);
if(ele.naturalWidth==0)
{
ele.onclick=null;
var thumburl=ele.src.slice(0,-6);



ele.outerHTML=kx3t+vidstr(vids[sigN])+kx4+thumburl+'" width=200 />'+lynklocfi(sigN)+'</a>';

}
else if((sigN%17)==0)
{
	var imhbox=document.createElement('b');
	mkimh(imhbox,sigN);
	ele.parentElement.appendChild(imhbox);
}


return;




}

function hv0func(ele,suub)
{
	//if(klyi<0x200){klyi=0x400+(klyi&0xFF);}

	klyi+=suub;
	setTimeout(function(){llimgThis(ele.nextSibling,true,0);}, klyi);
	klyi+=(suub+1);
	setTimeout(function(){llimgThis(ele.previousSibling,false,0);}, klyi);

	clearlbr(ele);
	ele.src=thumbstr(thumb[parseInt(ele.alt,10)]);
	ele.onclick=kv;
	document.body.scrollTop+=100;
}

var hv =function(e) {

klyi++;
if(klyi&7){return;}

hv0func(e.currentTarget,0xFC);

}


var hv0fast=new Array(3);
hv0fast[0]=function(e) {hv0func(e.currentTarget,0x100);}
hv0fast[1]=function(e) {


var tg=e.currentTarget;
tg.onmousemove=null;
tg.nrepl=0;
/*
var h8p=tg.height>>3;
var w8p=tg.width>>3;

var h8=e.offsetY-(h8p<<2);
var w8=e.offsetX-(w8p<<2);

if(h8>0 && w8>0 && h8<h8p && w8<w8p)
*/
{hv0func(tg,0x100);}
}

hv0fast[2]=function(e) {
	var ele=e.currentTarget;
	var vlf=(ele.lf&0x1f)<<5;
	var lf='0bak/tu/lu/'+locfi[ele.lf]+'.mp4';
	clearlbr(ele);
	//ele.outerHTML='<a href='+lf+'>@</a>';	//@@@@LOKAL@@@@@
	ele.src='poz.png';
	ele.style.webkitFilter='url(#longbar)';

	
	ele.style.marginLeft=0x100;
	ele.style.marginTop=-vlf;
	//ele.style.marginBottom=vlf;
	
	ele=ele.parentElement;
	ele.href=lf;
	//ele.lf=lf;
	
	//ele.onclick=showlf;
	//window.open(lf, null, null);
	
}

function showlf()
{
	window.open(this.lf, null, null);

}

const mgx1sma='</a><img class=tFlo alt=x';
const mgx1='<img style="-webkit-filter: url(#cur)" src="poz.png" alt=';
const mgx2=' title=';
const mgx3=' />';
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
	klyi=0x400|(klyi&0xFF);
	var ymgs=document.getElementsByTagName('img');
	var ymgsl=ymgs.length;
	for(var i=0;i<1024;i++){ymgs[i].onmouseover=hv;}
	
}

const c1bkg= '<br><img ';

function asgn_s(ym)
{
	document.body.scrollLeft=0;
	tblarea.innerHTML=ym.join(' /></b><b> =<a href="https://twitter.com/');
	var ymgs=document.getElementsByTagName('img');
	var ymgsl=ymgs.length;
	for(var i=1;i<1025;i++){ymgs[i].onmouseover=hv_s;}
}

function fullpgALLcur0()
{


var dvvsta=(curEro>>3);
var dvvendo=dvvsta+8;
var kole7='<center><h1>Serial<br>*<br>*<br>*<br>*<br>512+64*'+dvvsta+sixbkmark(dvvsta);

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
	
	var dvvsta=curEro;
	var dvvendo=dvvsta+512;
	var pidx=1;
	var ym=new Array(1026);

	ym[0]='<b>Serial '+dvvsta+c1bkg;
	ym[1025]='"></a></b>';

for(var jj=dvvsta;jj<dvvendo;jj++)
{
	
	var iszrda=jj;
	ym[pidx]=msgs[iszrda]+'" >'+iszrda+mgx1sma+iszrda;
	pidx++;
	
	iszrda=jj+512;
	ym[pidx]=msgs[iszrda]+'" >'+iszrda+mgx1sma+iszrda;
	pidx++;
		
}
	
	asgn_s(ym);
	curEro+=1024;
	
}

function fullpgALLrdm0()
{


var dvvsta=(iRDMarr>>3);
var dvvendo=dvvsta+8;
var kole7='<center><h1>Random<br>*<br>*<br>*<br>*<br>128*'+dvvsta+sixbkmark(dvvsta);//'<br>*<br>*<br>*<br>*<br>*</h1>';

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
	
	var dvvsta=iRDMarr;
	var dvvendo=dvvsta+128;
	var pidx=1;
	var ym=new Array(1026);

	ym[0]='<b>Random '+dvvsta+c1bkg;
	ym[1025]='"></a></b>';

for(var jj=dvvsta;jj<dvvendo;jj++)
{

	var ey=(RDMarr[jj]<<3);
	var eybk=(RDMarr[64+jj]<<3);
	for(var jjx=0;jjx<8;jjx++)
	{
			var iszrda=ey+jjx;
			ym[pidx]=msgs[iszrda]+'" >'+iszrda+mgx1sma+iszrda;
			pidx++;
			jjx++;
			iszrda=eybk+jjx;
			ym[pidx]=msgs[iszrda]+'" >'+iszrda+mgx1sma+iszrda;
			pidx++;
	}
}

	iRDMarr+=128;
	asgn_s(ym);
	
}

var fullpgALLcur=[fullpgALLcur0,fullpgALLcur1];
var fullpgALLrdm=[fullpgALLrdm0,fullpgALLrdm1];

var fullpgALL=fullpgALLcur;

function fullpg()
{
	tblarea.innerHTML='';
	document.body.scrollTop=0;
	var senta=document.createElement('center');

	


	var kole7='<h1>**'+iRDMarr+'**</h1>';


	var xma=(iRDMarr&7);
	var zko=iRDMarr-xma;
	var endo=zko+8;
	for(var j=zko;j<endo;){
		var iszrda=(RDMarr[j]<<3)+xma;
		kole7+=kx1t+msgs[iszrda]+
		kx2b1+iszrda+kx2b2+vidstr(vids[iszrda])+
		kx4+thumbstr(thumb[iszrda])+
		kx5b;
		j++;
		iszrda=(RDMarr[erocount-j]<<3)+xma;
		kole7+=kx1t+msgs[iszrda]+
		kx2b1+iszrda+kx2b2+vidstr(vids[iszrda])+
		kx4+thumbstr(thumb[iszrda])+
		kx5b;
	}
	
	
	senta.innerHTML =kole7;
	if(gloefya){senta.style.webkitFilter=gloefya;}
	tblarea.appendChild(senta);
	
	document.body.scrollLeft=100;
}

function fastrscroll(ev) {return false;}

function fullpgmenu()
{
RDMcurEro();
fullpg();
return false;
}



var cursvg=0;

SVGf[0].changechika=function(n6)
{
	
	
	if(this.fna===n6){return;}
	this.fna=n6;
	
	n6*=6;

	var chkpai=chikagifpa[0];
	var yv=chika[n6+1];
	var dov=yv%10;

	if(dov>0){
		yv-=dov;
		dov++;
		chkpai=chikagifpa[(n6%11)%dov];
	}
	
	
	
	
	this.setAttribute('x',chika[n6]);
	this.setAttribute('y',-yv);
	this.setAttribute('width',chika[n6+2]);
	this.setAttribute('height',chika[n6+3]);
	this.firstChild.setAttribute('href',chkpai+chika[n6+5]+'.gif');
	
}


var SVGf_cursvg_id='longbar';

SVGf[0].changechikaPNG=function(x,y,wid,hgt,pic)
{
	this.setAttribute('x',x);
	this.setAttribute('y',-y);
	this.setAttribute('width',wid);
	this.setAttribute('height',hgt);
	this.firstChild.setAttribute('href',pic);

}



const xjpa=['0bak/tu/xj/','0bak/tu/xj/fyn/gb/'];
SVGf[1].changechika=function(n6)
{
	
	
	if(this.fna===n6){return;}
	this.fna=n6;
	
	

	
	if(chikaJ[n6*6+4]>0){
		SVGf[0].changechika(n6);
		/*
		var dov=yv%10;
		if(dov>0){
			yv-=dov;
			dov++;
				
		}
		
		SVGf[0].changechikaPNG(chikaJ[n6],yv,chikaJ[n6+2],chikaJ[n6+3],xjpa[1]+chikaJ[n6+5]+exxt[(n6%11)%dov]);
		*/
		SVGf_cursvg_id='longbar';
		return;
	}
	n6*=6;
	var yv=chikaJ[n6+1];
	SVGf_cursvg_id='longbarJ';
	
	
	

	this.setAttribute('x',chikaJ[n6]);
	this.setAttribute('y',-yv);
	this.setAttribute('width',chikaJ[n6+2]);
	this.setAttribute('height',chikaJ[n6+3]);

	var img1=this.firstChild;
	var fnaya=chikaJ[n6+5];
	img1.setAttribute('href',xjpa[0]+fnaya+exxt[10]);
	img1=img1.nextSibling;
	img1.setAttribute('href',xjpa[1]+fnaya+'_j.png');
	
}

function changechikaChain()
{

	if(cursvg){cursvg=0;}
	else{cursvg=1;}
	
	//if(pvklyi&0xffffff00){setTimeout(changechikaChain, 0x20000); return;}
	//else{}
	
	setTimeout(changechikaChain, 0x4000+((pvklyi&0x1F)<<10));

	pvklyi+=klyi;
	pvklyi=pvklyi^(pvklyi<<3)
	pvklyi=pvklyi^(pvklyi>>1)
	pvklyi=pvklyi^(pvklyi<<5)
	

	SVGf[cursvg].changechika(pvklyi&0x3ff);

	
	

}

function roundng(rnum,mulpa) {
	v=chika[rnum];
	chika[rnum]= (v >= 0 || -1) * Math.round(Math.abs(v)*mulpa);
}

function chikasz()
{
	for(var i=0;i<32;i++)
	{
		var rnum=i*6;
		var mulpa=chika[rnum+4];
		
		//roundng(rnum,mulpa);
		roundng(rnum+1,mulpa);
		roundng(rnum+2,mulpa);
		roundng(rnum+3,mulpa);
		

		chika[rnum+4]=1.0/mulpa;
	}

}
function setklyi2()
{
	klyi2=locfiL;
	shuflocfi();
}

function chgfpgMode()
{
	if(fullpgALL===fullpgALLcur&&curEro>1021){curEro-=1022;}
	
	fpgMode++;
	pvklyi=Date.now()&(-1);
	
	switch(fpgMode)
	{
		case 1:
			setklyi2();
			//if(chika[4]<1.0){chikasz();}
		break;
		default:
			
			fpgMode=0;
			SVGf[cursvg].changechika(1);
		break;
		
	}
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
	else if(!sst[1]){kall(sst[0],null);}
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
		klyi=0x400|(klyi&0xFF);
		if(PozMode>0x100){
			setklyi2();
			PozMode=0x2;
			SetPozMode();
			document.onmousemove=null;
			document.oncontextmenu=dsmall;
			pvklyi=Date.now()&(-1);
		}
		
		
		setTimeout(fullpgALL[fpgMode], 0);
	return;

	case 81:
		if(klyi>0x100&&curEro>0x7F){curEro-=0x80;}
		
		
		PozMode=0x0;
		SetPozMode00();
		
		pvklyi=(-10)|(klyi&0xFF);
		
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
		setklyi2();
		klyi=0x400|(klyi&0xFF);
		pvklyi=Date.now()&(-1);
		if(PozMode!=0){PozMode=0x0; SetPozMode00();}
		document.onmousemove=null;
		document.oncontextmenu=dsmall;
		keyerocount=-100;
		document.body.background='';
		timgarea.src='';
		timgarea.style.maxHeight = '1%';
		
		
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
timgarea.style.maxHeight = '1%';


document.onkeydown=kycmd;

PozMode=0x0;
SetPozMode00();

document.oncontextmenu=dsmall;
keyerocount=-100;
timgarea.style.maxHeight = '1%';



var srdm=(1024*Math.random())>>2;
locfi[0]=locfi[srdm];

klyi=0x400|(srdm>>3);

chgfpgMode();

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