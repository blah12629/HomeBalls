const colors = require('tailwindcss/colors')

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
        dream: {
          pink: '#ea609a',
          purple: '#9070af',
          white: '#ffffff',
          black: '#4b514f',
          DEFAULT: '#f4b4d0',
          dark: '#e098b3',
        },
      },
      fontSize: {
        0: '0rem/* 0px */',
        '2xs': ['0.625rem/* 10px */', '0.75rem/* 12px */'],
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
