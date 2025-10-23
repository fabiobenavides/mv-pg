module.exports = {
  testEnvironment: "jsdom",
  transform: {
    "^.+\\.vue$": "vue-jest",          // transforma .vue
    "^.+\\.[jt]sx?$": "babel-jest"     // transforma JS/TS
  },
  moduleFileExtensions: ["js", "cjs", "json", "vue"],
  testMatch: ["**/tests/**/*.spec.cjs"],
  setupFilesAfterEnv: ["<rootDir>/jest.setup.js"]
};
