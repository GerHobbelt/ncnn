// ==UserScript==
// @match https://video.twimg.com/*
// ==/UserScript==


var vio=document.getElementsByTagName("video")[0];
if(vio=== undefined){setTimeout(function() { close(); }, 500);}

var kyfunc = new Array(2);

var plbrate=1.0;
var isrot=false;

vio.loop=true;


vio.style.maxHeight='800%';
vio.style.maxWidth='800%';


vio.height=window.innerHeight-4;
var intervalHandle = null;


function effeci()
{
document.onkeyup=kyfunc[1];
var sst=yput.value;
yput.rows=1;
if(sst)
{
	var c0=sst.charAt(0);
	if(c0=='/')
	{
		if(intervalHandle){clearInterval(intervalHandle);intervalHandle=null;}
		var sn=sst.split('/');
		var vsta=parseFloat(sn[1]);
		var fvend=parseFloat(sn[2]);
		if(fvend<0){
			fvend=-(vsta+fvend);
			yput.value='/'+vsta.toFixed(2)+'/'+fvend.toFixed(2)+'/\n';
		}
		
		vio.currentTime=vsta;
		intervalHandle=setInterval(function(){vio.currentTime=vsta;}, Math.ceil(fvend*(1000.5/plbrate)));
	} else if(c0=='r') {
		if(sst.length<3)
		{
			plbrate=1.0;
			
		}
		else
		{
			plbrate=parseFloat(sst.substring(1));
			
		}
		vio.playbackRate=plbrate;

	} else if(c0=='o') {
		if(isrot){rotvi('0');}
		rotvi(sst.substring(1));

	} else{vio.style.webkitFilter = sst.replace('\n',' ');}

	vio.play();
	return;
}
	vio.style.webkitFilter = '';
	vio.play();
}

function paosa(){

	document.onkeyup=kyfunc[0];
	yput.rows=8;
	if(intervalHandle){return;}
	else{vio.pause();}
}

function createeffctctrl()
{
var oyput = document.createElement('div');
oyput.style='position:fixed;right:0px;top:0px;color:white;';
oyput.innerHTML='<textarea rows=1 ></textarea>';
document.body.insertBefore(oyput,vio);
oyput=oyput.firstChild;
oyput.onfocus=paosa;
oyput.onblur=effeci;


return oyput;
}

var yput=createeffctctrl();


function rotvi(krot)
{
vio.style.margin='auto';
if(isrot)
{

vio.style.webkitTransform='';
vio.height=window.innerHeight-4;

isrot=false;
}
else
{

vio.style.webkitTransform = 'rotate('+krot+'deg)'; 
vio.height=window.outerWidth-50;


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
var tmxw=vio.scrollWidth/window.outerWidth;
var tmxh=vio.scrollHeight/window.outerHeight;

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
	document.onmousemove=mufunc0;
	document.body.style.overflow = "hidden";
}

function rmvpan()
{
	isnotpan=true;
	vio.controls=true;
	document.onmousemove=null;
	document.body.style.overflow = "auto";
}



function ruu(x,y)
{
document.body.scrollLeft=x*mxw;
document.body.scrollTop=y*mxh;
}






var mufunc0=function(ev) {

delayii++;

if(delayii > 0x14) {
	delayii=0x0;
	ruu(ev.clientX,ev.clientY);
} };

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
}
else{yput.value+='/'+vio.currentTime.toFixed(2)+'/2.0/\n';}


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

case 76:
yput.value+='blur(0px)\n';
return;

case 80:
yput.value+='sepia(0)\n';
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
		if(!isrot){vio.style.margin= '0px';
		if(vio.scrollWidth<(window.innerWidth+50)){vio.height+=600;}
		}
		

	
		if(calcscall()){installpan();}

		panni=true;
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
			if(!isrot){vio.style.margin= '0px';}
			vio.height-=100;
			if(!calcscall()){if(!isnotpan){rmvpan();}}
			
		return;
	case 101:
			vio.style.margin='auto';
			plbrate=1.0;
			ratechange();
		return;
	case 102:
		if(!isrot){vio.style.margin= '0px';}
		vio.height+=100;
		if(calcscall()){if(isnotpan){installpan();}}
		
		return;
	case 103:
		rotvi('270');
		break;
	case 104:
		plbrate+=0.1;
		ratechange();
		return;
	case 105:
		rotvi('90');
		return;



	}
};

document.onkeyup=kyfunc[1];