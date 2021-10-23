// ==UserScript==
// @name imgfunc
// @match https://*.imhentai.xxx/*t.jpg
// ==/UserScript==

function  addymg() {
var urr=this.value;
if(urr.length>10)
{
this.value='';

var boxx=document.createElement('p');
boxx.innerHTML='<img src="'+urr+'"> '+urr;


document.body.appendChild(boxx);
}
}

function instal()
{
document.body.style.color='#fff';
var ymr=document.createElement('input');
ymr.onblur=addymg;
document.body.appendChild(ymr);
}

instal();