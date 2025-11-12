<script setup>
import { store } from '@/components/Store';
import { onMounted } from 'vue';
import { http } from '@/api/http';

var model = {
    inputs: {
        id: null,
        userConnection: null,
        logText: "",
        date: null,
        starttime: null,
        endtime: null,
        
    }
}

onMounted(() => {
     if(store.user.id !== null){
        model.inputs.userConnection = store.user.id
     }
        });

async function CreateNewLog() {
    model.inputs.id = generateGUID();
    console.log(model.inputs)
        let url = '/CreateLog';
        const res = await http.post(url, model.inputs);


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
            <input type="time" v-model="model.inputs.starttime">
            <input type="time" v-model="model.inputs.endtime">
        </div>
        <div class="Date">
            <input type="date" required v-model="model.inputs.date">
        </div>
        <div class="ActivityText">
            <textarea id="Text" required v-model="model.inputs.logText"></textarea>
        </div>
        <div>
            <label>Velg hva slags aktivitet: </label>
            <br>
            <select class="tags">
                <option>Test</option>
                <option>Test 1</option>
                <option>Test2</option>
                <option>Test3</option>
                <option>Test4</option>
            </select>
        </div>
         <div class="submit"> 
            <button>Submit</button>
        </div>
       
        </form>
</template>

