// ==UserScript==
// @match https://video.twimg.com/*
// ==/UserScript==


var vio=document.getElementsByTagName("video")[0];


var plbrate=1.0;
var isrot=false;

vio.loop=true;


vio.style.maxHeight='400%'
vio.style.maxWidth='400%'
vio.height=window.outerHeight;

function rotvi(krot)
{

if(isrot)
{

vio.style.webkitTransform='';
vio.height=window.outerHeight;

isrot=false;
}
else
{

vio.style.webkitTransform = 'rotate('+krot+'deg)'; 
vio.height=window.outerWidth-20;

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
        case 98:
	plbrate-=0.21;
	if(plbrate<0.21)
	{plbrate=0.21;}
	ratechange();
            break;
	case 100:
		vio.height-=50;
		
	break;
	case 101:
		plbrate=1.0;
		ratechange();
	break;
        case 102:
	vio.height+=50;
	
            break;
	case 103:
	rotvi('270');
            break;
        case 104:
	plbrate+=0.21;
	ratechange();
            break;
	case 105:
	rotvi('90');
            break;




    }
};