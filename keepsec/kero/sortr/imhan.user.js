//https://imhentai.xxx/js/expp.js

function klear()
{
    pglim=0;
    pgnn=0;
    pgnn2=0;
    notload=false;
    loadcot=0;
    document.body.innerHTML='<div>loading</div>big<div>big</div>small<div>small</div>verysmall<div>verysmall</div><pre>info1</pre><pre>info2</pre>';
    aload=document.body.children[0];
    abig=document.body.children[1];
    asma=document.body.children[2];
    averysma=document.body.children[3];
    ainfo1=document.body.children[4];


}

function k3(clea)
{
    if(clea){k2(3);}
    
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
   
    this.onload=null;
    this.onerror=null;
    this.remove();

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

    if(this.naturalWidth==0)
    {
        this.remove();
        return;
    }
    loadcot++;
    if(loadcot>=300){
        notload=true;
        console.log('!!waiting!!');
        console.log('('+document.title+')');
        }

    //var ozk=performance.getEntriesByName(this.src);
    //var szz=ozk.encodedBodySize;

    var naw=this.naturalWidth;
    var nah=this.naturalHeight;
    //if(szz>0x400000)
    if(naw>599||nah>599)
    {
        abig.appendChild(this);

    }
    else if(naw<300&&nah<300)
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

function pgshow10()
{
    if(notload){return;}

    var endo=pgnn2+10;

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

    }

    
    pgnn++;
    if(pgnn>=pgE)
    {
        pgnn=0;
        pgnn2+=10;
       
    }
    

}

function  dofshow()
{
    pgE=gal.length;
    pgshow10();
    setInterval(pgshow10, 5000);

}

function f_show(galsta,pgsta,lymz)
{
    
    pgnn=galsta;
    pgnn2=pgsta+1;

    if((typeof gal === 'undefined')&&lymz){
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

//var gal=[];
