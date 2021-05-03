var vio=document.getElementById('media');



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

var delayii=0x0;
var mxw=0;
var mxh=0;

function calcscall()
{
var tmxw=vio.scrollWidth/window.outerWidth;
var tmxh=vio.scrollHeight/window.outerHeight;

if(tmxw>1.0&&tmxh>1.0)
{
mxw=tmxw-0.9;
mxh=tmxh-0.9;
return true;
} 
return false;
	
}

function ruu(x,y)
{
document.body.scrollLeft=x*mxw;
document.body.scrollTop=y*mxh;
}



function fakekuriakey(){return;}


var mufunc0=function(ev) {

if((delayii&0xF)==0x0) {
	delayii=0x1;
	ruu(ev.screenX,ev.screenY);
} else{delayii++;}

};

function ratechange()
{
vio.playbackRate=plbrate;
document.title="rate="+plbrate;
}

var panni=false;
var notplu500=true;

document.onkeydown=function(e) {
vio.muted=false;
if(panni)
{
	switch (e.keyCode) {

	case 27:
	case 106:
	case 112:
		close();
		return;
	case 111:
		panni=false;
		vio.controls=true;
		document.onmousemove=fakekuriakey;
		document.body.style.overflow = "auto";
		return;
	case 104:
		document.body.scrollTop-=50;
		return;

	case 98:
		document.body.scrollTop+=50;
		return;
	case 100:
		document.body.scrollLeft-=50;
		return;
	case 102:
		document.body.scrollLeft+=50;
		return;

	case 103:
		document.body.scrollLeft-=50;
		document.body.scrollTop-=50;
		return;
	case 99:
		document.body.scrollLeft+=50;
		document.body.scrollTop+=50;
		return;
	case 105:
		document.body.scrollLeft+=50;
		document.body.scrollTop-=50;
		return;
	case 97:
		document.body.scrollLeft-=50;
		document.body.scrollTop+=50;
		return;
	case 88:
		plbrate-=0.1;
		if(plbrate<0.2)
		{plbrate=0.2;}
		ratechange();
		return;
	case 65:
		

		vio.height-=100;

		calcscall();
			
		return;

	case 68:
		
		vio.height+=100;

		calcscall();
		
		return;

	case 87:
		plbrate+=0.1;
		ratechange();
		return;
	}

return;
}


	switch (e.keyCode) {



	case 27:
	case 106:
	case 112:
		close();
		return;
	case 111:
		if(!isrot){
		vio.style.margin= '0px';
		if(notplu500)
		vio.height+=600;
		notplu500=false;
		}

	
		if(calcscall()){
			vio.controls=false;
			document.body.style.overflow = "hidden";
			document.onmousemove=mufunc0;
		}

		panni=true;
		return;

	case 98:
		plbrate-=0.1;
		if(plbrate<0.2)
		{plbrate=0.2;}
		ratechange();
		return;
	case 100:
			if(!isrot){vio.style.margin= '0px';}
			vio.height-=100;
			
		return;
	case 101:
			vio.style.margin='auto';
			plbrate=1.0;
			ratechange();
		return;
	case 102:
		if(!isrot){vio.style.margin= '0px';}
		vio.height+=100;
		notplu500=false;
		
		return;
	case 103:
		rotvi('270');
		break;
	case 104:
		plbrate+=0.1;
		ratechange();
		return;
	case 105:
		rotvi('90');
		return;



	}
};