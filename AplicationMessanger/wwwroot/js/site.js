// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const winSize = document.querySelector("body"),
    menu = document.querySelector(".main-panel"),
    messages = document.querySelector(".message-part"),
    header = document.querySelector(".message-part_header"),
    messageContainer = document.querySelector(".message-part_container"),
    form = document.querySelector(".message-part_form");
if (winSize.offsetWidth > 1024) {

    messages.style.width = +(winSize.offsetWidth - menu.offsetWidth) + "px";
} else {
    messages.style.width = +(winSize.offsetWidth) + "px";
}

header.style.width = +(messages.offsetWidth) * 0.7 + "px";
messageContainer.style.width = +(form.offsetWidth) + "px";

window.addEventListener("resize", () => {
    if (winSize.offsetWidth > 1024) {
        messages.style.width = +(winSize.offsetWidth - menu.offsetWidth) + "px";
    } else {
        messages.style.width = +(winSize.offsetWidth) + "px";
    }
    header.style.width = +(messages.offsetWidth) * 0.7 + "px";
    messageContainer.style.width = +(form.offsetWidth) + "px";
});

messages.addEventListener("mouseenter", () => {
    messages.style.overflow = 'hidden';
});

const menuButton = document.querySelector(".menu-button"),
    closeBtn = document.querySelector(".close-button"),
    input = document.querySelector(".input"),
    inputbtn = document.querySelector(".inputBtn"),
    messageForm = document.querySelector(".message-part_form"),
    messagePnl = document.querySelector(".menu_panel-button"),
    navBar = document.querySelector(".nav"),
    closeNavmenu = document.querySelector(".closeNavMenu"),
    back = document.querySelector(".back")
    ;
console.log(closeBtn);
input.style.width = +(messageForm.offsetWidth * 0.8 - inputbtn.offsetWidth) + "px";
window.addEventListener("resize", () => {
    input.style.width = +(messageForm.offsetWidth * 0.8 - inputbtn.offsetWidth) + "px";
});
menuButton.addEventListener("click", () => {
    console.log("123");
    //menu.classList.add("menuActive");
    menu.style.left = 0;
    menu.style.visibility = "visible";
    closeBtn.style.visibility = "visible";
});
messagePnl.addEventListener("click", () => {
    navBar.style.visibility = "visible";
    navBar.style.left = "0%";
    closeNavmenu.style.visibility = "visible";
    back.style.visibility = "visible";
});
closeNavmenu.addEventListener("click", () => {

    navBar.style.left = "-100%";
    navBar.style.visibility = "hidden";
    closeNavmenu.style.visibility = "hidden";
    back.style.visibility = "hidden";
});
closeBtn.addEventListener("click", () => {
    console.log("brt")
    menu.style.left = -100 + "%";
    menu.style.visibility = "hidden";
    closeBtn.style.visibility = "hidden";
});
