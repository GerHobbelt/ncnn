function fistr()
{
var tblarea = document.getElementById('team');
var alul=thumb.length;


for(var i=0;i<alul;i++) {thumb[i]+='\t'+vids[i]+'\t'+msgs[i]+'\n';}
tblarea.innerHTML= thumb.join('');
}

fistr();