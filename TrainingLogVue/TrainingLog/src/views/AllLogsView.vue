<script setup>
import { onMounted, ref } from 'vue';
import { store } from '@/components/Store';
import { http } from '@/api/http';


const activeTag = ref();

const fullLogs = ref([]);

const filteredLogs = ref([]);

const tags = ref([]);

const showLogs = ref([]);

const page = ref(1);


onMounted(async() => {
    GetUserTags();
    await GetList();
    showLogsPartial();
})

async function GetList() {
        let url = '/GetList/' + store.user.id;
    const res = await http.get(url);
    (res.data).forEach(element => {
        var date = new Date(element.date)
        fullLogs.value.push({id: element.id, text: element.logText, date: date.toLocaleDateString()})
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
    filteredLogs.value = [] 
    if(activeTag.value == id) {
        activeTag.value = null;
    }
    else{
    activeTag.value = id;
    let url = '/GetLogListWithTag/' + id;
    const res = await http.get(url);
    (res.data).forEach(element => {
        (fullLogs.value).forEach(log => {
            if(log.id == element){
            filteredLogs.value.push(log)
            }
        })
    });  
    }
    
    showLogsPartial()
}


function showLogsPartial() {
    const isFilltered = filteredLogs.value.length > 0 ? true : false;
    const startIndex = (page.value - 1) * 8;
    if(isFilltered){
        showLogs.value = filteredLogs.value.slice(startIndex, page.value * 8);
    } else {
        showLogs.value = fullLogs.value.slice(startIndex, page.value * 8);
    }
    console.log(showLogs.value);
}
</script>

<template>
    <main>
        <div class="tags">
            <h2 class="title">Tags</h2>
            <button v-for="tag in tags" :class="{ active: tag.id === activeTag}" @click="GetListWithTag(tag.id)">{{ tag.title }}</button>
        </div>
        <div class="fulllogs">
            <div v-for="log in showLogs" :key="log.id">
                <RouterLink :to="'/log/' + log.id">
                <button>
                {{ log.date }}
            </button>
        </RouterLink>
    </div> 
        </div>
        <button @click="page > 1 ? page-- : page; showLogsPartial()">Previous</button>
        <button @click="page < showLogs.length / 7 ? page++ : page; showLogsPartial()">Next</button>
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
    justify-content: center;
    flex-wrap: wrap;
    width: 22vw;
    background-color: grey;
}

.tags > button {
    width: 5vw;
}

.fulllogs {
    display: flex;
    flex-direction: column;
    align-items: center;
    background-color: darkgray;
}

.active {
    background-color: green;
}

.title {
    width: 22vw;
    text-align: center;
}


</style>