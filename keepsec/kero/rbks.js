

var ifrm = new Array(scril);
var ifmarea = document.getElementById('ifm');

function prepg()
{
	var dstr="<h2>";
	for(var i=0;i<scril;i++){dstr+='<a href="../zzzkoero.html#'+i+'" target=_top>Goto '+i+'</a><br>';}
	ifmarea.innerHTML=dstr+"</h2><br><br><br><h3 onclick=mklokal() >Mp4Disk</h3>";
}

prepg();

const tx1='<html><body><pre id="t"></pre></body><scr'+'ipt src="pv/aadata.';
const tx2='.js"></scr'+'ipt><scr'+'ipt src="rpre.js"></scr'+'ipt></html>';

var loadcount=0;
function fyna()
{
	loadcount++;
	if(loadcount==scril) {for(var i=0;i<scril;i++){ifrm[i].outerHTML=ifrm[i].contentDocument.getElementById('t').outerHTML;}}
}

var donotgall=false;

function gall(ele)
{
	if(donotgall){return;}
	
	ifmarea.innerHTML='';
	for(var i=0;i<scril;i++)
	{
		var iframee = document.createElement('iframe');
		ifmarea.appendChild(iframee);
		ifrm[i]=iframee;

	}


	for(var i=0;i<scril;i++)
	{
		var jls=(scril-1-i);
		ifrm[i].contentDocument.write(tx1+jls+tx2);
		ifrm[i].contentDocument.close();
		ifrm[i].onload=fyna;
	}
	ele.outerHTML ='';
	return;
	
}
function printlokal()
{
	document.body.style.backgroundColor='black';
	var ll=locfi.length;
	locfi[0]='<a href="vidmga.htm">======mga======</a>';
	for(var i=1;i<ll;i++)
	{
		locfi[i]='<a href="tu/lu/'+locfi[i]+'.mp4">'+locfi[i]+'</a>';
	}
	ifmarea.innerHTML=locfi.join('  ');
	donotgall=true;
}

function mklokal()
{
	sk = document.createElement('script');
	sk.src='locfi.js';
	document.body.append(sk);
	sk.onload=printlokal;

}
