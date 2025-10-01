<script setup>
import { onMounted, ref } from 'vue';
import { store } from '@/components/Store';
import { http } from '@/api/http';


const activeTag = ref(
)
const Logs = ref([])

const tags = [{id: 1, name: "Climbing"}, {id: 2, name: "Running"}, {id: 3, name: "Calithenics"}]

const showLog = ref('');


onMounted(async () => {
     let url = '/GetList/' + store.user.id;
        const res = await http.get(url);
        (res.data).forEach(element => {
            var date = new Date(element.date)
            Logs.value.push({id: element.id, text: element.logText, date: date.toLocaleDateString()})
        });
})

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