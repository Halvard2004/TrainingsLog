<script setup>
import { onMounted, ref } from 'vue';


const days = ref([{dag: 'Mandag', status: true, today: false, date: ''}, 
              {dag: 'Tirsdag', status: true, today: false, date: ''}, 
              {dag: 'Onsdag', status: true, today: false, date: ''}, 
              {dag: 'Torsdag', status: false, today: false, date: ''}, 
              {dag: 'Fredag', status: false, today: false, date: ''}, 
              {dag: 'Lørdag', status: false, today: false, date: ''}, 
              {dag: 'Søndag', status: false, today: false, date: ''}]);


onMounted(() => {

  var today = new Date();
  var day = today.getDay() || 7;
  if (day !== 1) {
    today.setHours(-24 * (day - 1));
  }
  days.value.forEach(element => {
  element.date = today.toLocaleDateString();
  if(today.toLocaleDateString() === new Date().toLocaleDateString()){
    element.today = true;
  } else {
    element.today = false;
  }
  today.setHours(24);
  });
  console.log(days.value)
})

</script>

<template>
  <main v-on:load="">
   <div v-for="day in days">
      <button :class="day.status ? 'Done' : 'NotDone'">{{ day.dag }} {{ day.today ? 'Today' : '' }} {{ day.date == '' ? '' : day.date }}</button>
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
  height: 6vh;
  width: 16vw;
}
</style>