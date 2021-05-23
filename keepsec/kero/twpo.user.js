// ==UserScript==
// @match https://twitter.com/*/status/*
// @match https://twitter.com/*/with_replies
// ==/UserScript==

var noturl=false;
var notclick=true;

document.onkeydown=function(e) {


switch (e.keyCode) {
case 27:
case 106:
case 112:

	if(notclick)
	{
		var btn=document.getElementsByClassName('css-1dbjc4n r-1kihuf0 r-1ndi9ce')[0];
		if(btn=== undefined){close(); return;}
		btn.firstChild.click();
		document.scrollingElement.scrollTop+=150;
		notclick=false;
		return;
	}
	close();
	return;

case 101:
	if(noturl){return;}
	uv=location.href.split('/');
	if(uv.length>4&&uv[4]=='status') {
	noturl=true;
	window.location.href = 'https://twitter.com/'+uv[3]+'/with_replies';
	} else {noturl=true;};
	return;

}};