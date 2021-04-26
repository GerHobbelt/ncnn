
var tblarea = document.getElementById('team');
var recarea = document.getElementById('urlrec');
var timgarea = document.getElementById('timg');
var erocount=thumb.length/10;
var keyerocount=curEro+20;


function disabsk()
{
	keyerocount=0;
}


var nwvv1='<html><body style="background-color: black;"><center><video width="auto" id="media" controls loop muted autoplay><source src="';
var nwvv2='" type="video/mp4"></video></center></body><scr'+'ipt src="zvid.js"></scr'+'ipt></html>';

function nwdvid(vidurl)
{
	var newWindow = window.open(vidurl, null, null);

	//var newWindow = window.open("", null, null);
	//newWindow.document.write(nwvv1+vidurl+nwvv2);

}

function repg(ele)
{
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
kole7+='<a href="https://twitter.com/'+msgs[iszrda]+
'" >⛪　　　　</a><a onmouseenter=xt('+iszrda+
') onclick=xtp() >✨</a><br><a href="'+vidstr(vids[iszrda])+
'" ><img src="'+thumbstr(thumb[iszrda])+
':thumb" width="183"/></a>0<br>';

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
if(nymu>=0)
{
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
	if(curEro<erocount){apyed();
	keyerocount=curEro+20;
	if(keyerocount>erocount){keyerocount=erocount;}
	document.title=(curEro*10) + " to "+ (keyerocount*10);
	return false;}
	
}
window.oncontextmenu=menuFunction;
menuFunction();

document.onkeydown=function(e) {
switch (e.keyCode) {
	case 102:
	case 39:
		document.body.scrollLeft+=500;
	break;
	case 100:
	case 37:
		document.body.scrollLeft-=500;
		if(document.body.scrollLeft<600){document.body.background='';}
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
	document.body.scrollTop+=600;
	break;
}
	if(curEro<keyerocount){apyed();
	document.title=curEro*10;}
}

