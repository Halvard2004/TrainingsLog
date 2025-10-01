<script setup>
import { store } from '@/components/Store';
import { useRoute, useRouter } from 'vue-router'

function logout() {
  if (store.state.isLoggedIn) {
    store.LogOut()
  }
}

</script>

<template>
    <RouterLink to="/"><button>Home</button></RouterLink>
    <RouterLink to="/add"><button>Add</button></RouterLink>
    <RouterLink to="/log"><button>Log</button></RouterLink>
    <RouterLink to="/about"><button>About</button></RouterLink>
    <RouterLink to="/login" v-if="!store.state.isLoggedIn"><button>Login</button></RouterLink>
    <RouterLink to="/login" v-else><button @click="logout">Logout</button></RouterLink>

</template>

<style scoped>
button {
background-color: cornflowerblue;
  border-radius: 10px;
  max-height: 40px;
  height: 4vh;
  width: 18%;
  
}
.LoggInn {
  width: 9%;
}
</style>