// ==UserScript==
// @match https://twitter.com/*/status/*
// @match https://twitter.com/*/with_replies
// ==/UserScript==

var noturl=false;

document.onkeydown=function(e) {


switch (e.keyCode) {
case 27:
case 106:
case 112:
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