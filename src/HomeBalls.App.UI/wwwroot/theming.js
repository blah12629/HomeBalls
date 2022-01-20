const themeKey = "themeId";
setTheme(getTheme());

function getTheme() {
    var themeId = localStorage.getItem(themeKey);

    if (themeId === null) {
        themeId = "moon";
        localStorage.setItem(themeKey, themeId);
    }

    return themeId;
}

function getIconUrl() {
    var iconUrl = `https://www.serebii.net/itemdex/sprites/pgl/${getTheme()}ball.png`;
    console.log(iconUrl);
    return iconUrl;
}

function setTheme(themeId) {
    localStorage.setItem(themeKey, themeId);
    document.getElementById("theme-root").className = `theme-${themeId}`;

    var iconUrl = getIconUrl();
    var icons = document.querySelectorAll("[id^='app-icon']");
    for (var i = 0; i < icons.length; i ++) {
        icons[i].src = iconUrl;
    }

    console.log(`Theme set to \`${themeId}\`.`);
}