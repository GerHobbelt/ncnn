
var vio=document.getElementById('media');


var kyfunc = new Array(4);

var ispause=false;
var isrot=false;

vio.loop=true;


vio.style.maxHeight='800%';
vio.style.maxWidth='800%';

var plbrate=0.1;

var srcnum=0;

var delayii=0x0;
var mxw=0;
var mxh=0;

const sl1=1.1;
const sl2=0.9;

function createeffctctrl()
{
var oyput = document.getElementById('ymg');

oyput.onfocus=paosa;
oyput.onblur=effeci;


return oyput;
}

var yput=createeffctctrl();


var dfheight=window.innerHeight+500;
vio.height=dfheight;

var intervalHandle = null;


var shrinkbefore=false;




function toclpb()
{
	yput.select();
	document.execCommand('copy');
}

function effeci()
{
document.onkeyup=kyfunc[1];
document.onmousemove=kyfunc[2];
document.onwheel=kyfunc[3];
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
		//toclpb();
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

	} else if(c0=='l') {
		vio.height=parseInt(sst.substring(1),10);
		if(calcscall()){installpan();}

	} else if(c0=='z') {
		vio.src = 'tu/lu/mga/'+sst.substring(1)+'.mp4';
		ratechange();
	} else{
		
		vio.style.webkitFilter = sst.replace('\n',' ');
		//toclpb();

	}

	
	

	
}
else {vio.style.webkitFilter = '';}


ispause=false;
vio.play();
}


function rdce()
{
	if(!isrot){vio.style.margin= '0px';}
	vio.height-=100;
	shrinkbefore=true;
}

function zmin()
{
	shrinkbefore=false;
	if(!isrot){vio.style.margin= '0px';}
	vuvu=vio.height;
	vuvu+=100;
	yput.value='l'+vuvu;
	vio.height=vuvu;
	
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
	if(!isrot){vio.style.margin= '0px';
	vio.height+=900;
	}



	if(calcscall()){installpan();}
}




function paosa(){
	document.onwheel=null;
	document.onmousemove=null;
	if(vio.scrollWidth>1700){rdce();}

	document.onkeyup=kyfunc[0];
	yput.rows=8;
	if(intervalHandle){return;}
	else{vio.pause();}
}



function rotvi(krot)
{
vio.style.margin='0px';
if(isrot)
{

vio.style.webkitTransform='';
vio.height=dfheight;

isrot=false;
}
else
{

vio.style.webkitTransform = 'rotate('+krot+'deg)'; 
vio.height=window.outerWidth-50;


isrot=true;
}


}





function calcscall()
{
var tmxw=vio.scrollWidth/window.outerWidth;
var tmxh=vio.scrollHeight/window.outerHeight;

if(tmxw>sl1&&tmxh>sl1)
{
mxw=tmxw-sl1;
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
document.body.scrollLeft=x*mxw;
document.body.scrollTop=y*mxh;
}






kyfunc[2]=function(ev) {

delayii++;

if(delayii > 0xA) {
	delayii=0x0;
	ruu(ev.clientX,ev.clientY);
} };


kyfunc[3]=function(ev) { 
dltay=ev.deltaY;
if(dltay>50&&vio.height>(dfheight+150)){zmout();}
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
}
else{yput.value+='/'+vio.currentTime.toFixed(2)+'/2.0/\n';}


}

function swipao()
{
if(ispause){vio.play(); ispause=false;}
else {vio.pause(); ispause=true;}
return false;
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



	switch (e.keyCode) {



	case 27:
	case 106:
	case 112:
		srcnum++;
		iyk='tu/lu/mga/'+srcnum+'.mp4';
		vio.src = iyk;
		ratechange();
		vio.play();
		ispause=false;
		yput.value=iyk;
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
		vio.currentTime-=0.7;
		return;
	case 99:
		vio.currentTime+=0.7;
		return;
	case 100:
		zmout();
			
		return;
	case 101:
		swipao();
		return;
	case 102:
		zmin();
		
		
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
window.oncontextmenu=swipao;
ratechange();
zm600();