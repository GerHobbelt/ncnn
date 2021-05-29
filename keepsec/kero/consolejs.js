//dbgsvgf
var ochinka=SVGf[1];

var laztpic='';

var WtoH=0;


function showWHcalc(inW,inH)
{
    console.log('R='+WtoH.toFixed(3));
    console.log('W= '+inW+',  (H= '+(inW*WtoH).toFixed(0)+')');
    console.log('(W= '+(inH/WtoH).toFixed(0)+'),  H= '+inH);


}

function sg(vx,vy,vw,vh,garba,pic)
{
    if(pic){
        if(pic!=laztpic)
        {
            WtoH=0;
            var gifpasa=chikagifpa+pic+'.gif';
            ochinka.firstChild.setAttribute('href',gifpasa);
            timgarea.src=gifpasa;
            laztpic=pic;
            setTimeout(function(){WtoH=(timgarea.naturalHeight/timgarea.naturalWidth);showWHcalc(vw,vh);}, 100);

        }
        else{showWHcalc(vw,vh);}
        }
    else {pic=ochinka.firstChild.href.baseVal.replace(chikagifpa,'').replace('.gif','');}

ochinka.setAttribute('x',vx);
ochinka.setAttribute('y',vy);
ochinka.setAttribute('width',vw);
ochinka.setAttribute('height',vh);

var zstr='\nx="'+vx+'" y="'+vy+'" width="'+vw+'" height="'+vh+'" ><feImage href="0bak/tu/'+pic+'.gif"\n'+
vx+','+vy+','+vw+','+vh+',0.5,"'+pic+'",\n';

recarea.value+=zstr;
return zstr;

}

function printchkarr(arr)
{
    var stret='';
    for(var i=0;i<256;i++)
    {
        var tst=256+i;
        if(!arr[tst]){stret+=i+',';}
        tst=255-i;
        if(!arr[tst]){stret+=(-1-i)+',';}

    }
    return stret;

}

function printchika(chkdup)
{
    var chkl=(chika.length/6)<<0;

    if(chkdup)
    {
        var sigchk=new Array(512);

    }

    for(var i=0;i<chkl;i++)
    {
        var bs=i*6;
        var bssig=chika[bs];
        var bsfna=chika[bs+5];

        console.log(bssig+','+chika[bs+1]+','+chika[bs+2]+','+chika[bs+3]+','+chika[bs+4]+',"'+bsfna+'",');
        if(chkdup)
        {

            if(bssig>-256&&bssig<256){sigchk[bssig+256]=1;}
            
            for(var dd=i+1;dd<chkl;dd++)
            {
                var ddbs=dd*6;
                if(bssig==chika[ddbs]){console.log('badsig='+chika[ddbs+5]);}
                else if(bsfna==chika[ddbs+5]){console.log('badfna='+chika[ddbs+5])}

            }

        }

    }

     if(chkdup)
     { return printchkarr(sigchk);
         
     }else{ return 'good';}
   


}
//looppic
document.body.innerText='';

function mkimg(bs,sta,endo)
{
    for(var i=sta;i<endo;i++)
    {

        var ymg=document.createElement('img');
        ymg.src=bs+i+'.gif';
        document.body.appendChild(ymg);

    }

}


function mkimg_d(bs,sta,nimg)
{
    for(var i=sta;i<sta+nimg;i++)
    {

        var ymg=document.createElement('img');

        ymg.src=bs+i+'/detail.gif';
        document.body.appendChild(ymg);

    }

}

//site:hentai-img.com ドット
//https://ja.hentai-img.com/image/pixelated-retro-feel-erotic-pictures/attachment/30/

//mkimg('https://static.hentai-img.com/upload/20140109/2/1322/',0,50)
//mkimg('https://static10.hentai-img.com/upload/20200727/658/673466/',0,50)
//mkimg('https://static2.hentai-img.com/upload/20160801/74/75753/',0,50)
//mkimg('https://static4.hentai-img.com/upload/20170204/248/253889/',0,60)
//mkimg('https://static4.hentai-img.com/upload/20170302/263/268974/',0,50)
//mkimg('https://static4.hentai-img.com/upload/20170320/270/276203/',0,50)
//mkimg('https://static4.hentai-img.com/upload/20170504/298/304984/',0,50)
//mkimg('https://static4.hentai-img.com/upload/20170829/330/337421/',0,50)
//mkimg('https://static5.hentai-img.com/upload/20180302/374/382795/',0,100)
//mkimg('https://static6.hentai-img.com/upload/20180418/420/429067/',0,50)
//mkimg('https://static6.hentai-img.com/upload/20180531/455/465571/',0,50)
//mkimg('https://static6.hentai-img.com/upload/20180601/457/467875/',0,50)
//mkimg_d('https://static.hentai-gif-anime.com/upload/20180726/48/',97400,200)

//njagif
var kole=document.getElementsByTagName("img");
var kolel=kole.length;

for(var i=0;i<kolel;i++)
{
var uv=kole[i].src;
kole[i].src=uv.replace('/__rs_l30x30','');

}

