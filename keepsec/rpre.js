function fistr()
{
var tblarea = document.getElementById('t');
var alul=thumb.length;


for(var i=0;i<alul;i++) {thumb[i]+='\t'+vids[i]+'\t'+msgs[i];}
tblarea.innerHTML= thumb.join('\n');
}

fistr();