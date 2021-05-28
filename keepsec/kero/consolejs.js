//dbgsvgf

var ochinka=SVGf.children[3];

function sg(vx,vy,vw,vh,garba,pic)
{
    if(pic){ochinka.firstChild.setAttribute('href',chikagifpa+pic+'.gif');}
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

