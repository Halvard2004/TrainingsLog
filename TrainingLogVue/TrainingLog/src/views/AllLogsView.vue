<script setup>
import { onMounted, ref } from 'vue';
import { store } from '@/components/Store';
import { http } from '@/api/http';


const activeTag = ref();

const Logs = ref([]);

const tags = ref([]);

const showLog = ref('');


onMounted(() => {
    GetUserTags();
    GetList();
})

async function GetList() {
        let url = '/GetList/' + store.user.id;
    const res = await http.get(url);
    (res.data).forEach(element => {
        var date = new Date(element.date)
        Logs.value.push({id: element.id, text: element.logText, date: date.toLocaleDateString()})
    });
}


async function GetUserTags() {
        let url = '/GetTags/' + store.user.id;
    const res = await http.get(url);
    (res.data).forEach(element => {
        tags.value.push({id: element.id, title: element.title, User_Id: element.user_Id})
    });
}

async function GetListWithTag(id) {
    activeTag.value = id;
 
    let url = '/GetListWithTag/' + id;
    const res = await http.get(url);
    (res.data).forEach(element => {
        var date = new Date(element.date)
        Logs.value.push({id: element.id, text: element.logText, date: date.toLocaleDateString()})
    });
}
</script>

<template>
    <main>
        <div class="tags">
            <button v-for="tag in tags" :class="{ active: tag.id === activeTag}" @click="GetListWithTag(tag.id)">{{ tag.title }}</button>
        </div>
        <div class="logs">
            <button v-for="log in Logs">{{ log.date }}</button>
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

.tags {
    display: flex;
    flex-direction: row;
    align-content: center;
    flex-wrap: wrap;
    width: 22vw;
}

.tags > button {
    width: 5vw;
}

.logs {
    display: flex;
    flex-direction: column;
    align-items: center;
}

.active {
    background-color: green;
}


</style>