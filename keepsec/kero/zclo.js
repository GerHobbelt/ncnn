
var tblarea = document.getElementById('team');
var recarea = document.getElementById('urlrec');
var timgarea = document.getElementById('timg');
var erocount=Math.ceil(thumb.length/10);
var keyerocount=curEro+50;


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
		if(srck.slice(-6)==":thumb")
		{
			zsrcrec+='\n'+srck.slice(0,-6);
		}
		
	}
	
	}
	recarea.innerHTML+=zsrcrec;

	return zsrcrec;

}


function disabsk()
{
	keyerocount=0;
}


const nwvv1='<html><body style="background-color: black;"><center><video width="auto" id="media" controls loop muted autoplay><source src="';
const nwvv2='" type="video/mp4"></video></center></body><scr'+'ipt src="zvid.js"></scr'+'ipt></html>';

function nwdvid(vidurl)
{
	var newWindow = window.open(vidurl, null, null);

	//var newWindow = window.open("", null, null);
	//newWindow.document.write(nwvv1+vidurl+nwvv2);

}

function repg(ele)
{
	ele.blur();
	recarea.innerHTML+='\n\n'+erocount.toString(10);
	var evv=curEro+(ele.value*10);
	if(evv<0){evv=0;}
	if(evv>erocount){evv=erocount-10;}

	
		curEro=evv;
		tblarea.innerHTML="";
		
		
		menuFunction();
	

}

function vidstr(src)
{
	var c0=src.charAt(0);
	if(c0=='@')
	{return 'https://video.twimg.com/amplify_video/'+src.substring(1);}
	else if(c0=='!')
	{return 'https://video.twimg.com/tweet_video/'+src.substring(1);}
	else
	{return 'https://video.twimg.com/ext_tw_video/'+src;}
}

function thumbstr(src)
{
	var c0=src.charAt(0);
	if(c0=='@')
	{return 'https://pbs.twimg.com/amplify_video_thumb/'+src.substring(1);}
	else if(c0=='!')
	{return 'https://pbs.twimg.com/tweet_video_thumb/'+src.substring(1);}
	else if(c0=='h')
	{return src;}
	else
	{return 'https://pbs.twimg.com/ext_tw_video_thumb/'+src;}
}

var agen=['left','right'];

const kx1='<a href="https://twitter.com/';
const kx2='" >⛪　　　　　　　　</a><a onmouseenter=xt(';
const kx2b='" >====</a><br><a href="';
const kx3=') onclick=xtp() >✨</a><br><a href="';
const kx4='" ><img src="';
const kx5a=':thumb" width="183"/></a>0<br>';
const kx5b='" /></a><br>';

function mydav(sta, endo,ag)
{
	yina = document.createElement('div');
	yina.style['float'] = 'right';
	//yina.style.webkitTransform = 'rotate(90deg)'; 
	//yina.style['background-color']='black';
	//yina.style.width=210;
	
	var kole7="";
	for(var j=sta;j<endo;j++){
var iszrda=curEro*10+j;
kole7+=kx1+msgs[iszrda]+
kx2+iszrda+
kx3+vidstr(vids[iszrda])+
kx4+thumbstr(thumb[iszrda])+
kx5a;

	}
	yina.innerHTML = kole7;
	tblarea.appendChild(yina);
}
var nymu=-10;
function xtp()
{
recarea.innerHTML+='\nhttps://twitter.com/'+msgs[nymu];
}

function xt(iszrda)
{
	nymu=iszrda;
	var tsstr=thumbstr(thumb[iszrda]);
	document.body.background = tsstr;
	timgarea.src=tsstr;
	timgarea.style.maxHeight = '800%';
}

function rmvimg()
{
if(nymu>=0){
	timgarea.src='';
	timgarea.style.maxHeight = '1%';
	nymu=-1;
}
}

function apyed()
{
	mydav(0,5,0);
	mydav(5,10,0);
	//mydav(0,4,0);
	//mydav(4,6,1);
	//mydav(6,10,0);
	rmvimg();
	
	curEro++;
	
}



function menuFunction() {
	if(keyerocount<0){
	tblarea.innerHTML='';
	document.onmousemove=kuriakey;};

	if(curEro<erocount){apyed();
	keyerocount=curEro+50;
	if(keyerocount>erocount){keyerocount=erocount;}
	document.title=(curEro*10) + " to "+ (keyerocount*10);
	return false;}
	
}
window.oncontextmenu=menuFunction;
menuFunction();

function kuriakey(){keyerocount=curEro+50;}
function fakekuriakey(){return;}

function fpt(e,ele)
{
if(e.keyCode==13)
{
var evv=ele.value;
	if(evv<0){curEro+=evv;}
	else
	{
		if(evv>erocount){evv=erocount-10;}
		curEro=evv;
	}


ele.blur();
fullpg();
}
}

function fullpg()
{
	document.body.background='';
	nymu=10;
	rmvimg();


	var kole7='<div class="nv">　　　　　　　　　　　<input type="number" value='+
curEro+' onkeyup=fpt(event,this)>⛪</div><center>';

	for(var j=0;j<10;j++){
		var iszrda=curEro*10+j;
		kole7+=kx1+msgs[iszrda]+
		kx2b+vidstr(vids[iszrda])+
		kx4+thumbstr(thumb[iszrda])+
		kx5b;
	}
	
	tblarea.innerHTML =kole7+'</center>';
	document.title='fpg'+curEro;
	keyerocount=-100;
	document.onmousemove=fakekuriakey;

}


document.onmousemove=kuriakey;

document.onkeydown=function(e) {

if(keyerocount<0){
switch (e.keyCode) {
	case 65:
	case 106:
	
		curEro=Math.floor(Math.random()*erocount);
		fullpg();
	return;

	case 109:
		curEro--;
		fullpg();
	return;

	case 107:
		curEro++;
		fullpg();
	return;
}
return;
}


switch (e.keyCode) {
	case 65:
		curEro=Math.floor(Math.random()*erocount);
		fullpg();
	return;
	case 105:
	case 106:
		menuFunction();
		document.body.scrollTop+=300;
	break;
	case 102:
	case 39:
		document.body.scrollLeft+=500;
		document.body.scrollTop+=3;
	break;
	case 100:
	case 37:
		document.body.scrollLeft-=500;
		if(document.body.scrollLeft<600){document.body.background='';}
		document.body.scrollTop+=3;
	break;
	case 101:

		var evv=curEro+Math.floor((Math.random()-0.5)*erocount/7);
		if(evv>=0&&evv<erocount){
		curEro=evv;
		menuFunction();
		document.body.scrollTop+=300;
		}
	break;

	case 104:
		document.body.scrollTop-=600;
	break;

	case 98:
		document.body.scrollTop+=300;
	break;
}

	if(curEro<keyerocount){apyed();
	document.title=curEro*10;}

}

