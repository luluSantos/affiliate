var banner = document.createElement("div");
banner.style.minHeight = "100px";
script = document.scripts[document.scripts.length - 1];
var console1 = document.currentScript.getAttribute('x');
var console2 = document.currentScript.getAttribute('y');
var console3 = document.currentScript.getAttribute('z');
var ads = document.currentScript.getAttribute('w')
banner.innerHTML =
    '<iframe src="https://localhost:44354/DynamicWidget.aspx?w='+ ads + '"&x='+ console1 +'&y='+ console2 +'&z='+ console3 +' style=" border-width:0 " width="800" height="400" frameborder="0" scrolling="no"></iframe >'
   // '<a href='+ add +'><span style="height: 100px;width: 800px; background: red; position: fixed; z-index: 99999;"></span></a>\n'
script.parentElement.insertBefore(banner, script);


