// ==UserScript==
// @name VidTool
// @match https://video.twimg.com/*
// @match file://*.mp4
// ==/UserScript==



var vio=document.getElementsByTagName("video")[0];
if(vio=== undefined){setTimeout(close, 500);}

//var vio=document.getElementById('media');

var kyfunc = new Array(4);

var vio90h=0;
var plbrate=1.0;
var isrot=false;

vio.loop=true;


vio.style.maxHeight='800%';
vio.style.maxWidth='800%';

var dfheight=window.innerHeight-4;
vio.height=dfheight;
var intervalHandle = null;


var shrinkbefore=false;




function toclpb()
{
	yput.select();
	document.execCommand('copy');
}

var SVGf=null;
const SVGheader='http://www.w3.org/2000/svg';

function ParseSVGf(sst)
{
	
	var syp= sst.indexOf('\n');
	var ftr_id=sst.substring(1,syp);

	var prev=document.getElementById(ftr_id);
	if(prev){prev.innerHTML=sst.substring(syp);}
	else{
		var ftr_elem=document.createElementNS(SVGheader, 'filter');
		ftr_elem.id=ftr_id;
		ftr_elem.innerHTML=sst.substring(syp);
		SVGf.appendChild(ftr_elem);
		yput.value='>>'+ftr_id;
	}
	
	vio.style.webkitFilter='url(#'+ftr_id+')';

}

function ParseSVGblock(sst)
{
SVGf.innerHTML=sst;
var fltr_cot=SVGf.children.length;
var fltr_names="";
for(var i=0;i<fltr_cot;i++)
{
	fltr_names+='\n>>'+SVGf.children[i].id;
}

yput.value=fltr_names;
}


function effeci()
{
document.onkeyup=kyfunc[1];
var sst=yput.value;
yput.rows=1;
if(sst) {
var c0=sst.charCodeAt(0);
switch(c0) {
	case 47:	//==/
	
		if(intervalHandle){clearInterval(intervalHandle);intervalHandle=null;}
		sst=sst.replace('2.0123/\n/','-');
		var sn=sst.split('/');
		var vsta=parseFloat(sn[1]);
		var fvend=parseFloat(sn[2]);
		if(fvend<0){
			fvend=-(vsta+fvend);
			yput.value='/'+vsta.toFixed(2)+'/'+fvend.toFixed(2)+'/\n';
		}
		
		vio.currentTime=vsta;
		intervalHandle=setInterval(function(){vio.currentTime=vsta;}, Math.ceil(fvend*(1000.5/plbrate)));
	break;
	
	case 114:	//==r
		if(sst.length<3)
		{
			plbrate=1.0;
			
		}
		else
		{
			plbrate=parseFloat(sst.substring(1));
			
		}
		vio.playbackRate=plbrate;
	break;
	case 62:	//==<
		if(sst.charCodeAt(1)==62){

			if(sst.charCodeAt(2)==64){
				ParseSVGblock(sst.split('@')[1]);
				vio.play();
				return;
			}
			else {
			var syp=sst.indexOf('\n');
			if(syp==2){vio.style.webkitFilter=null;break;}
			else if(syp>2){sst=sst.substring(2,syp);}
			else{sst=sst.substring(2);}
			vio.style.webkitFilter='url(#'+sst+')';
			}
		}
		else{ParseSVGf(sst);}
		
	break;
	case 111:	//==o
		if(isrot){rotvi(null);}

		sst=sst.split('\n')[0];	//==m
		if(sst.charCodeAt(1)==109){rotvi([0x2,sst.substring(2)]);}
		else {rotvi([0x1,sst.substring(1)]);}
		
		vio.play();
	return;
	case 108:	//==l
		vio.height=parseInt(sst.substring(1),10);
		if(calcscall()){installpan();}
		vio.play();
	return;
	default:
		vio.style.webkitFilter = sst.replace('\n',' ');
		//toclpb();
	break;
}
	
	
	

	
}
else {vio.style.webkitFilter = null;}

if(shrinkbefore) {
vio.height=dfheight;
shrinkbefore=false;}

vio.play();
}


function rdce()
{
	vio.height-=100;

	if(isrot){rotvi_margin();}
	else{vio.style.margin= 0;}
	shrinkbefore=true;
}

function zmin()
{
	shrinkbefore=false;
	vuvu=vio.height;
	vuvu+=100;
	yput.value='l'+vuvu;
	vio.height=vuvu;

	if(isrot){rotvi_margin();}
	else{vio.style.margin= 0;}
	
	if(calcscall()){if(isnotpan){installpan();}}
}

function zmout()
{
	rdce();
	if(!calcscall()){if(!isnotpan){rmvpan();}}
}

function zm600()
{
	shrinkbefore=false;
	
	if(!isrot){vio.style.margin= 0;
	if(vio.height<(dfheight+50)){vio.height+=600;}
	}



	if(calcscall()){installpan();}
}




function paosa(){
	document.onwheel=null;
	if(vio.scrollWidth>1700){rdce();}

	document.onkeyup=kyfunc[0];
	yput.rows=8;
	if(intervalHandle){return;}
	else{vio.pause();}
}


function createeffctctrl()
{
var oyput = document.createElement('div');
oyput.style='position:fixed;right:0px;top:0px;color:white;';
oyput.innerHTML='<textarea rows=1 ></textarea><svg width=0 height=0 ><defs></defs></svg>';
document.body.insertBefore(oyput,vio);
SVGf=oyput.children[1].firstChild;
oyput=oyput.firstChild;
oyput.onfocus=paosa;
oyput.onblur=effeci;
oyput.oncontextmenu=function(){close();return false;};


return oyput;
}

var yput=createeffctctrl();


function rotvi_margin()
{
	var cur_h=vio.height;
	if(vio90h>0)
	{
		var sya=(cur_h-vio.scrollWidth)>>1;
		if(sya>0)
		{
		vio.style.marginTop='-'+sya+'px';

		var syb=window.innerWidth-cur_h;
		if(syb>0){vio.style.marginLeft=(sya+(syb>>1))+'px';}
		else {vio.style.marginLeft=sya+'px';}
		}
	}
	else
	{
		var sya=(vio.scrollWidth-cur_h)>>1;
		vio.style.marginLeft='-'+sya+'px';
		vio.style.marginTop=sya+'px';
	}

}

function rotvi(krot)
{


if(isrot) {
	vio.style.margin='auto';
	vio.style.webkitTransform=null;
	vio.height=dfheight;

	isrot=false;
} else if(krot) {
	
	var typ=krot[0];
if(typ)
{
	vio.style.margin='auto';
	switch(typ)
	{
		case 0x1:
			vio.style.webkitTransform = 'rotate('+krot[1]+'deg)'; 
		break;
		case 0x2:
			vio.style.webkitTransform = 'matrix('+krot[1]+')';
		break;
	}
}
else
{
	vio.style.margin='0px';


	if(!vio90h){
		document.onwheel=null;

		var lvioasp=(vio.videoHeight/vio.videoWidth);

		if(lvioasp<1.0){vio90h=4-window.innerWidth;}
		else {vio90h=(dfheight*lvioasp)<<0;}

	}

	var t1=krot[1]?-1.0:1.0;

	vio.style.webkitTransform = 'matrix(0,'+t1+','+(-t1)+',0,0,0)';

	if(vio90h<0){var lvio90h=-vio90h;}
	else {var lvio90h=vio90h;}

	vio.height=lvio90h;
	rotvi_margin();
}


isrot=true;
}


}



var delayii=0x0;
var mxw=0;
var mxh=0;

const sl1=1.1;
const sl2=0.9;

function calcscall()
{

if(isrot)
{
var tmxw=vio.scrollHeight/window.outerWidth;
var tmxh=vio.scrollWidth/window.outerHeight;
}
else
{
var tmxw=vio.scrollWidth/window.outerWidth;
var tmxh=vio.scrollHeight/window.outerHeight;
}

if(tmxw>sl1&&tmxh>sl1)
{
mxw=tmxw-sl2;
mxh=tmxh-sl2;
return true;
} 
return false;
	
}

var isnotpan=true;
function installpan()
{
	isnotpan=false;
	vio.controls=false;
	document.onmousemove=kyfunc[2];
	document.onwheel=kyfunc[3];
	document.body.style.overflow = "hidden";
}

function rmvpan()
{
	isnotpan=true;
	vio.controls=true;
	document.onmousemove=null;
	document.onwheel=null;
	document.body.style.overflow = "auto";
}



function ruu(x,y)
{
document.scrollingElement.scrollLeft=x*mxw;
document.scrollingElement.scrollTop=y*mxh;
}






kyfunc[2]=function(ev) {

delayii++;

if(delayii > 0x14) {
	delayii=0x0;
	ruu(ev.clientX,ev.clientY);
} };


kyfunc[3]=function(ev) { 
dltay=ev.deltaY;
if(dltay>50){
vih=vio.height-200;
if(vih>dfheight){zmout();}
else {vio.height=dfheight+70;}

}
else if(dltay<-50){zmin();}
};

function ratechange()
{
vio.playbackRate=plbrate;
yput.value='r'+plbrate.toFixed(2);
}

function klirlup()
{
if(intervalHandle){
clearInterval(intervalHandle);
intervalHandle=null;
yput.value='';
}
else{yput.value+='/'+vio.currentTime.toFixed(2)+'/2.0123/\n';}


}

var panni=false;


kyfunc[0]=function(e) {



	switch (e.keyCode) {

	case 66:
	yput.value+='rightness(1.2)\n';
	return;
case 67:
yput.value+='ontrast(1.5)\n';
return;

case 83:
yput.value+='aturate(2.5)\n';
return;

case 72:
yput.value+='ue-rotate(30deg)\n';
return;

case 73:
yput.value+='nvert(1)\n';
return;


case 80:
yput.value+='sepia(0)\nblur(0px)\n';
return;

	}
};


kyfunc[1]=function(e) {
//vio.muted=false;
if(panni)
{
	switch (e.keyCode) {

	case 27:
	case 106:
	case 112:
		close();
		return;

	case 83:
		klirlup();
		return;
	case 111:
		panni=false;
		rmvpan();
		return;
	case 104:
		document.body.scrollTop-=50;
		return;

	case 98:
		document.body.scrollTop+=50;
		return;
	case 100:
		document.body.scrollLeft-=50;
		return;
	case 102:
		document.body.scrollLeft+=50;
		return;

	case 103:
		document.body.scrollLeft-=50;
		document.body.scrollTop-=50;
		return;

	case 105:
		document.body.scrollLeft+=50;
		document.body.scrollTop-=50;
		return;
	case 97:
		vio.currentTime-=2;
		return;
	case 99:
		vio.currentTime+=2;
		return;
	case 88:
		plbrate-=0.1;
		if(plbrate<0.2)
		{plbrate=0.2;}
		ratechange();
		return;
	case 65:
		

		vio.height-=100;

		calcscall();
			
		return;

	case 68:
		
		vio.height+=100;

		calcscall();
		
		return;

	case 87:
		plbrate+=0.1;
		ratechange();
		return;
	}

return;
}


	switch (e.keyCode) {



	case 27:
	case 106:
	case 112:
		close();
		return;

	case 83:
		klirlup();
		return;
	case 111:
		zm600();

		//panni=true;
		return;

	case 98:
		plbrate-=0.1;
		if(plbrate<0.2)
		{plbrate=0.2;}
		ratechange();
		return;
	case 97:
		vio.currentTime-=2;
		return;
	case 99:
		vio.currentTime+=2;
		return;
	case 100:
		zmout();
			
		return;
	case 101:
		vio.style.margin='auto';
		plbrate=1.0;
		ratechange();
		return;
	case 102:
		zmin();
		
		
		return;
	case 103:
		rotvi([null,true]);	//270,-1,1
		break;
	case 104:
		plbrate+=0.1;
		ratechange();
		return;
	case 105:
		rotvi([null,false]);	//90
		return;



	}
};

document.onkeyup=kyfunc[1];


document.onwheel=function(e){
if(e.deltaY<-100) {
document.onwheel=null;
zm600();}};
