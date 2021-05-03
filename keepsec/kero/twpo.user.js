// ==UserScript==
// @match https://twitter.com/*/status/*
// @match https://twitter.com/*/with_replies
// ==/UserScript==


document.onkeydown=function(e) {


switch (e.keyCode) {
case 27:
case 106:
case 112:
	close();
	return;}};