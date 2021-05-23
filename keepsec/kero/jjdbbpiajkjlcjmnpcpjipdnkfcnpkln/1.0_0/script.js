// ==UserScript==
// @name YT&NiCo
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


var SVGf=null;
const SVGheader='http://www.w3.org/2000/svg';

function ParseSVGf(sst)
{
	var ftr_elem=document.createElementNS(SVGheader, 'filter');
	var syp= sst.indexOf('\n');
	var ftr_id=sst.substring(1,syp);
	ftr_elem.id=ftr_id;
	ftr_elem.innerHTML=sst.substring(syp);
	SVGf.appendChild(ftr_elem);
	yput.value='>>'+ftr_id;
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



function effeci(nkyfun)
{
document.onkeyup=kyfunc[nkyfun];
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
			if(syp==2){vio.style.webkitFilter='';break;}
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
else {vio.style.webkitFilter = '';}

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
oyput.innerHTML='<textarea rows=1 ></textarea><svg width=0 height=0 ><defs></defs></svg>';
pa.insertBefore(oyput,bifo);
SVGf=oyput.children[1].firstChild;
oyput=oyput.firstChild;

return oyput;
}



function rotvi(krot)
{
vio.style.margin='auto';

if(isrot) {
	vio.style.webkitTransform='';
	vio.height=dfheight;

	isrot=false;
} else if(krot) {

var typ=krot[0];
if(typ)
{
	
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

var vh=window.innerWidth-4;
if(vioasp<0){
document.onwheel=null;
var lvioasp=dfheight/(vh*(vio.videoWidth/vio.videoHeight));
if(lvioasp>1.0||(Math.abs(lvioasp-1.0)<0.005)){vioasp=0;}
else {vioasp=lvioasp;}
}

var t1=krot[1]?-1.0:1.0;

if(vioasp){t1*=vioasp;}
var t2=-t1;
vio.style.webkitTransform = 'matrix(0,'+t1+','+t2+',0,0,0)';



vio.height=vh;
}


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



case 80:
yput.value+='sepia(0)\nblur(0px)\n';
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