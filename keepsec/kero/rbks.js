

var ifrm = new Array(scril);
var ifmarea = document.getElementById('ifm');

function prepg()
{
	var dstr="<h2>";
	for(var i=0;i<scril;i++){dstr+='<a href="../zzzkoero.html#'+i+'" target=_top>Goto '+i+'</a><br>';}
	ifmarea.innerHTML=dstr+"</h2><br><br><br><h3 onclick=mklokal() >Mp4Disk</h3>";

	document.body.style.backgroundColor='#400010';

	var lvdb=localStorage.getItem('blmpg');
	if(!lvdb)
	{
		localStorage.setItem('blmpg', '<feGaussianBlur stdDeviation="5" result="bml"/><feBlend mode="screen" in="bml" in2="SourceGraphic" />');
		localStorage.setItem('blm', '<feConvolveMatrix preserveAlpha="true" order="3 3" kernelMatrix="0 -1 0 -1 5 -1 0 -1 0" in="SourceGraphic" result="sha"/><feGaussianBlur stdDeviation="3" result="bml"/><feBlend mode="screen" in="bml" in2="sha" />');
	}

}

prepg();

//const tx1='<html><body><pre id="t"></pre></body><scr'+'ipt src="pv/aadata.';
const tx1='<html><body></body><scr'+'ipt>var thiid=';
const tx1b=';</scr'+'ipt><scr'+'ipt src="pv/aadata.';
const tx2='.js"></scr'+'ipt><scr'+'ipt src="rpre.js"></scr'+'ipt></html>';

var loadcount=0;
function fyna_old()
{
	loadcount++;
	if(loadcount==scril) {for(var i=0;i<scril;i++){ifrm[i].outerHTML=ifrm[i].contentDocument.getElementById('t').outerHTML;}}
}

function fyna()
{
	//downloadString(this.contentDocument.getElementById('t').innerText,'text/csv','rlines.'+this.id);
	this.remove();
}


var donotgall=false;




function gall(ele)
{
	
	if(donotgall){
	shuflocfi(locfi.length);
	return;}
	

	document.body.style.backgroundColor='white';
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
		ifrm[i].id=jls;
		ifrm[i].contentDocument.write(tx1+jls+tx1b+jls+tx2);
		ifrm[i].contentDocument.close();
		//ifrm[i].onload=fyna;
	}
	ele.outerHTML ='';
	return;
	
}





function shuflocfi(ll)
{
	var zsta=ll>>1;
	for(var i=0;i<zsta;i++)
	{
		y=i<<1;
		nx=1+((Math.random() *zsta)<<1);
		tmp=locfi[y];
		locfi[y]=locfi[nx];
		locfi[nx]=locfi[y+1];
		locfi[y+1]=tmp;
	}
	ibz=2;
	if(ll&1){ibz=3;}

	for(var i=1;i<ibz;i++)
	{
		zsta=ll-i;
		nx=(Math.random() *ll)>>1;
		tmp=locfi[nx];
		locfi[nx]=locfi[zsta];
		locfi[zsta]=tmp;
	}

	ifmarea.innerHTML=locfi.join('  ');
}

function printlokal()
{
	document.body.style.backgroundColor='black';
	var ll=locfi.length;
	locfi[0]='<a href="vidmga.htm">======mga======</a>';
	for(i=1;i<ll;i++)
	{
		locfi[i]='<a href="tu/lu/'+locfi[i]+'.mp4">'+locfi[i]+'</a>';
	}

	shuflocfi(ll);
	donotgall=true;
}

function mklokal()
{
	var sk = document.createElement('script');
	sk.src='locfi.js';
	document.body.append(sk);
	sk.onload=printlokal;

}
