<template>
  <div id="app">
    <FeeCalculator />
  </div>
</template>

<script>
import FeeCalculator from './components/FeeCalculator.vue';

export default {
  name: 'App',
  components: {
    FeeCalculator
  }
};
</script>

<style scoped>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  font-family: Arial, sans-serif;
  background-color: #f5f5f5;
  color: #333;
  padding: 20px;
}

#app {
  width: 100%;
  margin: 0 auto;
}
</style>
