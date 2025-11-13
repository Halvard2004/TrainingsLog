<script setup>
import { store } from '@/components/Store';
import { onMounted, ref } from 'vue';
import { http } from '@/api/http';

var model = {
    inputs: 
        {
        log: {
            id: null,
            userConnection: null,
            logText: "",
            date: null,
            starttime: null,
            endtime: null,
            
        },
        tag: {
            id: null
        }
        },
    tags: ref([]),
    logTag: {
        id: null,
        log_id: null,
        tag_id: null
    }
}

onMounted(() => {
     if(store.user.id !== null){
        model.inputs.log.userConnection = store.user.id;
        GetUserTags();
     }
        });

async function CreateNewLog() {
    model.inputs.log.id = generateGUID();
        let url = '/CreateLog';
        http.post(url, model.inputs.log);
        console.log("Made New Log")
    if(model.inputs.tag.id !== null){
        console.log("Start To make Connection")
        CreateNewTagLogConnection();
        console.log("Made Connection")
    }    
    model.inputs.log = {
        userConnection: store.user.id
    }


}

async function GetUserTags() {
        let url = '/GetTags/' + store.user.id;
    const res = await http.get(url);
    (res.data).forEach(element => {
        model.tags.value.push({id: element.id, title: element.title, User_Id: element.user_Id})
    });

}

async function CreateNewTagLogConnection() {
    model.logTag.id = generateGUID();
    model.logTag.log_id = model.inputs.log.id;
    model.logTag.tag_id = model.inputs.tag.id;
    let url = '/CreateLogTagConnection';
    http.post(url, model.logTag);
    model.logTag = {
    };

}

function generateGUID() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    const r = Math.random() * 16 | 0; 
    const v = c === 'x' ? r : (r & 0x3 | 0x8); 
    return v.toString(16);
  });
}

</script>

<template>
 <h1>Legg til aktivitet</h1>
        <form @submit.prevent="CreateNewLog" class="form">
        <div class="TimeInDay">
            <h2>Start</h2>      
            <h2>Stop</h2>
            <input type="time" v-model="model.inputs.log.starttime">
            <input type="time" v-model="model.inputs.log.endtime">
        </div>
        <div class="Date">
            <input type="date" required v-model="model.inputs.log.date">
        </div>
        <div class="ActivityText">
            <textarea id="Text" required v-model="model.inputs.log.logText"></textarea>
        </div>
        <div>
            <label>Velg hva slags aktivitet: </label>
            <br>
            <select class="tags" v-model="model.inputs.tag.id">
                <option v-for="tag in model.tags.value" :value="tag.id">{{ tag.title }}</option>
            </select>
        </div>
         <div class="submit"> 
            <button>Submit</button>
        </div>
       
        </form>
</template>

