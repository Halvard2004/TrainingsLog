<script setup>
import { RouterLink, RouterView } from 'vue-router'
import Navi from './components/Nav.vue'
import { store } from './components/Store';
</script>

<template>
  <div>
  <header>
    {{ store.user.username }}
    <div>
      
      <h1>Welcome to my Training app</h1>
      <nav class="nav">
        <Navi></Navi>
      </nav>
    </div>
  </header>
<div class="wrapper">
  <Suspense>
    <RouterView />
  </Suspense>
  </div>
</div>
  
</template>

<style scoped>
.app {
  align-content: center;
}
.nav {
  border-top-style: solid;
  padding-top: 1vh;
}


.wrapper {
  display: flex;
  justify-content: center;
  border-style: solid;
  border-radius: 20px;
  width: 40vw;
  height: 60vh;
  margin-top: 10px;
  flex-wrap: wrap;;
}
</style>
