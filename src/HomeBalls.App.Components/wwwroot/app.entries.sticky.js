window.elementIds = {
    appRoot: "app-root",
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

window.elements = {
    appRoot: null,
    topContainer: null,
    entries: { header: { number: null, sprite: null, name: null } }
};
window.styles = { topContainer: null, entries: { header: { number: null, sprite: null, name: null } } };
window.rectangles = { topContainer: null, entries: { header: { number: null, sprite: null, name: null } } }
window.scrollPosition = { appRoot: { x: 0, y: 0 } }

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
updateAppRootElement();
window.elements.appRoot.onscroll = element => {
    let previousPosition = window.scrollPosition.appRoot;
    let currentPosition = { x: element.target.scrollLeft, y: element.target.scrollTop };
    window.scrollPosition.appRoot = currentPosition;

    let isLeftFlushed = { before: previousPosition.x <= 0, now: currentPosition.x <= 0 };
    if (isLeftFlushed.before == isLeftFlushed.now) return;

    // console.log(`Scrolled by x. ${previousPosition.x}\t${currentPosition.x}`);
    getEntriesShadowedIds().forEach(id => tryUpdateEntriesShadow(id));
};
window.resizes.topContainer.observe(document.getElementById(window.elementIds.topContainer));

function updateAppRootElement() {
    window.elements.appRoot = document.getElementById(window.elementIds.appRoot);
}
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
function getEntriesShadowedIds() {
    let headerIds = window.elementIds.entries.header;
    return [headerIds.number, headerIds.sprite, headerIds.name]
        .concat(window.elementIds.entries.rows.flatMap(row =>
            [ row.number, row.sprite, row.name ]))
        .filter(id => id !== null);
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
    tryUpdateEntriesShadow(id);
    window.resizes.entries.number.observe(document.getElementById(id));
}
function registerHeaderSpriteId(id) {
    window.elementIds.entries.header.sprite = id;
    updateEntryHeaderSpriteElement();
    tryUpdateHeaderTop(id);
    tryUpdateEntriesSpriteLeft(id);
    tryUpdateEntriesShadow(id);
    window.resizes.entries.sprite.observe(document.getElementById(id));
}
function registerHeaderNameId(id) {
    window.elementIds.entries.header.name = id;
    tryUpdateHeaderTop(id);
    tryUpdateEntriesNameLeft(id);
    tryUpdateEntriesShadow(id);
}
function registerHeaderBallId(id) {
    window.elementIds.entries.header.balls.push(id);
    tryUpdateHeaderTop(id);
}

function registerRowIndex(index) {
    window.elementIds.entries.rows[index] = { number: null, sprite: null, name: null };
}
function registerRowNumberId(index, id) {
    window.elementIds.entries.rows[index].number = id;
    tryUpdateEntriesSpriteLeft(id);
    tryUpdateEntriesShadow(id);
}
function registerRowSpriteId(index, id) {
    window.elementIds.entries.rows[index].sprite = id;
    tryUpdateEntriesSpriteLeft(id);
    tryUpdateEntriesShadow(id);
}
function registerRowNameId(index, id) {
    window.elementIds.entries.rows[index].name = id;
    tryUpdateEntriesNameLeft(id);
    tryUpdateEntriesShadow(id);
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

function tryUpdateEntriesShadow(id) {
    let element = document.getElementById(id);
    if (element === null) return;

    element.classList.toggle("shadow-entry-td", window.scrollPosition.appRoot.x > 0);
}