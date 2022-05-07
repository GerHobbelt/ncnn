var pfix='';
var bskanvas=document.createElement('WK');
const hydco='#ccc'

function blendmode(flag)
{
	switch(flag)
	{
		case 1:
		return 'multiply';
		case 2:
		return 'screen';
		case 3:
		return 'overlay';
		case 4:
		return 'darken';
		case 5:
		return 'lighten';
		case 6:
		return 'color-dodge';
		case 7:
		return 'color-burn';
		case 8:
		return 'hard-light';
		case 9:
		return 'soft-light';
		case 10:
		return 'difference';
		case 11:
		return 'color';
		case 12:
		return 'luminosity';
		case 13:
		return 'hue';
		case 14:
		return 'saturation';

		case 20:
		return 'lighten';
		case 21:
		return 'color-dodge';

	}
	return 'normal';
/*

'svg:plus':15,
'svg:dst-in':16,
'svg:dst-out':17,
'svg:src-atop':18,
'svg:dst-atop':19,
'krita:soft_light':20,
'krita:linear_dodge':21
*/

}

function pfind(itm)
{
	var hide=false;
	var tof=itm;
	var str='\t'
	for(var i=0;i<10;i++)
	{	
		tof=tof.parentElement;
		var zoi=tof.dataset.header;
		if(zoi)
		{
			zoi=zoi.replaceAll('\\n','\n');
			tof.dataset.header=zoi;
			zoi=zoi.split('\n');
			var zoilen=zoi.length;
			if(zoilen>4)
			{
				var flag=parseInt(zoi[zoilen-1],16);
				if(((flag>>5)&1)==1){hide=true;}
			}

			str=zoi[0]+'\n'+str;
		}
		else { break;}
	}
	if(str.length==1){return ['',hide];}
	return [str.replace('\n\t','\n'),hide];
}

function doKorrect()
{
	if(Korrect.show)
	{
		Korrect.style.display = 'none';
		Korrect.show = false;
	}
	else
	{
		Korrect.style.display = 'inline';
		Korrect.show = true;
	}
}

function setup()
{
LayerCtrl=document.getElementsByClassName('bs')[0];
LayerCtrl.children[0].onclick=doKorrect;
vdl=rbi.length;
vimg=new Array(vdl);
var uv=location.href.split('!');
pfix=uv[1]+'/';
bskanvas.style.height=gH+'px';
bskanvas.style.width=gW+'px';
document.body.appendChild(bskanvas);

for(var i=vdl-1;i>=0;i--)
{
	var zet=rbi[i];
	if(zet) {
	zet=zet.split('.');
	var flag=parseInt(zet[3],16);
	zet[3]=flag;
	rbi[i]=zet;

	var ymg=document.createElement('img');
	ymg.idx=i;
	bskanvas.appendChild(ymg);
	
	ymg.style.top=zet[0]+'px';
	ymg.style.left=zet[1]+'px';
	vimg[i]=ymg;
	loadwitheff(ymg,i, flag);
	}
	
}
var allitm=document.getElementsByTagName('li');
for(var i=vdl-1;i>=0;i--)
{
	var itm=allitm[i];
	var idx=parseInt(itm.title);
	var tup=pfind(itm);
	rbi[idx][2]=tup[0]+itm.innerText;
	var oshow=vimg[idx].show;
	if(tup[1] && oshow)
	{
		itm.style.color='#c0c';
		vimg[idx].show=false;
		vimg[idx].style.display = 'none';
	}
	else
	{

		if(oshow) { itm.style.color='#c00';}
		else {itm.style.color=hydco;}
	}
	

}

Korrect=document.createElement('img');
Korrect.style.top='0px';
Korrect.style.left='0px';
Korrect.style.display = 'none';
Korrect.show=false;

var lbs=pfix+vdl.toString().padStart(3, "0");
Korrect.src=lbs+'.0.avif';
Korrect.style.webkitMask='url('+lbs+'.1.avif) 0% 0% / 100%';


bskanvas.appendChild(Korrect);
document.body.appendChild(LayerCtrl);

}



function loadwitheff(ymg,num,flag=0)
{
	var blnd=flag&0x1F;
	if(blnd!=0){ymg.style.mixBlendMode=blendmode(blnd);}

	var rshow=(flag>>5)&1
	if(rshow==0)
	{
		ymg.src=pfix+num.toString().padStart(3, "0")+'.png';
		ymg.show=true;
	} else {
		ymg.show=false;
		ymg.style.display = 'none';
	}
}

function a2(num)
{
	if(vimg[num].src)
	{
		if(vimg[num].show)
		{
		vimg[num].show=false;
		vimg[num].style.display = 'none';
		return 'HIDE '+rbi[num][2];
		}
		vimg[num].show=true;
		vimg[num].style.display = 'inline';
		return 'SHOW '+rbi[num][2];
	}
	vimg[num].style.display = 'inline';
	loadwitheff(vimg[num],num);
	return rbi[num][2];

}

function a(num)
{
	if(vimg[num].src)
	{
		if(vimg[num].show)
		{
		vimg[num].show=false;
		vimg[num].style.display = 'none';
		return false;
		}
		vimg[num].show=true;
		vimg[num].style.display = 'inline';
		return true;
	}
	vimg[num].style.display = 'inline';
	loadwitheff(vimg[num],num);
	return true;

}

function a_on(num)
{
	if(vimg[num].src)
	{
		if(!vimg[num].show)
		{
		vimg[num].show=true;
		vimg[num].style.display = 'inline';
		
		}
		return;
	}
	vimg[num].style.display = 'inline';
	loadwitheff(vimg[num],num);
	return true;
}

function a_off(num)
{
	vimg[num].show=false;
	vimg[num].style.display = 'none';
}

var lztul=null;

function kontimg(e)
{
	var ele=e.target;
	switch(ele.tagName)
	{
		case 'LI':
		
		if(a(parseInt(ele.title))) { ele.style.color='#c00';}
		else {ele.style.color=hydco;}
		lztul=ele.parentElement;
		return;

		case 'IMG':
			if(ele.idx < vdl)
			{
				console.log(rbi[ele.idx][2]);
			}
		return;

	}
}

function kycmd(e) {
    var ekeyCode=e.keyCode;
    switch (ekeyCode) {
		case 27:
			switch(LayerCtrl.className)
			{
				case 'bs':
					LayerCtrl.className='bb';
					var skal=(1.05/window.devicePixelRatio).toFixed(2);
					if(skal>1.5){LayerCtrl.style.webkitTransform='matrix('+skal+',0,0,'+skal+',120,'+((LayerCtrl.scrollHeight*3)>>2)+')';}
					return;
				case 'bb':
					LayerCtrl.className='bs';
					LayerCtrl.style.webkitTransform='';
					return;
				
			}
			
		return;
		case 65:	//A
			if(lztul)
			{
				var cl=lztul.children.length;
				for(var i=0;i<cl;i++)
				{
					var itm=lztul.children[i];
					if(itm.tagName=='LI')
					{
						a_on(parseInt(itm.title));
						itm.style.color='#c00';
					}
				}

			}
		return;
		case 81:	//Q
			if(lztul)
			{
				var cl=lztul.children.length;
				for(var i=0;i<cl;i++)
				{
					var itm=lztul.children[i];
					if(itm.tagName=='LI')
					{
						a_off(parseInt(itm.title));
						itm.style.color=hydco;
					}
				}

			}
		return;
	}
}
document.onclick=kontimg;
document.onkeydown=kycmd;

setup();