//https://imhentai.xxx/js/expp.js

function klear()
{
    bsurl='';
    pglim=0;
    pgnn=0;
    pgnn2=0;
    notload=false;
    loadcot=0;
    document.body.innerHTML='<div>loading</div><div>big</div><div>small</div><div>verysmall</div><pre>info1</pre><pre>info2</pre>';
    aload=document.body.children[0];
    abig=document.body.children[1];
    asma=document.body.children[2];
    averysma=document.body.children[3];
    ainfo1=document.body.children[4];


}

function goon()
{
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


function findsome2()
{

    if(this.naturalWidth==0)
    {
        this.remove();
        return;
    }
    loadcot++;
    if(loadcot>=1000){notload=true;}

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
    for(pgnn2=pgnn2;pgnn2<endo;pgnn2++)
    {
        var ymg=document.createElement('img');
        ymg.src=bsurl+pgnn2+'.gif';
        ymg.alt=gal[pgnn];
        ymg.onload=findsome2;
        ymg.onerror=findnone2;
        aload.appendChild(ymg);

    }

    document.title=pglim+','+pgnn+','+pgnn2+') //'+bsurl;

    if(pgnn2>=pglim)
    {
        pgnn2=0;
        pgnn++;
        bsurl=mkurl(gal[pgnn]);

    }
    

}

function f_show(lim,galsta,pgsta)
{
    pglim=lim;
    pgnn=galsta;
    pgnn2=pgsta;

    bsurl=mkurl(gal[pgnn]);
    pgshow10();
    setInterval(pgshow10, 5000);

}


function f_cot(lim)
{
    ainfo1.innerText='less than '+lim;
    pglim=lim;
    setInterval(pgcot10, 2000);

}

klear();

//var gal=[
//];
