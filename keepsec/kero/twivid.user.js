// ==UserScript==
// @match https://video.twimg.com/*
// ==/UserScript==


var vio=document.getElementsByTagName("video")[0];

//document.getElementsByTagName('meta')['viewport'].content="height=device-width";

var plbrate=1.0;
var isrot=false;

vio.loop=true;

function rotvi(krot)
{

if(isrot)
{

vio.style.webkitTransform='';

isrot=false;
}
else
{

vio.style.webkitTransform = 'rotate('+krot+'deg)  scale(1.8)'; 


isrot=true;
}


}

function ratechange()
{
if(plbrate<0.21)
{plbrate=0.2;}

vio.playbackRate=plbrate;
document.title="rate="+plbrate;
}
document.onkeydown=function(e) {
    switch (e.keyCode) {
        case 98:
	plbrate-=0.2;
	ratechange();
            break;
	case 100:
		vio.height-=20;
	break;
	case 101:
		plbrate=1.0;
		ratechange();
	break;
        case 102:
            vio.height+=20;
            break;
	case 103:
	rotvi('270');
            break;
        case 104:
	plbrate+=0.2;
	ratechange();
            break;
	case 105:
	rotvi('90');
            break;




    }
};