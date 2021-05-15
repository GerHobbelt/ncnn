// ==UserScript== 
// @name        syga
// @namespace   twivideo.net
// @description Script description 
// @require http://127.0.0.1/dbs.js
// @include     * 
// @version     1.0.0 
// ==/UserScript==

function settxbox()
{
txbx=document.getElementsByClassName('footer')[0];
txbx.innerHTML='';
yina = document.createElement('textarea');
yina.cols=200;
yina.rows=200;
txbx.appendChild(yina);
return yina;
}

var txbx=settxbox();

var twilistl=twilist.length;
var curna=0;

const vtwsig=	['',				'@',				'!',			'+'];
const vtwimg=['https://pbs.twimg.com/ext_tw_video_thumb/',	'https://pbs.twimg.com/amplify_video_thumb/',	'https://pbs.twimg.com/tweet_video_thumb/','https://pbs.twimg.com/media/'];
const vtwvid=['https://video.twimg.com/ext_tw_video/',		'https://video.twimg.com/amplify_video/',		'https://video.twimg.com/tweet_video/'];

function trimthumb(thupic)
{

	for(i=0;i<4;i++)
	{
		thupic = thupic.replace(vtwimg[i], vtwsig[i]);
		if (thupic.charAt(0) != 'h') {return thupic;}
	}					
	return thupic;
}

function trimvid(vid)
{
	for(i=0;i<3;i++)
	{
		vid = vid.replace(vtwvid[i], vtwsig[i]);
		if (vid.charAt(0) != 'h') {return vid;}
	}	
					
	
	
	return vid;
}


function reclog()
{
	var vio=document.getElementById('result_content');
if(vio)
{
	vio=vio.children[0].children[0];
	ymg=vio.children[0];
	txbx.value+='\n'+trimthumb(ymg.src)+'\t'+trimvid(vio.href)+'\t-'+twilist[curna-1];
	return;
}
txbx.value+='\n;'+twilist[curna-1];

}

function fioua()
{



	if(curna<twilistl)
	{



	var uuarea = document.getElementById('add_url');
	var gobtn = document.getElementById('url_submit');
	document.title=curna+'==='+twilist[curna];
	uuarea.value='https://twitter.com/i/status/'+twilist[curna];
	gobtn.click();
	curna++;
	setTimeout(reclog, 30000);
	}
	else
	{
	document.title='!!end';
	clearInterval(syko);
	}
}

var syko=setInterval(fioua, 80000);

/*
document.onkeydown=function(e) {
    switch (e.keyCode) {
case 104:
fioua();
break;
}
}
*/
