

var ifrm = new Array(scril);
var ifmarea = document.getElementById('ifm');

function prepg()
{
	var dstr="<h3>";
	for(var i=0;i<scril;i++){dstr+='<a href="../zzzkoero.html?'+i+'">Goto '+i+'</a><br>';}
	ifmarea.innerHTML=dstr+"</h3>";
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

function gall(ele)
{
	
	ifmarea.innerHTML='';
	ifmarea.style='';
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


