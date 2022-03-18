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

  },
  mounted(){
    this.checkLogin();
  },
  methods:{
    login(){
      this.checkLogin();
      if(!this.isLogin){
        oidcUserManager.signinRedirect();
      }
      
    },
    checkLogin(){
      oidcUserManager.getUser().then((user)=> {
        if (user) {
          this.isLogin=true;
          console.log("User:",user);
          this.$router.push("/");
        }else{
          console.log("User:",user);
        }
      }).catch(a=>{
        console.log("Usercatch:",a);

      });
    }
  }
}
</script>

<style>

</style>
