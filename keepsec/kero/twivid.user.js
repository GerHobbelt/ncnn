// ==UserScript==
// @match https://video.twimg.com/*
// ==/UserScript==


var vio=document.getElementsByTagName("video")[0];

if(vio=== undefined){setTimeout(function() { close(); }, 1000);}


var plbrate=1.0;
var isrot=false;

vio.loop=true;


vio.style.maxHeight='800%';
vio.style.maxWidth='800%';
//document.body.style.overflow = "hidden";

vio.height=window.innerHeight-4;

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



function ratechange()
{
vio.playbackRate=plbrate;
document.title="rate="+plbrate;
}
document.onkeydown=function(e) {
    switch (e.keyCode) {
	case 27:
	case 106:
	case 112:
	close();
            break;
        case 98:
	plbrate-=0.1;
	if(plbrate<0.2)
	{plbrate=0.2;}
	ratechange();
            break;
	case 100:
		if(!isrot){vio.style.margin= '0px';}
		vio.height-=100;
		
	break;
	case 101:
		vio.style.margin='auto';
		plbrate=1.0;
		ratechange();
	break;
        case 102:
	if(!isrot){vio.style.margin= '0px';}
	vio.height+=100;
	
            break;
	case 103:
	rotvi('270');
            break;
        case 104:
	plbrate+=0.1;
	ratechange();
            break;
case 105:
	rotvi('90');
            break;



    }
};