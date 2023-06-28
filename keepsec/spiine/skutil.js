const glo={
player:null,
sk:null,
gl:null,
set removepart(v){
	this.sk.defaultSkin.attachments[v]=null;
}

}


var notrec=true;
var uvrecz=[];



function teztrv()
{
    var akz=[]
    var ll= uvrecz.length;
    for(var vv=0;vv<ll;vv++)
    {
        var ty16=uvrecz[vv];
        var tyl=ty16.length;
        var strr='<path d="M '+ty16[0]+' '+ty16[1];
        for(var i=2;i<tyl;i+=2)
        {
            strr+=' L '+ty16[i]+' '+ty16[i+1];
        }
        strr+=' z"/>\n';
        akz.push(strr);
 
    }

    return akz.join('');

}

function recUVs(ftarray)
{
	//if(notrec){return;}

	var ll=ftarray.length;
	var brec=new Uint16Array(ll);
	for(var i=0;i<ll;i++)
	{
		brec[i]=ftarray[i]*2048;
	}
	uvrecz.push(brec);

}


function saveplayer(g)
{
	glo.player=g;

}

function savegl(mwgl)
{
	glo.gl=mwgl.gl;
}

function saveglosk(g)
{
	glo.sk=g;
	var kanvaz=glo.player.firstElementChild;
	kanvaz.style.width=g.width;
	kanvaz.style.height=g.height;
	

}

function onlotest(arb)
{
	arb[0]=0;
	return arb;

}

class ddsImage {
	ddspath;
	onerror;
	crossOrigin;
	ddsdata;
	ddsfmt=0x83F3;
	width;
	height;
	width2;
	height2;
	isRealDDS=true;
	onload_func;
	onerror_func;
	tex;
	set onload(v){this.onload_func=v;}
	set onerror(v){this.onerror_func=v;}
	set src(v)
	{
		this.ddspath=v;

		fetch(this.ddspath).then((response) => {
				return response.arrayBuffer();
			}).then((buf)=>{
				
				var header = new Uint32Array(buf,0,0x80);
				if(header[0]==0x20534444)
				{
					this.tex=this.tex_dds;
					this.height=header[3];
					this.width=header[4];
					this.ddsdata=new Uint8Array(buf,0x80);
				
					this.onload_func(123);
				} else {
					this.tex=this.tex_img;
					this.isRealDDS=false;
					var ymg=new Image();
					this.imgbak=ymg;
					ymg.onload=this.onload_func;
					ymg.onerror=this.onerror_func;
					var _imgburl = URL.createObjectURL( new Blob( [ buf ] ) );
					this.imgburl=_imgburl;
					ymg.src=_imgburl;
				}
				});
		
	}

	tex_dds(gl)
	{
		
		gl.compressedTexImage2D(
			  gl.TEXTURE_2D,
			  0,
			  this.ddsfmt,
			  this.width,
			  this.height,
			  0,
			  this.ddsdata);
		
	}

	tex_img(gl)
	{
		gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, this.imgbak);
		URL.revokeObjectURL( this.imgburl );
	}

	
}
