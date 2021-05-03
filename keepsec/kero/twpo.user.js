// ==UserScript==
// @match https://twitter.com/*/status/*
// ==/UserScript==


document.onkeydown=function(e) {


switch (e.keyCode) {
case 27:
case 106:
case 112:
	close();
	return;}};