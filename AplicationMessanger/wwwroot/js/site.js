// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const winSize = document.querySelector("body"),
    menu = document.querySelector(".main-panel"),
    messages = document.querySelector(".message-part"),
    header = document.querySelector(".message-part_header"),
    messageContainer = document.querySelector(".message-part_container"),
    form = document.querySelector(".message-part_form");
messages.style.width = +(winSize.offsetWidth - menu.offsetWidth) + "px";
header.style.width = +(messages.offsetWidth) * 0.7 + "px";
messageContainer.style.width = +(form.offsetWidth) + "px";

window.addEventListener("resize", () => {
    messages.style.width = +(winSize.offsetWidth - menu.offsetWidth) + "px";
    header.style.width = +(messages.offsetWidth) * 0.7 + "px";
    messageContainer.style.width = +(form.offsetWidth) + "px";
    console.log("32");
});

messages.addEventListener("mouseenter", () => {
    messages.style.overflow = 'hidden';
    console.log("ss");
});
