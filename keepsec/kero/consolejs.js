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

//imh
//https://imhentai.xxx/js/expp.js

function klear()
{
    lding=0;
    pglim=0;
    pgnn=0;
    pgnn2=0;
    notload=false;
    loadcot=0;
    document.body.innerHTML='verysma<div>verysma</div>sma<div>sma</div>big<div>big</div><div>loading</div><pre>info1</pre><pre>info2</pre>';
    
    
    
    averysma=document.body.children[0];
    asma=document.body.children[1];
    abig=document.body.children[2];
    aload=document.body.children[3];
    ainfo1=document.body.children[4];

    nwd=window.open('https://ezgif.com/optimize', null, null);


}
var block5s=false;

function rst5s()
{
    block5s=false;

}


document.ondblclick=function(ev)
{

    if(block5s){return;}

    var ele=ev.target.src;
    if(ele)
    {
        nwd.location.href ='https://ezgif.com/optimize?url='+ele;
        block5s=true;
        setTimeout(rst5s, 5000);
        nwd.focus();

    }

}

function k3(clea)
{


    
        k2(clea);
   
    
    loadcot=0;
    notload=false;
}

function findnone()
{
    ainfo1.innerText+="\n'"+this.alt+"',";
    this.onload=null;
    this.onerror=null;
    this.remove();

}

function findnone2()
{
   lding--;
    this.onload=null;
    this.onerror=null;
    if(this.naturalWidth==0)
    {
        this.remove();
        
    }

}

function findsome()
{
    abig.appendChild(this);
    this.onload=null;
    this.onerror=null;
    //this.previousSibling.remove();

}

function k2(typ)
{
    switch(typ)
    {
        case 1:
            asma.innerHTML='';
            return;
            
        case 2:
           averysma.innerHTML='';
           return;

        case 3:
            asma.innerHTML='';
            averysma.innerHTML='';
            break;



    }
    abig.innerHTML='';


}

function findsome2()
{

    lding--;

    var naw=this.naturalWidth;
    if(naw==0)
    {
        this.remove();
        return;
    }
    naw=(naw*this.naturalHeight)>>14;



    
    loadcot+=(10+naw);
    if(loadcot>9999){
        notload=true;
        document.title='('+document.title+')';
        }

    
    if(naw>30)
    {
        abig.appendChild(this);

    }
    else if(naw<5)
    {
        averysma.appendChild(this);
    }
    else
    {
        asma.appendChild(this);

    }

    this.onload=null;
    this.onerror=null;

    

}

function mkurl(orig)
{

    return 'https://m'+orig.replace('@','.imhentai.xxx/')+'/';
}

function pgcot10()
{
    if(notload){return;}

    abig.innerHTML='';
    var endo=pgnn+10;
    for(pgnn=pgnn;pgnn<endo;pgnn++)
    {
        var ymg=document.createElement('img');
        ymg.src=mkurl(gal[pgnn])+pglim+'t.jpg';
        ymg.alt=gal[pgnn];
        ymg.onload=findsome;
        ymg.onerror=findnone;
        aload.appendChild(ymg);

    }


}

function getgoodpgnn(endo)
{
    for(var i=pgnn;i<pgE;pgnn++)
    {
        if(endo<gallims[pgnn]){return pgnn;}
    }
}

function pgshow10()
{
    if(notload){return;}

    if(lding>10){return;}

    var endo=pgnn2+10;
    pgnn=getgoodpgnn(endo);

    document.title=pgnn+','+(pgnn2-1);

    for(var i=pgnn2;i<endo;i++)
    {
        var ymg=document.createElement('img');
        var syg=gal[pgnn];
        ymg.alt=syg;
        ymg.src=mkurl(syg)+i+'.gif';
        
        ymg.onload=findsome2;
        ymg.onerror=findnone2;
        aload.appendChild(ymg);
        lding++;

    }

    
    
    pgnn++;
    if(pgnn>=pgE)
    {
        pgnn=0;
        pgnn2+=10;
       
    }
    

}

var glolim=50;
var gallims=null;


function prepgal()
{
    gallims=new Array(pgE);
    if(gal[0].split('@').length==3)
    {
        for(var i=0;i<pgE;i++)
        {
            var imz=gal[i].split('@');
            gal[i]=imz[0]+'@'+imz[2];
            gallims[i]=10+parseInt(imz[1].substr(1),10)*5;
        }
    }
    else
    {
        for(var i=0;i<pgE;i++)
        {
            gallims[i]=glolim;
        }
    }
}

function  dofshow()
{
    pgE=gal.length;
    prepgal();
    pgshow10();
    setInterval(pgshow10, 5000);

}

function f_show(galsta,pgsta,lymz)
{
    
    pgnn=galsta;
    pgnn2=pgsta+1;

    if((typeof gal === 'undefined')&&lymz){
        glolim=lymz*10;
        var sk = document.createElement('script');
	   sk.src='https://klanly.github.io/imh/az'+lymz+'.js';
	   sk.onload=dofshow;
	   document.body.appendChild(sk);
    }
    else{dofshow();}


}


function f_cot(lim)
{
    ainfo1.innerText='less than '+lim;
    pglim=lim;
    setInterval(pgcot10, 2000);

}

klear();


//var gal=[
