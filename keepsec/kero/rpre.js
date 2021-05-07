function fistr()
{
var tblarea = document.getElementById('team');
var alul=thumb.length;
var rez=new Array(alul);
alul--;

for(var i=alul;i>=0;i--) {rez[alul-i]=thumb[i]+'\t'+vids[i]+'\t'+msgs[i]+'\n';}
tblarea.innerHTML= rez.join(''); 
}

fistr();