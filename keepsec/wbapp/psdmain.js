var renb=[];
var renbinfo=[];
var renbcanvas=[]

function showkanvu()
{
	this.onclick=null;
	this.children[0].style.display='block';

}

function cleanAlpha(ymg)
{
	var vl=ymg.length;
	for(var i=3;i<vl;i+=4)
	{
		if(ymg[i]<5)
		{
			ymg[i]=0;
			ymg[i-1]=0;
			ymg[i-2]=0;
			ymg[i-3]=0;
		}
	}
return ymg;

}

function alpha_blnd(alph,blen)
{
	var roita='';
	if(alph==1){
		roita='.1';
	} else {
		roita='.'+alph.toFixed(3).substr(2);
	}

	if(blen=='norm')
	{
		return roita+'._.';
	}
	return roita+'.'+blen+'.';
}

const t = "this-is-a-message";
const a = document.querySelector("#results");
const r = document.querySelector('input[type="file"]');
const n = new Worker(new URL("psdwkr.js", document.baseURI || self.location.href), {type: void 0});

n.addEventListener("message", (e => (({data: e}, a) => {
		const {
			type: r,
			timestamp: n,
			value: o
		} = e;
if ("Layer" === r)
{
	const e = o;
	var ynna = `${e.name}`;
	var ynfo = `${e.top}.${e.left}${alpha_blnd(e.composedOpacity,e.blendMode)}${e.visible}`;
	renb.push(ynna);
	renbinfo.push(ynfo);
	a.insertAdjacentHTML("beforeend", `<h3>${ynna}</h3>`);
	a.insertAdjacentHTML("beforeend", `<div><p class="layer-info">${ynfo}</p></div>`);
	var pikbox=document.createElement('H1');
	pikbox.appendChild((e => {
				const t = document.createElement("canvas"),
				a = t.getContext("2d"),
					{
						width: r,
						height: n,
						pixelData: o
					} = e,
				s = a.createImageData(r, n);
				renbcanvas.push(t);
				t.style.display='none';
				cleanAlpha(o);
				return t.width = r, t.height = n, s.data.set(o), a.putImageData(s, 0, 0), t
			})(e))
	pikbox.append('^ShowMe^');
	pikbox.onclick=showkanvu;

	a.appendChild(pikbox);
}
	})(e, a)));

r.addEventListener("change", (() => {
		const e = r.files[0];
		e && ((e => {
			if (e.arrayBuffer) return e.arrayBuffer(); {
				const t = new FileReader;
				return t.readAsArrayBuffer(e), new Promise((e => {
					t.addEventListener("load", (t => {
						if (!t.target) throw new Error("Loaded file but event.target is null");
						e(t.target.result)
					}))
				}))
			}
		})(e).then((e => {
			n.postMessage({
				type: "ParseData",
				value: e,
				signature: t,
				timestamp: Date.now()
			}, [e])
		})), r.value = "", a.innerHTML = "")
	}));



var tbbr=document.getElementsByClassName('aside-content')[0];


function downloadImage(data, filename = 'untitled.png') {
    var a = document.createElement('a');
    a.href = data;
    a.download = filename;
    a.innerText=filename+'\n\n'
    tbbr.appendChild(a);
    a.click();
}

var dkl=-1;

function timeddl()
{
	downloadImage(renbcanvas[dkl].toDataURL("image/png").replace("image/png", "image/octet-stream"), dkl.toString().padStart(3, "0")+'.png');
	dkl--;
	if(dkl>=0)
	{
		setTimeout(timeddl, 100);
	}
}

function dumplaya()
{
	tbbr.innerHTML='';
	dkl=renb.length-1;
	timeddl();
}

function dumplaya_old()
{
	renb=[];
	tbbr.innerHTML='';
        
var laya=document.getElementsByClassName('layer-info');
var layal=laya.length;

for(var i=0;i<layal;i++)
{
    var vuzt=i.padStart(4, "0")+laya[i].innerText;
    
    var dv = laya[i].parentElement;
    renb.push(dv.previousSibling.innerText);
    var dataURL = dv.nextSibling.toDataURL("image/png").replace("image/png", "image/octet-stream");

    downloadImage(dataURL, vuzt);
    
    
}
    }

function clear()
{

    var lynka=document.getElementsByTagName("a");
    var lynkal=lynka.length-1;
    for(var i=lynkal;i>0;i--)
        {
            lynka[i].remove();
        }
}

function ren()
    {

        return "rb=['"+renb.join("',\n'")+"']\n\nrbi=['"+renbinfo.join("',\n'")+"'];\n\n";
        
    }