const colors = require('tailwindcss/colors')

function computeColor(key, value, blackKey = '--color-black', whiteKey = '--color-white') {
  var alphas = computeAlphas(value);
  var [r, g, b] = [
    computeChannel(key, alphas, 'r', blackKey, whiteKey),
    computeChannel(key, alphas, 'g', blackKey, whiteKey),
    computeChannel(key, alphas, 'b', blackKey, whiteKey)
  ];
  var color = `${r} ${g} ${b}`;

  return ({ opacityValue }) => {
    return (opacityValue === undefined ?
      `rgb(${color})` :
      `rgb(${color} / ${opacityValue})`);
  }
}

function computeAlphas(value) {
  if (value === 500) return [0, 0, 1];

  var alpha = Math.abs((500 - value) / 100 * 0.2);
  return (value < 500 ? [alpha, 0, 1 - alpha] : [0, alpha, 1 - alpha]);
}

function computeChannel(key, alphas, channel, blackKey = '--color-black', whiteKey = '--color-white') {
  var [w, k, c] = [`var(${whiteKey}-${channel})`, `var(${blackKey}-${channel})`, `var(${key}-${channel})`]
  return `calc(${w} * ${alphas[0]} + ${k} * ${alphas[1]} + ${c} * ${alphas[2]})`;
}

module.exports = {
  content: [ '../HomeBalls.App*/**/*.{html,razor}' ],
  safelist: [ 'theme-dream', 'theme-moon', 'theme-lure' ],
  darkMode: 'class', // false or 'media' or 'class'
  theme: {
    extend: {
      animation: {
        'spin-partial': 'spin-partial 1.5s linear infinite',
      },
      colors: {
        theme: {
          primary: {
            DEFAULT: computeColor('--color-primary', 500),
            100: computeColor('--color-primary', 100),
            200: computeColor('--color-primary', 200),
            300: computeColor('--color-primary', 300),
            400: computeColor('--color-primary', 400),
            500: computeColor('--color-primary', 500),
            600: computeColor('--color-primary', 600),
            700: computeColor('--color-primary', 700),
            800: computeColor('--color-primary', 800),
            900: computeColor('--color-primary', 900),
          },
          secondary: {
            DEFAULT: computeColor('--color-secondary', 500),
            100: computeColor('--color-secondary', 100),
            200: computeColor('--color-secondary', 200),
            300: computeColor('--color-secondary', 300),
            400: computeColor('--color-secondary', 400),
            500: computeColor('--color-secondary', 500),
            600: computeColor('--color-secondary', 600),
            700: computeColor('--color-secondary', 700),
            800: computeColor('--color-secondary', 800),
            900: computeColor('--color-secondary', 900),
          },
          accent: {
            DEFAULT: computeColor('--color-accent', 500),
            100: computeColor('--color-accent', 100),
            200: computeColor('--color-accent', 200),
            300: computeColor('--color-accent', 300),
            400: computeColor('--color-accent', 400),
            500: computeColor('--color-accent', 500),
            600: computeColor('--color-accent', 600),
            700: computeColor('--color-accent', 700),
            800: computeColor('--color-accent', 800),
            900: computeColor('--color-accent', 900),
          },
          black: {
            DEFAULT: computeColor('--color-black', 500),
            100: computeColor('--color-black', 100),
            200: computeColor('--color-black', 200),
            300: computeColor('--color-black', 300),
            400: computeColor('--color-black', 400),
            500: computeColor('--color-black', 500),
          },
          white: {
            DEFAULT: computeColor('--color-white', 500),
            500: computeColor('--color-white', 500),
            600: computeColor('--color-white', 600),
            700: computeColor('--color-white', 700),
            800: computeColor('--color-white', 800),
            900: computeColor('--color-white', 900),
          }
        }
      },
      fontSize: {
        '0': [ '0rem/* 0px */', '0rem/* 0px */' ]
      },
      keyframes: {
        'spin-partial': {
          '0%, 75%': { transform: 'rotate(0deg)' },
          '100%': { transform: 'rotate(360deg)' },
        }
      },
      minWidth: {
        8: '2rem/* 32px */;',
        14: '3.5rem/* 56px */;',
        20: '5rem/* 80px */;',
        24: '6rem/* 96px */;',
        36: 'rem/* 32px */;',
      },
      maxHeight: {
        'none': 'none'
      },
      spacing: {
        0.75: '0.1875rem/* 3px */;',
        1.75: '0.4375rem/* 7px */;',
        6.5: '1.625rem/* 26px */',
        15: '3.75rem/* 60px */',
        22: '5.5rem/* 88px */;',
        128: '32rem/* 512px */;',
        160: '40rem/* 640px */;',
      },
      transitionProperty: {
        'height': 'height',
        'width': 'width'
      }
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}