function openComponent(outerId, innerId) {
    let height = document.getElementById(innerId).getBoundingClientRect().height;
    document.getElementById(outerId).style.height = `${parseFloat(height)}px`;
}

function closeComponent(outerId) {
    document.getElementById(outerId).style.height = "0px";
}