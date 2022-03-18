var entriesId = "entries";
var topContainedId = "top-container";
var entriesElement = document.getElementById(entriesId);
var topContainerElement = document.getElementById(topContainedId);

var topContainerResizes = new ResizeObserver((entries, observer) => {
    entries.forEach(entry => updateEntriesHeaderPosition());
});
topContainerResizes.observe(topContainerElement);

function updateEntriesHeaderPosition() {
    console.log("updateEntriesHeaderPosition");
    document.querySelectorAll("[id^='entries-head']").forEach(element => {
        updateEntriesElementPosition(element);
    });
}

function updateEntriesElementPositionById(id) {
    var cell = document.getElementById(id);
    if (cell !== null) updateEntriesElementPosition(cell);
}

function updateEntriesElementPosition(element) {
    let style = window.getComputedStyle(topContainerElement);
    let height = topContainerElement.offsetHeight + parseFloat(style.marginTop) + parseFloat(style.marginBottom);
    element.style.top = `${height}px`;

    console.log(`\`#${element.id}.style.top\` set to ${element.style.top}.`);
}