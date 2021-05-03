
var tblarea = document.getElementById('team');
var recarea = document.getElementById('urlrec');
var timgarea = document.getElementById('timg');
var erocount=Math.ceil(thumb.length/10);
var keyerocount=curEro+50;
var hardlim=curEro+501;


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
	recarea.value+=zsrcrec;

	return zsrcrec;

}

function printsele()
{
	var lynz=recarea.value.split('\n');
	var lynzl=lynz.length;
	var zsrcrec="\n\n======\n\n";
	for(var i=0;i<lynzl;i++)
	{
		var c0=lynz[i].charAt(0);
		if(c0=='^')
		{
			var yr=parseInt(lynz[i],10);
			zsrcrec+=thumb[i]+'\t'+vids[i]+'\t'+msgs[i]+'\t'+lynz[i]+'\n';

		}
		
	}

	recarea.value+=zsrcrec;
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

var canfire=true;

function repg(ele)
{
	ele.blur();
	timgarea.src='';
	timgarea.style.maxHeight = '1%';

	canfire=true;
	recarea.value+='\n\n'+erocount.toString(10);
	var evv=curEro+(ele.value*10);
	if(evv<0){evv=0;}
	if(evv>erocount){evv=erocount-10;}

	
		curEro=evv;
		hardlim=curEro+501;
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

var speg=['　　','　','　　　　　','　　　','　　　　','　　','　','　　　　　','　　　','　　　　'];

const kx1='<a href="https://twitter.com/';
const kx2b='" >====<br>====</a><br><a href="';
const kx2a1='" >⛪　　　　';
const kx2a2='</a><a title=i';
const kx3=' onmouseover=xt(this) onclick=xtp() >✨</a><br><a href="';
const kx4='" ><img src="';
const kx5a=':thumb" width=205 /></a>0<br>';
const kx5b='" /></a><br>';

function mydav(sta, endo,ag)
{
	yina = document.createElement('div');
	yina.style['float'] = 'right';

	
	var kole7="";
	for(var j=sta;j<endo;j++){
var iszrda=curEro*10+j;
kole7+=kx1+msgs[iszrda]+
kx2a1+speg[j]+kx2a2+iszrda+
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

	recarea.value+='\n^'+nymu;
	document.body.scrollLeft+=2000;
	setTimeout(function() {skrback();}, 1000);
	//recarea.value+='\nhttps://twitter.com/'+msgs[nymu];
}



function xt(ele)
{


var c0=ele.title.charAt(0);

//if(c0=='z'){return;}

var iszrda = parseInt(ele.title.substring(1));


	if(nymu!=iszrda)
	{
		nymu=iszrda;
		ovrcount=0;
		var syyr=thumbstr(thumb[iszrda]);
		switch(c0)
		{
			case 'g':
				timgarea.src=syyr;
			return;
			case 'i':
			{
				var ymgg=ele.nextSibling.nextSibling.firstChild;
				if(ymgg.naturalWidth==0)
				{
					ymgg.alt=msgs[iszrda];
					ymgg.src=syyr;
					ymgg.height=ymgg.width;
					ele.title='r'+iszrda;
					
				}
				else
				{
					ele.title='g'+iszrda;
					
				}
				nymu=-10;
			}
			return;
			case 'r':
			{
				var ymgg=ele.nextSibling.nextSibling.firstChild;
				if(ymgg.naturalWidth==0)
				{
					var niki=msgs[iszrda].split('/')[0];
					if(niki=='i'){niki=' >⛔</a>';}
					else{niki=' href="https://twitter.com/'+niki+'">⛔</a>'};
					ele.outerHTML='<a title=z'+iszrda+niki;
				}
				else
				{
					ymgg.alt='';
					ele.title='g'+iszrda;
					timgarea.src=syyr;
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
			setTimeout(function() {skrback();}, 1000);
			

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
	mydav(0,10,0);
	
	//mydav(0,4,0);
	//mydav(4,6,1);
	//mydav(6,10,0);
	
	
	curEro++;
	
}



function menuFunction() {
	if(keyerocount<0){
	tblarea.innerHTML='';
	document.onmousemove=kuriakey;};

	if(curEro<erocount){apyed();
	rmvimg();
	keyerocount=curEro+50;
	if(keyerocount>erocount){keyerocount=erocount;}
	document.title=(curEro*10) + " to "+ (keyerocount*10);
	return false;}
	
}
window.oncontextmenu=menuFunction;
menuFunction();


function symfire()
{
	apyed();
	document.title=curEro*10;
}

function kuriakey(){
	if(canfire)
	{
		canfire=false;
		if(curEro<hardlim)
		{
			keyerocount=curEro+50;
			setTimeout(function() {symfire(); canfire=true;}, 1200);
		}
		
		
	}
	
}
//function fakekuriakey(){return;}

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
function partpg()
{
	yina = document.createElement('center');
	var kole7='<br>';
	for(var j=0;j<20;j++){
		var iszrda=curEro*10+j;
		kole7+=kx1+msgs[iszrda]+
		kx2b+vidstr(vids[iszrda])+
		kx4+thumbstr(thumb[iszrda])+
		kx5b;
	}
	yina.innerHTML = kole7;
	tblarea.appendChild(yina);
	curEro++;
	curEro++;
	document.title='pfpg'+curEro;
}
function fullpg()
{



	var kole7='<div class="nv">　　　　　　　　　　　<input type="number" value='+
curEro+' onkeyup=fpt(event,this)>⛪</div><center>';

	for(var j=0;j<20;j++){
		var iszrda=curEro*10+j;
		kole7+=kx1+msgs[iszrda]+
		kx2b+vidstr(vids[iszrda])+
		kx4+thumbstr(thumb[iszrda])+
		kx5b;
	}
	
	tblarea.innerHTML =kole7+'</center>';
	document.title='fpg'+curEro;
	keyerocount=-100;
	document.onmousemove=null;

}


document.onmousemove=kuriakey;

document.onkeydown=function(e) {

if(keyerocount<0){
switch (e.keyCode) {
	case 90:
	case 101:
	
		curEro=Math.floor(Math.random()*erocount);
		fullpg();
	return;

	case 81:
		curEro=0;
		menuFunction();
	return;

	case 109:
		curEro--;
		curEro--;
		fullpg();
	return;

	case 107:
		curEro++;
		curEro++;
		fullpg();
	return;

	case 104:
		document.body.scrollTop-=600;
	return;

	case 98:
		document.body.scrollTop+=600;
	return;

}
return;
}


switch (e.keyCode) {
	case 81:
		curEro=Math.floor(Math.random()*erocount);
		fullpg();
		document.body.background='';
		timgarea.src='';
		timgarea.style.maxHeight = '1%';
	return;
	case 105:
	case 106:
		menuFunction();
		document.body.scrollTop+=300;
	break;

	case 90:
	case 107:
		partpg();
		document.body.scrollTop+=1000;
	return;

	case 102:
	case 39:
		document.body.scrollLeft+=500;
		document.body.scrollTop+=10;
	break;
	case 100:
	case 37:
		document.body.scrollLeft-=500;
		if(document.body.scrollLeft<600){document.body.background='';}
		document.body.scrollTop+=10;
	break;
	case 111:

		var evv=curEro+Math.floor((Math.random()-0.5)*erocount/7);
		if(evv>=0&&evv<erocount){
		curEro=evv;
		hardlim=curEro+501;
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

	if(curEro<keyerocount){symfire();}
	//rmvimg();

}

