const colors = require('tailwindcss/colors')

function addOpacity(variable) {
  return ({ opacityValue }) => {
    if (opacityValue === undefined) {
      return `rgb(var(${variable}))`
    }
    return `rgb(var(${variable}) / ${opacityValue})`
  }
}

module.exports = {
  mode: 'jit',
  purge: {
    enabled: true,
    content: ['./**/*.html', './**/*.razor'],
  },
  darkMode: 'media', // false or 'media' or 'class'
  theme: {
    extend: {
      animation: {
        'spin-partial': 'spin-partial 1.5s linear infinite',
      },
      colors: {
        theme: {
          primary: addOpacity('--color-primary'),
          secondary: addOpacity('--color-secondary'),
          accent: addOpacity('--color-accent'),
          black: addOpacity('--color-black'),
          white: addOpacity('--color-white')
        }
      },
      keyframes: {
        'spin-partial': {
          '0%, 75%': { transform: 'rotate(0deg)' },
          '100%': { transform: 'rotate(360deg)' },
        },
      },
      minWidth: {
        8: '2rem/* 32px */;',
        14: '3.5rem/* 56px */;',
        20: '5rem/* 80px */;',
        24: '6rem/* 96px */;',
        36: 'rem/* 32px */;',
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
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}
