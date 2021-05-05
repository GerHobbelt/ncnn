// ==UserScript==
// @match https://video.twimg.com/*
// ==/UserScript==


var vio=document.getElementsByTagName("video")[0];
if(vio=== undefined){setTimeout(function() { close(); }, 500);}



var plbrate=1.0;
var isrot=false;

vio.loop=true;


vio.style.maxHeight='800%';
vio.style.maxWidth='800%';


vio.height=window.innerHeight-4;
var intervalHandle = null;

function seclup(sta,endo)
{if(vio.currentTime>endo){vio.currentTime=sta;}}

function effeci()
{
var sst=yput.value;
if(sst)
{

var c0=sst.charAt(0);
if(c0=='!')
{
var sn=sst.split('!');
var vsta=parseFloat(sn[1]);
var vend=parseFloat(sn[2])+vsta;
intervalHandle=setInterval(function(){seclup(vsta,vend);}, 1000);
}
else
{
vio.pause();
vio.style.webkitFilter = sst.replace('\n',' ');
}
return;
}
vio.style.webkitFilter = '';
}

function kuntinu(){
vio.play();
}

function createeffctctrl()
{
var oyput = document.createElement('div');
oyput.style='position:fixed;right:0px;top:0px;';
oyput.innerHTML='<textarea rows=8 >brightness(1)\ncontrast(1)\nsaturate(1)\nhue-rotate(0deg)\nblur(0px)\ninvert(0)\nsepia(0)</textarea>';
document.body.insertBefore(oyput,vio);
oyput=oyput.firstChild;
oyput.ondblclick=effeci;
oyput.onblur=kuntinu;


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

function calcscall()
{
var tmxw=vio.scrollWidth/window.outerWidth;
var tmxh=vio.scrollHeight/window.outerHeight;

if(tmxw>1.0&&tmxh>1.0)
{
mxw=tmxw-0.9;
mxh=tmxh-0.9;
return true;
} 
return false;
	
}

function ruu(x,y)
{
document.body.scrollLeft=x*mxw;
document.body.scrollTop=y*mxh;
}



//function fakekuriakey(){return;}


var mufunc0=function(ev) {

delayii++;

if(delayii > 0x14) {
	delayii=0x0;
	ruu(ev.clientX,ev.clientY);
} };

function ratechange()
{
vio.playbackRate=plbrate;
document.title="rate="+plbrate;
}

function klirlup()
{
if(intervalHandle){
clearInterval(intervalHandle);
intervalHandle=null;
}
else{yput.value='!0.0!2.0';}


}

var panni=false;
var notplu500=true;

document.onkeydown=function(e) {
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
		vio.controls=true;
		document.onmousemove=null;
		document.body.style.overflow = "auto";
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
	case 99:
		document.body.scrollLeft+=50;
		document.body.scrollTop+=50;
		return;
	case 105:
		document.body.scrollLeft+=50;
		document.body.scrollTop-=50;
		return;
	case 97:
		document.body.scrollLeft-=50;
		document.body.scrollTop+=50;
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
		if(!isrot){
		vio.style.margin= '0px';
		if(notplu500)
		vio.height+=600;
		notplu500=false;
		}

	
		if(calcscall()){
			vio.controls=false;
			document.body.style.overflow = "hidden";
			document.onmousemove=mufunc0;
		}

		panni=true;
		return;

	case 98:
		plbrate-=0.1;
		if(plbrate<0.2)
		{plbrate=0.2;}
		ratechange();
		return;
	case 100:
			if(!isrot){vio.style.margin= '0px';}
			vio.height-=100;
			
		return;
	case 101:
			vio.style.margin='auto';
			plbrate=1.0;
			ratechange();
		return;
	case 102:
		if(!isrot){vio.style.margin= '0px';}
		vio.height+=100;
		notplu500=false;
		
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