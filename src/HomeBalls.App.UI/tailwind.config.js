const colors = require('tailwindcss/colors');

module.exports = {
  mode: 'jit',
  purge: {
    enabled: false,
    content: [
        './**/*.html',
        './**/*.razor'
    ],
  },
  darkMode: 'media', // false or 'media' or 'class'
  theme: {
    extend: {
      colors: {
        dream: {
          pink: "#ea609a",
          purple: "#9070af",
          white: "#ffffff",
          black: "#4b514f",
          DEFAULT: "#f4b4d0",
          dark: "#e098b3",
        }
      },
      fontSize: {
        '0': '0rem',
      },
      spacing: {
        '1.75': '0.4375rem/* 7px */;',
        '128': '32rem/* 512px */;',
        '160': '40rem/* 640px */;'
      }
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}
