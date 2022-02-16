const themeKey = "ThemeId";
setTheme(getTheme());

function getTheme() {
    var themeId = localStorage.getItem(themeKey);

    if (themeId === null) {
        themeId = "dream";
        localStorage.setItem(themeKey, themeId);
    }

    return themeId;
}

function getIconUrl() {
    var iconUrl = `https://www.serebii.net/itemdex/sprites/pgl/${getTheme()}ball.png`;
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
}