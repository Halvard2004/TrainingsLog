<script setup>
import TheWelcome from '../components/TheWelcome.vue'

const days = [{dag: 'Mandag', status: true, today: false}, 
              {dag: 'Tirsdag', status: true, today: false}, 
              {dag: 'Onsdag', status: true, today: false}, 
              {dag: 'Torsdag', status: false, today: true}, 
              {dag: 'Fredag', status: false, today: false}, 
              {dag: 'Lørdag', status: false, today: false}, 
              {dag: 'Søndag', status: false, today: false}]
</script>

<template>
  <main>
   <div v-for="day in days">
      <button :class="day.status ? 'Done' : 'NotDone'">{{ day.dag }} {{ day.today ? 'Today' : '' }}</button>
   </div>
  </main>
</template>


<style scoped>
main {
  margin: 2vh;
}
button {
  background-color: cornflowerblue;
  border-radius: 10px;
  max-height: 40px;
  height: 4vh;
  width: 12vw;
}
</style>