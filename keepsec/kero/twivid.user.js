// ==UserScript==
// @match https://video.twimg.com/*
// ==/UserScript==

var vio=document.getElementsByTagName("video")[0];


var plbrate=1.0;



var isrot=false;

function rotvi(krot)
{

deg='0';
if(isrot)
{


vio.style.top = '0px';
//vio.width=vio.videoWidth;
vio.height=window.outerHeight;

isrot=false;
}
else
{

vio.style.top = '0px';
deg=krot;
//vio.height=vio.videoHeight;
vio.height=window.outerWidth-20;

isrot=true;
}

vio.style.webkitTransform = 'rotate('+deg+'deg)'; 
vio.style.mozTransform    = 'rotate('+deg+'deg)'; 
vio.style.msTransform     = 'rotate('+deg+'deg)'; 
vio.style.oTransform      = 'rotate('+deg+'deg)'; 
vio.style.transform       = 'rotate('+deg+'deg)'; 

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