<template>
    <main>
        <h1>Legg til aktivitet</h1>
        <form>
        <div class="TimeInDay">
            <h2>Start</h2>      
            <h2>Stop</h2>
            <input type="time"><input type="time">
        </div>
        <div class="Date">
            <input type="date">
        </div>
        <div class="ActivityText">
            <textarea id="Text"></textarea>
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
        </form>
    </main>
</template>

<style scoped>
.TimeInDay > input {
    margin: 20px;
}

.TimeInDay > h2 {
    display: flex;
}

#Text {
    width: 20vw;
    height: 30vh;
}
</style>