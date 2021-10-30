<template>
  <div>
    <el-button type="primary" v-if="!isLogin"  @click="login">登录</el-button>
  </div>
</template>

<script >
import oidcUserManager from '../common/oidc'
export default {
  name: 'Header',
  data() {
    return {
      isLogin:false
    };
  },
  created(){
    oidcUserManager.getUser().then((user)=> {
        if (user) {
          this.isLogin=true;
            console.log("User:",user);
            this.$router.push("/");
        }
    });
  },
  methods:{
    login(){
      oidcUserManager.signinRedirect();
    },
  }
}
</script>

<style>

</style>
