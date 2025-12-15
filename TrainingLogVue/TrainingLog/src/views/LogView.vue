<script setup>
import { RouterLink, RouterView, useRoute } from 'vue-router'
import { store } from '@/components/Store';
import { ref, onMounted } from 'vue';
import { http } from '@/api/http';

var model = {
    route: useRoute(),
    inputs: {
        log: ref({}),
        tag: ref({}),
        
    },
    log: ref({

    }),
    tag: ref({

    }),
    isEdit: ref(false),
    tags: ref([])
}

onMounted(async () => {
    await GetLog()
    await GetTag()
    GetUserTags();
})

async function GetLog() {
    let url = '/GetLog/' + model.route.params.loggId;
    const res = await http.get(url);
    model.log.value = res.data;
    model.log.value.date = new Date(model.log.value.date).toLocaleDateString();
}
async function GetTag() {
    let url = '/GetTagFromLogId/' + model.route.params.loggId;
    const res = await http.get(url);
    model.tag.value = res.data;
}

function isEdit() {
    model.isEdit.value = true;
    model.inputs.log.value = {...model.log.value};
    model.inputs.tag.value = {...model.tag.value};
    let [TempDay, TempMonth, TempYear] = model.inputs.log.value.date.split('.');
    let tempdate = new Date(TempYear + '-' + TempMonth + '-' + TempDay);
    model.inputs.log.value.date = new Date(tempdate.getTime() - (tempdate.getTimezoneOffset() * 60000)).toISOString().substring(0, 10);
}

async function GetUserTags() {
        let url = '/GetTags/' + store.user.id;
    const res = await http.get(url);
    (res.data).forEach(element => {
        model.tags.value.push({id: element.id, title: element.title, User_Id: element.user_Id})
    });

}

async function UpdateLog() {
    if (model.inputs.log.value != model.log.value) {
        let url = '/UpdateLog/' + model.route.params.loggId;
        await http.put(url, model.inputs.log.value);

        if(model.inputs.tag.value != model.tag.value) {
            let url2 = '/UpdateLogTag/' + model.route.params.loggId + '/' + model.inputs.tag.value.id;
            console.log(url2);
            await http.put(url2);
        }

        await GetLog();
        await GetTag();
    }
    
    model.isEdit.value = false;
}

async function DeleteLog() {
    let url = '/DeleteLog/' + model.route.params.loggId;
    await http.delete(url);
}


</script>

<template>
    <main v-if="!model.isEdit.value">
        <h2>Date: {{ model.log.value.date }}</h2>
        <h2>{{ model.log.value.logText }}</h2>
        {{ model.log.value.startTime }}
        {{ model.log.value.endTime }}
         <button>{{ model.tag.value.title }}</button>
         <br></br>
         <button @click="isEdit()">Edit</button>
         <button @click="DeleteLog">Delete</button>
    </main>
    <main v-else>
        <form @submit.prevent="UpdateLog()" class="form">
        <div class="TimeInDay">
            <h2>Start --- Stop</h2>
            <input type="time" v-model="model.inputs.log.value.startTime">
            <input type="time" v-model="model.inputs.log.value.endTime">
        </div>
        <div class="Date">
            <h2>Date</h2>
            <input type="date" required v-model="model.inputs.log.value.date">
        </div>
        <div class="ActivityText">
            <h2>Log Tekst</h2>
            <textarea id="Text" required v-model="model.inputs.log.value.logText"></textarea>
        </div>
        <div>
            <h2><label>Velg hva slags aktivitet: </label></h2>
            <select class="tags" v-model="model.inputs.tag.value.id">
                <option disabled :value="null">Please select one</option>
                <option v-for="tag in model.tags.value" :value="tag.id">{{ tag.title }}</option>
            </select>
        </div>
        <br></br>
        <div class="buttons">
         <div class="submit"> 
            <button>Submit</button>
        </div>
        <div class="cancel"> 
            <button @click="model.isEdit.value = false">Cancel</button>
        </div>
       </div>
        </form>

    </main>
</template>

<style scoped>
.buttons {
    display: flex;
}
</style>