var banner = document.createElement("div");
banner.style.minHeight = "100px";
script = document.scripts[document.scripts.length - 1];
var adid = document.currentScript.getAttribute('x');
var add = 'https://localhost:44354/Redirect?x=' + adid;
banner.innerHTML =
    '<a href='+ add +'><span style="height: 100px;width: 800px; background: red; position: fixed; z-index: 99999;"></span></a>\n'
script.parentElement.insertBefore(banner, script);


