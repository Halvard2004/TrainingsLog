import { reactive } from 'vue';

export const store = reactive({
    user: {
        id: null, 
        username: ""
    },
    state: {
        isLoggedIn: false
    },

    LoginUser(Login) {
        this.user = Login
        this.state.isLoggedIn = true
    },
    LogOut() {
        this.user = {id: null}
        this.state.isLoggedIn = false
    },

})
