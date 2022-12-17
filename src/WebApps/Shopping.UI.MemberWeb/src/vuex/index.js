import { createStore } from 'vuex'

// Create a new store instance.
const store = createStore({
  state () {
    return {
      isLogin: false
    }
  },
  mutations: {
    loginChange (state) {
      state.isLogin=true;
    }
  }
})
export default store;