// ==UserScript==
// @match https://www.youtube.com/embed/*
// @match https://www.youtube.com/watch?v=*
// @match http://embed.nicovideo.jp/watch/*
// ==/UserScript==



var vio=null;
var yput=null;
var pll=null;

var kyfunc = new Array(3);


var plbrate=1.0;
var isrot=false;


var intervalHandle = null;


function toclpb()
{
	yput.select();
	document.execCommand('copy');
}

function effeci(nkyfun)
{
document.onkeyup=kyfunc[nkyfun];
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

function createeffctctrl(pa,bifo)
{
var oyput = document.createElement('div');
oyput.style='position:fixed;right:0px;top:0px;color:white;';
oyput.innerHTML='<textarea rows=1 ></textarea>';
pa.insertBefore(oyput,bifo);
oyput=oyput.firstChild;

return oyput;
}




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
var notplu500=true;

function ytpaosa()
{
	pll.style.width='88%';
	paosa();
	setTimeout(function() { yput.focus(); }, 500);
}

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



	switch (e.keyCode) {

	case 81:
		
		ytpaosa();
		return;



	case 83:
		klirlup();
		return;


	case 88:
		plbrate-=0.1;
		if(plbrate<0.1)
		{plbrate=0.1;}
		ratechange();
		return;


	
	case 87:
		plbrate+=0.1;
		ratechange();
		return;
	



	}
};

var ytemburl=null;

kyfunc[2]=function(e) {



	switch (e.keyCode) {

	case 81:
		window.location.href = 'https://www.youtube.com/embed/'+ytemburl;
		
		return;



	case 83:
		klirlup();
		return;


	case 88:
		plbrate-=0.1;
		if(plbrate<0.1)
		{plbrate=0.1;}
		ratechange();
		return;


	
	case 87:
		plbrate+=0.1;
		ratechange();
		return;
	



	}
};



function yteffeci(){
pll.style.width='100%';
effeci(1);
}

function yt2effeci(){
effeci(2);
}

function waitinstall()
{
var flx=document.getElementById('flex');
if(flx)
{
var oyput = document.createElement('div');
oyput.innerHTML='<textarea rows=1 ></textarea>';
flx.appendChild(oyput);
yput=oyput.firstChild;

yput.onfocus=paosa;
yput.onblur=yt2effeci;
document.onkeyup=kyfunc[2];
vio=document.getElementsByTagName("video")[0];
clearInterval(intervalHandle);
intervalHandle=null;
}

}

function installhk()
{
	var uv=location.href.split('/');
	if(uv[2]=='www.youtube.com')
	{
		if(uv[3]=='embed')
		{
			vio=document.getElementsByTagName("video")[0];

			var ppll=document.getElementById('player');
			

			pll=ppll.firstChild;
		

			

			yput=createeffctctrl(document.body,ppll);
			
			yput.onblur=yteffeci;
			document.onkeyup=kyfunc[1];
			
			
			var trash1=document.getElementsByClassName('ytp-pause-overlay ytp-scroll-min ytp-scroll-max')[0];
			trash1.innerHTML='';
			trash1.className='';
			trash1=document.getElementsByClassName('ytp-gradient-top')[0];
			trash1.className='';
			
			
			
		}
		else
		{
			var uv1=uv[3].split('=');
			if(uv1[0]=='watch?v')
			{

				
				ytemburl=uv1[1];
				intervalHandle=setInterval(waitinstall, 1000);

				
				


				
			}
			
		}
	}
	else if(uv[2]=='embed.nicovideo.jp')
	{

		var oyput = document.createElement('div');
		oyput.innerHTML='<textarea rows=1 ></textarea>';
		document.getElementsByClassName('f171bmb6')[1].appendChild(oyput);
		yput=oyput.firstChild;
		
		yput.onfocus=paosa;
		yput.onblur=yt2effeci;
		


		vio=document.getElementsByTagName("video")[0];
		document.onkeyup=kyfunc[2];
	}

}

installhk();