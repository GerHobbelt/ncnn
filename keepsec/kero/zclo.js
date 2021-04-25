
var tblarea = document.getElementById('team');
var recarea = document.getElementById('urlrec');
//var timgarea = document.getElementById('timg');
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

	if(evv>0&&evv<erocount)
	{
		curEro=evv;
		tblarea.innerHTML="";
		
		keyerocount=curEro+20;
		keyFunction();
	}

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
	yina.style['float'] = 'right';//'left';
	yina.style['background-color']='black';
	
	var kole7="";
	for(var j=sta;j<endo;j++){
var iszrda=curEro*10+j;
kole7+='<a href="https://twitter.com/'+msgs[iszrda]+
'" >⛪　　　　　</a><a onmouseenter=xt('+iszrda+
') onclick=xtp() >✨　　　　　<br></a><a href="'+vidstr(vids[iszrda])+
'" ><img src="'+thumbstr(thumb[iszrda])+
':thumb" width="205"/></a>0<br>';

	}
	yina.innerHTML = kole7;
	tblarea.appendChild(yina);
}
var nymu=0;
function xtp()
{
recarea.innerHTML+='\nhttps://twitter.com/'+msgs[nymu];
}

function xt(iszrda)
{
	nymu=iszrda;
	var tsstr=thumbstr(thumb[iszrda]);
	document.body.background = tsstr;
	//timgarea.src=tsstr;
}

function apyed()
{
	mydav(0,3,0);
	mydav(3,7,1);
	mydav(7,10,0);
	//tblarea.appendChild(brk);
	
	curEro++;
	
}

function keyFunction() {
if(curEro<keyerocount){apyed();
document.title=curEro*10;}
}

function menuFunction() {
	if(curEro<erocount){apyed();
	keyerocount=curEro+20;
	if(keyerocount>erocount){keyerocount=erocount;}
	document.title=(curEro*10) + " to "+ (keyerocount*10);
	return false;}
}

document.onkeydown=keyFunction;
window.oncontextmenu=menuFunction;
keyFunction();