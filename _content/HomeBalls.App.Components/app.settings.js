function openComponent(outerId, innerId) {
    // console.log(`closeComponent(${outerId}, ${innerId})`);
    let height = document.getElementById(innerId).getBoundingClientRect().height;
    document.getElementById(outerId).style.height = `${parseFloat(height)}px`;
}

function closeComponent(outerId) {
    // console.log(`closeComponent(${outerId})`);
    document.getElementById(outerId).style.height = "0px";
}