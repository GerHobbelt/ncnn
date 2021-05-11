
var tblarea = document.getElementById('team');
var recarea = document.getElementById('urlrec');
var timgarea = document.getElementById('timg');
var erocount=0;
var keyerocount=0;
var hardlim=0;


var RDMarr=null;
var iRDMarr=0xF00000;

function RDMcurEro()
{
	if(iRDMarr>=erocount)
	{
		iRDMarr=0;
		RDMarr=mkRDMarr(erocount);
	}
	curEro=RDMarr[iRDMarr];
	iRDMarr++;
}

function chkdst(arr,y)
{
	adst=arr[y];
	if(adst===undefined){return y;}
	return adst;
}

function mkRDMarr(num)
{arr=new Array(num);
zsta=(num>>1);
for(i=0;i<zsta;i++)
{
	nx=1+((Math.random() *zsta)<<1);

	y=i<<1;
	

	//adst=arr[nx];
	//if(adst===undefined){adst=nx;}
	arr[y]=chkdst(arr,nx);//adst;

	y1=y+1;

	//adst=arr[y1];
	//if(adst===undefined){adst=y1;}
	arr[nx]=chkdst(arr,y1);//adst;

	arr[y1]=y;
	
}


yz=num-2;

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







return arr;}

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
	setTimeout(skrback, 1000);
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
					else{
						ymgg.alt+='\n^'+niki+'\n';
						niki=' href="https://twitter.com/'+niki+'/with_replies">⛔</a>'};
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
	document.title=curEro + "0 to "+ keyerocount+'0 key';
	return false;}
	
}



function symfire()
{
	apyed();
	canfire=true;
}
var mouseRDM=false;

function kuriakey(){
	if(canfire)
	{
		canfire=false;
		
		if(mouseRDM){RDMcurEro();}
		else if(curEro>=hardlim){return;}
		
		keyerocount=curEro+50;
		setTimeout(symfire, 1200);
		
		
		
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
	document.title=curEro+'0--pfpg';
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
	document.title=curEro+'0--fpg';
	keyerocount=-100;
	document.onmousemove=null;

}



function installclo(){


erocount=Math.ceil(thumb.length/10);

if(curEro>erocount){curEro=erocount-10;}

keyerocount=curEro+50;
hardlim=curEro+501;

window.oncontextmenu=menuFunction;
menuFunction();
document.onmousemove=kuriakey;

document.onkeydown=function(e) {

if(keyerocount<0){
switch (e.keyCode) {
	case 27:
	case 90:
	case 101:
	case 106:
	case 112:
		
		RDMcurEro();
		fullpg();
	return;

	case 81:
		uv=tblarea.id.split('.');
		if(uv.length>1) { curEro=parseInt(uv[1],10);}
		else {curEro=0;}
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
	case 65:
		mouseRDM=false;
		keyerocount=0;
		hardlim=0;
		setTimeout(function() {document.execCommand('copy');recarea.focus();}, 500);
		
	return;
	case 81:
		RDMcurEro();
		fullpg();
		document.body.background='';
		timgarea.src='';
		timgarea.style.maxHeight = '1%';

		
	return;
	case 105:
	case 106:
		mouseRDM=false;
		menuFunction();
		document.body.scrollTop+=300;
	return;

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
		mouseRDM=true;
		RDMcurEro();
		keyerocount=curEro+50;
		hardlim=curEro+501;
		symfire();
		document.body.scrollTop+=300;
	return;

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
}

function mkscript(sksrc)
{
	sk = document.createElement('script');
	sk.src=sksrc;
	document.body.append(sk);
	return sk;
}


function insp(apa)
{

uv=location.href.split('#');
if(uv.length>1){
uvv1=uv[1];
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