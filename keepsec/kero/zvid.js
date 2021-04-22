var vio=document.getElementById('media');

var plbrate=1.0;

vio.height=window.outerHeight;

var isrot=false;

function rotvi()
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
deg='90';
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
vio.muted = false;
    switch (e.keyCode) {
        case 100:
            vio.height-=20;
            break;
case 105:
	rotvi();
            break;
        case 101:
	plbrate=1.0;
	ratechange();
            break;
        case 104:
	plbrate+=0.2;
	ratechange();
            break;
        case 102:
            vio.height+=20;
            break;
        case 98:
	plbrate-=0.2;
	ratechange();
            break;
    }
};