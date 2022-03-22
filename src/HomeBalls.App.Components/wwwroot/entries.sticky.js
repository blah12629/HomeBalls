window.elementIds = {
    topContainer: "top-container",
    entries: {
        header: {
            padding: { left: null, right: null },
            number: null,
            sprite: null,
            name: null,
            balls: []
        },
        rows: []
    }
}
window.elements = { topContainer: null, entries: { header: { number: null, sprite: null, name: null } } };
window.styles = { topContainer: null, entries: { header: { number: null, sprite: null, name: null } } };
window.rectangles = { topContainer: null, entries: { header: { number: null, sprite: null, name: null } } }

window.resizes = {
    topContainer: new ResizeObserver((entries, observer) => {
        entries.forEach(entry => {
            updateTopContainerElement();
            getEntriesHeaderIds().forEach(id => tryUpdateHeaderTop(id));
        });
    }),
    entries: {
        number: new ResizeObserver((entries, observer) => {
            entries.forEach(entry => {
                updateEntryHeaderNumberElement();
                getEntriesSpriteIds().forEach(id => tryUpdateEntriesSpriteLeft(id));
                getEntriesNameIds().forEach(id => tryUpdateEntriesNameLeft(id));
                // console.log(`${window.elementIds.entries.header.number} resized to ${window.rectangles.entries.header.number.width}px.`);
            });
        }),
        sprite: new ResizeObserver((entries, observer) => {
            entries.forEach(entry => {
                updateEntryHeaderNumberElement();
                updateEntryHeaderSpriteElement();
                getEntriesNameIds().forEach(id => tryUpdateEntriesNameLeft(id));
                // console.log(`${window.elementIds.entries.header.sprite} resized to ${window.rectangles.entries.header.sprite.width}px.`);
            });
        })
    }
}

updateTopContainerElement();
window.resizes.topContainer.observe(document.getElementById(window.elementIds.topContainer));

function updateTopContainerElement() {
    window.elements.topContainer = document.getElementById(window.elementIds.topContainer);
    window.styles.topContainer = window.getComputedStyle(window.elements.topContainer);
    window.rectangles.topContainer = window.elements.topContainer.getBoundingClientRect();
}
function updateEntryHeaderNumberElement() {
    window.elements.entries.header.number = document.getElementById(window.elementIds.entries.header.number);
    window.styles.entries.header.number = window.getComputedStyle(window.elements.entries.header.number);
    window.rectangles.entries.header.number = window.elements.entries.header.number.getBoundingClientRect();
}
function updateEntryHeaderSpriteElement() {
    window.elements.entries.header.sprite = document.getElementById(window.elementIds.entries.header.sprite);
    window.styles.entries.header.sprite = window.getComputedStyle(window.elements.entries.header.sprite);
    window.rectangles.entries.header.sprite = window.elements.entries.header.sprite.getBoundingClientRect();
}

function getEntriesHeaderIds() {
    let headerIds = window.elementIds.entries.header;
    return [
        headerIds.padding.left, headerIds.padding.right,
        headerIds.number, headerIds.sprite, headerIds.name
    ].concat(headerIds.balls).filter(id => id !== null);
};
function getEntriesNumberIds() {
    return getEntriesColumnIds(window.elementIds.entries.header.number, row => row.number);
}
function getEntriesSpriteIds() {
    return getEntriesColumnIds(window.elementIds.entries.header.sprite, row => row.sprite);
}
function getEntriesNameIds() {
    return getEntriesColumnIds(window.elementIds.entries.header.name, row => row.name);
}
function getEntriesColumnIds(headerId, rowMap) {
    return [headerId].concat(window.elementIds.entries.rows.map(rowMap)).filter(id => id !== null);
}

function registerHeaderLeftPaddingId(id) {
    window.elementIds.entries.header.padding.left = id;
    tryUpdateHeaderTop(id);
}
function registerHeaderRightPaddingId(id) {
    window.elementIds.entries.header.padding.right = id;
    tryUpdateHeaderTop(id);
}
function registerHeaderNumberId(id) {
    window.elementIds.entries.header.number = id;
    updateEntryHeaderNumberElement();
    tryUpdateHeaderTop(id);
    window.resizes.entries.number.observe(document.getElementById(id));
}
function registerHeaderSpriteId(id) {
    window.elementIds.entries.header.sprite = id;
    updateEntryHeaderSpriteElement();
    tryUpdateHeaderTop(id);
    tryUpdateEntriesSpriteLeft(id);
    window.resizes.entries.sprite.observe(document.getElementById(id));
}
function registerHeaderNameId(id) {
    window.elementIds.entries.header.name = id;
    tryUpdateHeaderTop(id);
    tryUpdateEntriesNameLeft(id);
}
function registerHeaderBallId(id) {
    window.elementIds.entries.header.balls.push(id);
    tryUpdateHeaderTop(id);
}

function registerRowIndex(index) {
    window.elementIds.entries.rows[index] = { sprite: null, name: null };
}
function registerRowSpriteId(index, id) {
    window.elementIds.entries.rows[index].sprite = id;
    tryUpdateEntriesSpriteLeft(id);
}
function registerRowNameId(index, id) {
    window.elementIds.entries.rows[index].name = id;
    tryUpdateEntriesNameLeft(id);
}

function tryUpdateHeaderTop(id) {
    let element = document.getElementById(id);
    if (element === null) return;

    let height = window.rectangles.topContainer.height +
        parseFloat(window.styles.topContainer.marginTop) +
        parseFloat(window.styles.topContainer.marginBottom);
    element.style.top = `${height}px`;
}
function tryUpdateEntriesSpriteLeft(id) {
    let element = document.getElementById(id);
    if (element === null) return;

    let left = window.rectangles.entries.header.number.width +
        parseFloat(window.styles.entries.header.number.left);
    element.style.left = `${left}px`;
}
function tryUpdateEntriesNameLeft(id) {
    let element = document.getElementById(id);
    if (element === null) return;

    let left = window.rectangles.entries.header.number.width +
        parseFloat(window.styles.entries.header.number.left) +
        window.rectangles.entries.header.sprite.width;
    element.style.left = `${left}px`;
}