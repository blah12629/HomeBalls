var themeRootId = "theme-root";
var themeKey = "ThemeId";
var themeDefault = "dream";
var isDarkModeKey = "IsDarkMode";
var isDarkModeDefault = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;

ensureThemeValuesExist();
updateTheme();

function ensureThemeValuesExist() {
    ensureValueExists(themeKey, themeDefault);
    ensureValueExists(isDarkModeKey, isDarkModeDefault);
}

function ensureValueExists(key, value) {
    if (localStorage.getItem(key) === null) localStorage.setItem(key, value);
}

function getIconUrl(theme = null, isDarkMode = null) {
    theme = getThemeValues(theme, isDarkMode).theme;
    let iconUrl = `https://www.serebii.net/itemdex/sprites/pgl/${theme}ball.png`;
    return iconUrl;
}

function getThemeValues(theme = null, isDarkMode = null) {
    return {
        theme: theme === null ? localStorage.getItem(themeKey) : theme,
        isDarkMode: isDarkMode === null ? localStorage.getItem(isDarkModeKey) : isDarkMode
    };
}

function updateTheme(theme = null, isDarkMode = null) {
    let themeValues = getThemeValues(theme, isDarkMode);
    updateThemeRoot(themeValues.theme, themeValues.isDarkMode);
    updateAppIcons(themeValues.theme, themeValues.isDarkMode);
}

function updateThemeRoot(theme = null, isDarkMode = null) {
    let themeValues = getThemeValues(theme, isDarkMode);
    let themeValue = `theme-${themeValues.theme}`;
    if (themeValues.isDarkMode === 'true') themeValue += ` dark`;
    document.getElementById(themeRootId).className = themeValue;
}

function updateAppIcons(theme = null, isDarkMode = null) {
    let themeValues = getThemeValues(theme, isDarkMode);
    let iconUrl = getIconUrl(themeValues.theme, themeValues.isDarkMode);
    let icons = document.querySelectorAll("[id^='app-icon']");
    for (let i = 0; i < icons.length; i ++) {
        icons[i].src = iconUrl;
    }
}