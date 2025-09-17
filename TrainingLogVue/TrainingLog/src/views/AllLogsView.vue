<script setup>
import { ref } from 'vue';



const Logs = ref([{date: '14.08.2025', id: 1, tagsId: [1, 3]}, 
            {date: '15.08.2025', id: 2, tagsId: [2, 3]}, 
            {date: '16.08.2025', id: 3, tagsId: [1]},
            {date: '17.08.2025', id: 4, tagsId: [1, 3]}])

const tags = [{id: 1, name: "Climbing"}, {id: 2, name: "Running"}, {id: 3, name: "Calithenics"}]
</script>

<template>
<main>
    <div v-for="log in Logs">
        <button>{{ log.date }}</button>
    </div>
</main>
</template>

<style scoped>
main {
    margin-top: 2vh;
}

button {
  background-color: cornflowerblue;
  border-radius: 10px;
  max-height: 40px;
  height: 4vh;
  width: 12vw;
  margin: 3px;
}
</style>